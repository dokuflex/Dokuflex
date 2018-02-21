using System.Windows.Forms;
using DokuFlex.WinForms.Common;
using FileImporter.ViewModels;
using log4net;

namespace FileImporter
{
    public class MyApplicationContext : ApplicationContext
    {
        private int _formCount;
        private readonly MainViewModel _mainViewModel;
        private AdvancedViewModel _advViewModel;
        private ProgressViewModel _progressViewModel;
        private readonly ILog _log;
        private readonly ConfigurationManager _configMgr = new ConfigurationManager();

        public MyApplicationContext()
        {
            Application.ThreadExit += Application_ThreadExit;
            Application.ThreadException += Application_ThreadException;
            _log = LogManager.GetLogger("FileImporter");
            _log.Info("Application started");
            _mainViewModel = new MainViewModel();
            _formCount++;

            var config = _configMgr.OpenConfiguration();

            if (config.ProcessStopped)
            {
                if (MessageBox.Show("Hay archivos pendientes de importar. ¿Desea continuar con la importación?", "FileImporter", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var token = Session.GetTikect();

                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        _progressViewModel = new ProgressViewModel
                        {
                            CommunityId = config.UserGroupId,
                            DocumentaryId = config.DocumentaryTypeId,
                            FolderId = config.FolderId,
                            ItemIndex = config.LastItemProcessed,
                            UploadList = config.DataFields
                        };
                        var progressFrom = new ProgressForm(_progressViewModel);
                        progressFrom.FormClosed += FormClosed;
                        progressFrom.UploadFile(token);
                    }
                }
                else
                    ShowMainForm();
            }
            else
                ShowMainForm();
        }

        private void Application_ThreadExit(object sender, System.EventArgs e)
        {
            if (_progressViewModel != null && _progressViewModel.Importing)
            {
                var configMngr = new ConfigurationManager();
                var config = configMngr.OpenConfiguration();
                config.ProcessStopped = true;
                config.UserGroupId = _progressViewModel.CommunityId;
                config.FolderId = _progressViewModel.FolderId;
                config.DocumentaryTypeId = _progressViewModel.DocumentaryId;
                config.LastItemProcessed = _progressViewModel.ItemIndex;
                config.DataFields = _progressViewModel.UploadList;
                configMngr.SaveConfiguration(config);
            }
        }

        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (_progressViewModel != null && _progressViewModel.Importing)
            {
                var configMngr = new ConfigurationManager();
                var config = configMngr.OpenConfiguration();
                config.ProcessStopped = true;
                config.UserGroupId = _progressViewModel.CommunityId;
                config.FolderId = _progressViewModel.FolderId;
                config.DocumentaryTypeId = _progressViewModel.DocumentaryId;
                config.LastItemProcessed = _progressViewModel.ItemIndex;
                config.DataFields = _progressViewModel.UploadList;
                configMngr.SaveConfiguration(config);
            }

            _log.Error(e.Exception.Message, e.Exception);
        }

        private void ShowMainForm()
        {
            var mainForm = new MainForm(_mainViewModel);
            mainForm.FormClosed += FormClosed;
            mainForm.Show();
        }

        private void FormClosed(object sender, FormClosedEventArgs e)
        {
            _formCount--;

            if (sender is MainForm && ((Form)sender).DialogResult == DialogResult.OK)
            {
                _formCount++;
                _advViewModel = new AdvancedViewModel(_mainViewModel.DocumentaryType?.elements, _mainViewModel.FileName);
                var advForm = new AdvancedForm(_advViewModel);
                advForm.FormClosed += FormClosed;
                advForm.Show();
            }

            if (sender is AdvancedForm && ((Form)sender).DialogResult == DialogResult.OK)
            {
                _formCount++;
                _progressViewModel = new ProgressViewModel
                {
                    CommunityId = _mainViewModel.UserGroup.id,
                    FolderId = _mainViewModel.Folder.id,
                    DocumentaryId = _mainViewModel.DocumentaryType.id,
                    UploadList = _advViewModel.GetUploadList()
                };
                var progressFrom = new ProgressForm(_progressViewModel);
                progressFrom.FormClosed += FormClosed;
                progressFrom.UploadFile(_mainViewModel.Token);
                progressFrom.Show();
            }

            if (_formCount == 0)
            {
                _log.Info("Application exit");
                ExitThread();
            }
        }
    }
}
