using System;
using System.Windows.Forms;
using DokuFlex.WinForms.Common;
using FileImporter.ViewModels;
using System.ComponentModel;
using DokuFlex.Windows.Common.Services.Data;
using DokuFlex.WinForms.Common;

namespace FileImporter
{
    public partial class MainForm : Form
    {
        private MainViewModel _viewmodel;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            UpdateButtonStatus();
        }

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(MainViewModel viewModel)
            : this()
        {
            _viewmodel = viewModel;
        }

        private void BtnBrowse_Click(object sender, System.EventArgs e)
        {
            using (var openFileDlg = new OpenFileDialog
                {
                    Title = "Abrir archivo",
                    Filter = "Archivos de texto|*.txt;*.csv"
                })
            {
                if (openFileDlg.ShowDialog() == DialogResult.OK)
                {
                    _viewmodel.FileName = openFileDlg.FileName;
                    TxtBoxFilename.Text = openFileDlg.FileName;
                }
            }
        }

        private void SetVMCredentials()
        {
            _viewmodel.UserName = textBox3.Text;
            _viewmodel.Password = textBox4.Text;
        }

        private void BtnConnection_Click(object sender, System.EventArgs e)
        {
            using (var settingsDlg = new SettingsForm())
            {
                settingsDlg.ShowDialog();
            }
        }

        private async void BtnLogin_Click(object sender, EventArgs e)
        {
            SetVMCredentials();

            var result = await _viewmodel.LoginAsync();
            if (result)
            {
                await _viewmodel.GetDocumentaryTypes();
                cbxDocumentaryTypes.DataSource = new BindingList<Documentary>(_viewmodel.DocumentaryTypes);
            }
            else
                MessageBox.Show("Los parámetros de conexión o las credenciales no son validas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            UpdateButtonStatus();
        }

        private void UpdateButtonStatus()
        {
            BtnLogin.Enabled = string.IsNullOrWhiteSpace(_viewmodel.Token);
            BtnDestinationFolder.Enabled = !string.IsNullOrWhiteSpace(_viewmodel.Token);
            cbxDocumentaryTypes.Enabled = BtnDestinationFolder.Enabled;
        }

        private void cbxDocumentaryTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            _viewmodel.DocumentaryType = cbxDocumentaryTypes.SelectedItem as Documentary;
        }

        private void BtnDestinationFolder_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDlg = new BrowseFolderForm(_viewmodel.Token))
            {
                if (folderBrowserDlg.ShowFolderBrowserDialog())
                {
                    _viewmodel.Folder = folderBrowserDlg.Folder;
                    _viewmodel.UserGroup = folderBrowserDlg.Group;
                    LblDestinationFolder.Text = folderBrowserDlg.FullPath;
                }
            }
        }

        private void BtnAccept_Click(object sender, EventArgs e)
        {
            if (_viewmodel.IsValid())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Los datos no son validos, revise la información introducida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
