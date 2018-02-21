using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DokuFlex.Windows.Common.Services;
using DokuFlex.Windows.Common.Services.Data;
using DokuFlex.WinForms.Common;
using System.IO;
using DokuFlex.Windows.Common;

namespace DokuSign
{
    public partial class MainForm : Form
    {
        private string _psFilename;
        private string _outputPdf;
        private string _documentDir;
        private IDigitalSignature _digitalSignature;
        private UserGroup _community;
        private FileFolder _folder;
        private string _ticket;
        private IDataService _dataService;
        private int _signatureIndex = 0;

        public List<SearchUserResult> SelectedUsers { get; set; }

        public bool HasErrors()
        {
            var result = false;

            if (String.IsNullOrWhiteSpace(folderPath.Text))
            {
                errorProvider.SetError(folderPath, ErrorMessages.DestinationFolderNoValidError);
                result = true;
            }

            return result;
        }

        public void ClearErrors()
        {
            errorProvider.SetError(folderPath, String.Empty);

            if (_digitalSignature != null)
                _digitalSignature.ClearErrors();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SelectedUsers = new List<SearchUserResult>();
            int.TryParse(ConfigurationManager.GetValue(Constants.SignatureType), out _signatureIndex);

            switch (_signatureIndex)
            {
                case 0:
                    this.Size = new Size(770, 500);
                    sigLocal1.BringToFront();
                    sigLocal1.LoadInformation(_outputPdf, _documentDir);
                    _digitalSignature = sigLocal1 as IDigitalSignature;
                    break;

                case 1:
                    this.Size = new Size(580, 360);
                    sigOnline1.BringToFront();
                    await sigOnline1.LoadInformation(_outputPdf);
                    _digitalSignature = sigOnline1 as IDigitalSignature;
                    break;

                case 2:
                    sigBiometric1.BringToFront();
                    sigBiometric1.LoadInformation(_outputPdf, _documentDir);
                    _digitalSignature = sigBiometric1 as IDigitalSignature;

                    break;

                default:
                    break;
            }

            _folder = new FileFolder
            {
                id = ConfigurationManager.GetValue("DefaultFolderId"),
                name = ConfigurationManager.GetValue("DefaultFolderName")
            };

            folderPath.Text = ConfigurationManager.GetValue("DefaultFolderPath");
        }

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string psFilename, string outputPdf, string documentDirectory)
        {
            InitializeComponent();

            this._psFilename = psFilename;
            this._outputPdf = outputPdf;
            this._documentDir = documentDirectory;
            this._dataService = DataServiceFactory.Create();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            ClearErrors();

            if (HasErrors()) return;

            StartNewProgressInfo(StringResources.SigningDocument);
            this.Cursor = Cursors.WaitCursor;
            bool signed = true;
            try
            {
                _digitalSignature.SignDocument();

                StartNewProgressInfo(StringResources.UploadingSignedFiles);

                _ticket = await Session.GetTikectAsync();
                UploadResult result = null;
                switch (_signatureIndex)
                {
                    case 0 :

                        result = await _dataService.UploadAsync(_ticket, _community.id, String.Empty,
                            _folder.id, String.Empty, String.Empty, _digitalSignature.DocumentId, false, String.Empty,
                            String.Empty,"DokuSignPrinterLocalCertificate", false, new System.IO.FileInfo(_digitalSignature.DocumentSigned));
                        break;

                    case 1 :
                        result = await _dataService.UploadAsync(_ticket, _community.id, String.Empty,
                            _folder.id, String.Empty, String.Empty, _digitalSignature.DocumentId, false,
                            _digitalSignature.BiometricSignature, sigOnline1.CertificatePass, "DokuSignPrinterSystemCertificate",
                            false, new System.IO.FileInfo(_digitalSignature.DocumentSigned));
                        break;

                    case 2 :
                        result = await _dataService.UploadAsync(_ticket, _community.id, String.Empty,
                            _folder.id, String.Empty, String.Empty, _digitalSignature.DocumentId, false,
                            String.Empty, String.Empty, "DokuSignPrinterBiometric", false,
                            new System.IO.FileInfo(_digitalSignature.DocumentSigned));

                        var sigPositions = _digitalSignature.GetSignaturePositions();
                        var sigPosition = (SignaturePosition)null;
                        var sigPositionsArgs = new List<DokuFlex.Windows.Common.Services.Data.SignaturePosition>(sigPositions.Count);

                        foreach (int key in sigPositions.Keys)
                        {
                            if (sigPositions.TryGetValue(key, out sigPosition))
                            {
                                sigPositionsArgs.Add(new DokuFlex.Windows.Common.Services.Data.SignaturePosition()
                                {
                                    page = key,
                                    x = sigPosition.SigPosX,
                                    y = sigPosition.SigPosY
                                });
                            }
                        }

                        await _dataService.AddImageSignatureAsync(new FileInfo(_digitalSignature.BiometricSignature),
                            new FileInfo(_digitalSignature.SignatureImage), _ticket, result.nodeId, sigPositionsArgs.ToArray());
                        break;

                    default:
                        break;
                }

                if (SelectedUsers.Count > 0)
                {
                    await _dataService.RequestDocumentSignature(_ticket, result.nodeId, SelectedUsers.Select(u => u.id).ToList());
                }

                try
                {
                    Directory.Delete(_documentDir, true);
                }
                catch (Exception ex)
                {
                    DokuFlex.Windows.Common.Log.LogFactory.CreateLog().LogError(ex);
                    //Silent exception
                }

                ConfigurationManager.Save(); //Save configuration changes.
            }
            catch (Exception ex)
            {
                DokuFlex.Windows.Common.Log.LogFactory.CreateLog().LogError(ex);
                signed = false;
            }
            finally
            {
                HideProgressInfo();
                this.Cursor = Cursors.Default;
            }
            if (signed)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void HideProgressInfo()
        {
            progressBar.Visible = false;
            progressBar.Enabled = false;
            progresslabel.Visible = false;
        }

        private void StartNewProgressInfo(string progressText)
        {
            progressBar.Visible = true;
            progressBar.Enabled = true;
            progresslabel.Text = progressText;
            progresslabel.Visible = true;
        }

        private async void btnBrowse_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                _ticket = await Session.GetTikectAsync();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            using (var form = new BrowseFolderForm(_ticket))
            {
                if (form.ShowFolderBrowserDialog())
                {
                    _community = form.Group;
                    _folder = form.Folder;

                    folderPath.Text = form.FullPath;

                    ConfigurationManager.SetValue("DefaultFolderId", form.Folder.id);
                    ConfigurationManager.SetValue("DefaultFolderName", form.Folder.name);
                    ConfigurationManager.SetValue("DefaultFolderPath", form.FullPath);

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void addSignerButton_Click(object sender, EventArgs e)
        {
            UserSearchForm form = new UserSearchForm();
            form.dataService = _dataService;
            form.SelectedUsers = SelectedUsers;
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                SelectedUsers = form.SelectedUsers;
                sendToSignText.Text = String.Join(";", SelectedUsers.Select(u => u.fullName));
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (_signatureIndex == 2 && this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
    }
}
