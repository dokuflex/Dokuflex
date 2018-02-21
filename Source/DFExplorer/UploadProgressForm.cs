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
using DokuFlex.Windows.Common.Services;

namespace DokuFlex.Explorer
{
    public partial class UploadProgressForm : Form
    {
        private IDataService dataService;
        private bool uploadDirectory = false;
        private string ticketId;
        private string communityId;
        private string folderId;
        private string[] paths;
        private bool taskCanceled;
        private string folderName;
        private string directoryPath;


        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            progressBar.Enabled = true;

            if (this.uploadDirectory)
            {
                await UploadFolderAsync(folderId, directoryPath);
            }
            else
            {
                var filePaths = paths.Where(p => File.Exists(p)).ToArray();

                await UploadFilesAsync(folderId, folderName, filePaths);

                if (!taskCanceled)
                {
                    var dirPaths = paths.Where(p => Directory.Exists(p)).ToArray();

                    foreach (var dir in dirPaths)
                    {
                        await UploadFolderAsync(folderId, dir);

                        if (taskCanceled) break;
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private async Task UploadFolderAsync(string parentFolderId, string dirPath)
        {
            string parentDirPath = String.Empty;
            string createFolderId = String.Empty;
            DirectoryInfo dirInfo = null;
            int dirIndex = 0;
            Dictionary<string, string> createDirs = new Dictionary<string, string>();
            string[] filePaths = null;
            List<string> dirPaths = new List<string>();

            createDirs.Add(String.Empty, parentFolderId);
            dirPaths.Add(dirPath);

            while (dirIndex < dirPaths.Count)
            {
                var strIndex = dirPaths[dirIndex].LastIndexOf('\\');
                parentDirPath = dirPaths[dirIndex].Remove(strIndex);
                dirInfo = new DirectoryInfo(dirPaths[dirIndex]);

                progessLabel.Text = String.Format("Creando carpeta \"{0}\"...", dirInfo.Name);
                progessLabel.Refresh();

                if (createDirs.ContainsKey(parentDirPath))
                {
                    string folderId = String.Empty;
                    if (createDirs.TryGetValue(parentDirPath, out folderId))
                    {
                        createFolderId = await this.dataService.CreateFolderAsync(this.ticketId, this.communityId, folderId, dirInfo.Name);
                    }                   
                }
                else
                {
                    createFolderId = await this.dataService.CreateFolderAsync(this.ticketId, this.communityId, parentFolderId, dirInfo.Name);
                }

                createDirs.Add(dirPaths[dirIndex], createFolderId);

                if (this.taskCanceled) break;

                filePaths = Directory.EnumerateFiles(dirPaths[dirIndex], "*", SearchOption.TopDirectoryOnly).ToArray();
                dirPaths.AddRange(Directory.EnumerateDirectories(dirPaths[dirIndex], "*", SearchOption.TopDirectoryOnly));
                
                await UploadFilesAsync(createFolderId, dirInfo.Name, filePaths);

                dirIndex++;

                if (this.taskCanceled) break;
            }

            //foreach (var dir in dirPaths)
            //{

            //    dirInfo = new DirectoryInfo(dir);

            //    progessLabel.Text = String.Format("Creando carpeta \"{0}\"...", dirInfo.Name);
            //    progessLabel.Refresh();
                
            //    createFolderId = await this.dataService.CreateFolderAsync(ticketId, communityId, parentFolderId, dirInfo.Name);

            //    if (taskCanceled) break;
                
            //    filePaths = Directory.EnumerateFiles(dir, "*", SearchOption.TopDirectoryOnly).ToArray();

            //    await UploadFilesAsync(createFolderId, dirInfo.Name, filePaths);

            //    if (taskCanceled) break;
            //}
        }

        private async Task UploadFilesAsync(string parentFolderId, string parentFolderName, params string[] filePaths)
        {
            var fileCount = filePaths.Count();

            for (int i = 0; i < fileCount; i++)
            {
                progessLabel.Text = String.Format("Copiando archivos {0} de {1} para \"{2}\"", i + 1, fileCount, parentFolderName);
                progessLabel.Refresh();

                var fileId = await this.dataService.UploadAsync(ticketId, communityId, String.Empty,
                    parentFolderId, String.Empty, String.Empty, true, String.Empty, "plugin", false,
                    new FileInfo(filePaths[i]));

                if (taskCanceled) break;
            }
        }

        public UploadProgressForm()
        {
            InitializeComponent();
        }

        public UploadProgressForm(string ticketId, string communityId,
            string folderId, string folderName, string[] paths)
        {
            InitializeComponent();

            this.uploadDirectory = false;
            this.dataService = DataServiceFactory.Create();
            this.ticketId = ticketId;
            this.communityId = communityId;
            this.folderId = folderId;
            this.folderName = folderName;
            this.paths = paths;
            this.taskCanceled = false;
        }

        public UploadProgressForm(string ticketId, string communityId,
            string folderId, string folderName, string directoryPath)
        {
            InitializeComponent();

            this.uploadDirectory = true;
            this.dataService = DataServiceFactory.Create();
            this.ticketId = ticketId;
            this.communityId = communityId;
            this.folderId = folderId;
            this.folderName = folderName;
            this.directoryPath = directoryPath;
            this.paths = null;
            this.taskCanceled = false;
        }

        private void UploadProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
            {
                this.taskCanceled = true;
            }
        }
    }
}
