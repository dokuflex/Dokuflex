using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DokuFlexVPrinter
{
    using System.IO;
    using Ghostscript.NET;
    using Ghostscript.NET.Processor;
    using DokuFlex.Windows.Common;
    using DokuFlex.Windows.Common.Services;
    using DokuFlex.Windows.Common.Services.Data;
    using DokuFlex.WinForms.Common;

    public partial class MainForm : Form
    {
        private string _ticket;
        private UserGroup _community;
        private FileFolder _folder;
        private IDataService _dataService;
        private string _bioSignature;
        private string _inputFile;
        private string _outputFile;
        private Certificate _certificate;

        public bool ConvertPsToPdf(string inputFile, string outputFile)
        {
            GhostscriptVersionInfo gv = GhostscriptVersionInfo.GetLastInstalledVersion();

            try
            {
                using (GhostscriptProcessor processor = new GhostscriptProcessor(gv, true))
                {
                    processor.Processing += new GhostscriptProcessorProcessingEventHandler(processor_Processing);

                    List<string> switches = new List<string>();
                    switches.Add("-empty");
                    switches.Add("-dQUIET");
                    switches.Add("-dSAFER");
                    switches.Add("-dBATCH");
                    switches.Add("-dNOPAUSE");
                    switches.Add("-dNOPROMPT");
                    switches.Add("-sDEVICE=pdfwrite");
                    switches.Add("-dCompatibilityLevel=1.4");
                    switches.Add("-sOutputFile=" + outputFile);
                    switches.Add("-c");
                    switches.Add("-f");
                    switches.Add(inputFile.ToString());

                    processor.StartProcessing(switches.ToArray(), null);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private void processor_Processing(object sender, GhostscriptProcessorProcessingEventArgs e)
        {
            progressBar.Value = e.CurrentPage;
        }

        private bool HasErrors()
        {
            var hasError = false;

            if (String.IsNullOrWhiteSpace(cbxSignatureType.Text))
            {
                errorProvider.SetError(cbxSignatureType, "El proveedor de firma no es valido");
                hasError = true;
            }

            if (String.IsNullOrWhiteSpace(textPath.Text))
            {
                errorProvider.SetError(textPath, "La ubicación de destino no es valida");
                hasError = true;
            }         

            if (cbxSignatureType.SelectedIndex == 0 && 
                cbxCertificates.SelectedIndex == 0)
            {
                errorProvider.SetError(cbxCertificates, "El certificado no es valido");
                hasError = true;
            }

            if (cbxSignatureType.SelectedIndex == 0 &&
                _certificate == null)
            {
                errorProvider.SetError(cbxCertificates, "El certificado no es valido");
                hasError = true;
            }

            if (cbxSignatureType.SelectedIndex == 1 &&
                String.IsNullOrWhiteSpace(_bioSignature))
            {
                errorProvider.SetError(btnBioSign, "Haga clic aquí para recolectar la firma");
                hasError = true;
            }

            return hasError;
        }

        private void ClearErrors()
        {
            errorProvider.SetError(cbxSignatureType, "");
            errorProvider.SetError(textPath, "");
            errorProvider.SetError(cbxCertificates, "");
            errorProvider.SetError(btnBioSign, "");
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.Cursor = Cursors.WaitCursor;

            Task<string> theTask = Session.GetTikectAsync();

            try
            {
                _ticket = await theTask;

                if (String.IsNullOrWhiteSpace(_ticket))
                {
                    this.Close();
                    return;
                }

                var certificateList = await _dataService.ListCertificatesAsync(_ticket);
                certificateList.Insert(0, new Certificate() { id = String.Empty, text = String.Empty});

                cbxCertificates.DataSource = new BindingList<Certificate>(certificateList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}\n\n{1}", "Ha ocurrido un error en la llamada", ex.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            _dataService = DataServiceFactory.Create();
        }

        public MainForm(string inputFile)
        {
            InitializeComponent();

            var fileName = String.Format("{0}.pdf", DateTime.Now.ToString("s").Replace(":", String.Empty));

            _inputFile = inputFile;
            _outputFile = String.Format("{0}\\{1}", System.IO.Path.GetTempPath(), fileName);
            _dataService = DataServiceFactory.Create();
        }

        private void cbxSignatureType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbxSignatureType.SelectedIndex)
            {
                case 1 :
                    btnBioSign.Enabled = true;
                    cbxCertificates.Enabled = false;
                    break;

                default:
                    btnBioSign.Enabled = false;
                    cbxCertificates.Enabled = true;
                    break;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var form = new BrowseFolderForm(_ticket))
            {
                if (form.ShowFolderBrowserDialog())
                {
                    _community = form.Group;
                    _folder = form.Folder;

                    textPath.Text = form.FullPath;
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (HasErrors()) return;

            ClearErrors();

            this.Cursor = Cursors.WaitCursor;
            progressBar.Enabled = true;
            progressBar.Visible = true;

            try
            {
                if (ConvertPsToPdf(_inputFile, _outputFile))
                {
                    progressBar.Style = ProgressBarStyle.Marquee;

                    UploadResult result = null;

                    switch (cbxSignatureType.SelectedIndex)
                    {
                        case 1:
                            result = await _dataService.UploadAsync(_ticket, _community.id, String.Empty,
                            _folder.id, String.Empty, String.Empty, false, String.Empty, "plugin",
                            false, new System.IO.FileInfo(_outputFile));
                            break;

                        default:
                            result = await _dataService.UploadAsync(_ticket, _community.id, String.Empty,
                            _folder.id, String.Empty, String.Empty, false, _certificate.id, "plugin",
                            false, new System.IO.FileInfo(_outputFile));
                            break;
                    }

                    //Delete output file from temp folder;
                    File.Delete(_outputFile);

                    if (result != null)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Ha ocurrido un error en la conversión del archivo a PDF", "DokuFlex VPrinter",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
                progressBar.Enabled = false;
                progressBar.Visible = false;
            }
        }

        private void cbxCertificates_SelectedIndexChanged(object sender, EventArgs e)
        {
            _certificate = cbxCertificates.SelectedItem as Certificate;
        }

        private void btnBioSign_Click(object sender, EventArgs e)
        {
            using (var form = new SigPlusForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _bioSignature = form.SelectedValue;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
