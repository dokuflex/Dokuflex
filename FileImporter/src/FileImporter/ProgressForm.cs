namespace FileImporter
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using DokuFlex.Windows.Common.Services;
    using DokuFlex.Windows.Common.Services.Data;
    using System.Collections.Generic;
    using log4net;
    using FileImporter.ViewModels;

    public partial class ProgressForm : Form
    {
        private string _fileId;
        private bool _taskAsyncStarted;
        private IDataService _dataService;
        private string _token;
        private ILog _log;
        private ProgressViewModel _viewModel;

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var index = 0;
            var filename = string.Empty;
            _taskAsyncStarted = true;
            _viewModel.Importing = true;

            try
            {
                foreach (var item in _viewModel.UploadList)
                {
                    if (index < _viewModel.ItemIndex)
                    {
                        index++;
                        continue;
                    }

                    filename = item.Key;
                    SetProgressIndex(_viewModel.ItemIndex + 1);

                    var result = await _dataService.UploadAsync(_token, _viewModel.CommunityId, string.Empty, _viewModel.FolderId, string.Empty,
                        string.Empty, true, string.Empty, string.Empty, false, new FileInfo(item.Key));

                    if (result != null)
                    {
                        _log.Info($"Successfully imported file: {item.Key}");
                        await _dataService.UpdateDocumentMetadataAsync(_token, _viewModel.DocumentaryId, result.nodeId, item.Value.ToArray());
                        _log.Info($"Updated metadata for: {result.nodeId}");
                    }

                    index++;
                    _viewModel.ItemIndex++;
                }

                _viewModel.Importing = false;
            }
            catch (Exception ex)
            {
                _log.Error($"Can't import file: {filename}", ex);
                _taskAsyncStarted = false;
                SaveProcessStatus();
                MessageBox.Show("Ha ocurrido un error mientras se importaban los datos", "File Importer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _taskAsyncStarted = false;
            }

            Close();
        }

        private void SetProgressIndex(int index)
        {
            lbTransfering.Text = $"Importando archivos {index} de {_viewModel.UploadList.Count}...";
        }

        private void SaveProcessStatus()
        {
            var configMngr = new ConfigurationManager();
            var config = configMngr.OpenConfiguration();
            config.ProcessStopped = true;
            config.UserGroupId = _viewModel.CommunityId;
            config.FolderId = _viewModel.FolderId;
            config.DocumentaryTypeId = _viewModel.DocumentaryId;
            config.LastItemProcessed = _viewModel.ItemIndex;
            config.DataFields = _viewModel.UploadList;
            configMngr.SaveConfiguration(config);
        }

        public void UploadFile(string ticket)
        {
            _token = ticket;
            SetProgressIndex(_viewModel.ItemIndex);
            this.Show();
        }

        public ProgressForm()
        {
            InitializeComponent();
            _log = LogManager.GetLogger("FileImporter");
            _fileId = string.Empty;
            _dataService = DataServiceFactory.Create();
        }

        public ProgressForm(ProgressViewModel viewModel)
            : this()
        {
            _viewModel = viewModel;
        }

        private void TransferFileView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = _taskAsyncStarted;
        }
    }
}
