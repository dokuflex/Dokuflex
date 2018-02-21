using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DokuFlex.Windows.Common.Services.Data;
using DokuFlex.Windows.Common.Services;

namespace DokuFlex.Explorer
{
    public partial class DownloadProgressForm : Form
    {
        private IDataService dataService;
        private string sessionId;
        private string folderPath;
        private bool canceled = false;
        private List<FileFolder> files;
        private string communityId;

        private async Task DownloadFilesAsync()
        {
            var fileCount = files.Count();

            for (int i = 0; i < fileCount; i++)
            {
                progressLabel.Text = String.Format("Descargando {0} de {1} para \"{2}\"", i + 1, fileCount, folderPath);
                progressLabel.Refresh();

                var filePath = String.Format("{0}\\{1}", folderPath, files[i].name);
                var result = await this.dataService.DownloadAsync(this.sessionId, files[i].id, filePath);

                if (canceled || !result) break;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            await DownloadFilesAsync();
        }

        public DownloadProgressForm()
        {
            InitializeComponent();
        }

        public DownloadProgressForm(string sessionId, string communityId, string folderPath, ref List<FileFolder> files)
        {
            InitializeComponent();
            
            // TODO: Complete member initialization
            this.dataService = DataServiceFactory.Create();
            this.sessionId = sessionId;
            this.communityId = communityId;
            this.folderPath = folderPath;
            this.files = files;
        }

        private void DownloadProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
            {
                this.canceled = true;
            }
        }
    }
}
