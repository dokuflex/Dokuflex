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
using DokuFlex.Windows.Common;
using DokuFlex.Windows.Common.Services;
using DokuFlex.Windows.Common.Services.Data;
using DokuFlex.WinForms.Common;
using DokuFlex.WinForms.Common.Resources;
using DokuFlex.Windows.Common.Helpers;
using System.Diagnostics;

namespace DokuFlex.Explorer
{
    public partial class MainForm : Form
    {
        private IDataService dataSvc;
        private List<FileExtension> extensions;
        private SourceSelection souceSelection = SourceSelection.None;
        private UserGroup currentCommunity;
        private DokuFolderInfo currentFolder;
        private DokuFileInfo currentFile;
        private ListViewItem currentListViewItem;

        private TreeNode targetNode;

        private void InitializeControls()
        {

            helpToolStripMenuItem.Image = ImageResources.HelpSmall;
            getHelpToolStripMenuItem.Image = ImageResources.HelpSmall;
            aboutToolStripMenuItem.Image = ImageResources.InfoSmall;

            toolStripButtonNewFolder.Image = ImageResources.FolderNewSmall;
            toolStripSplitButtonUpload.Image = ImageResources.FolderUploadSmall;
            toolStripSplitButtonView.Image = ImageResources.view_details;
            toolStripButtonDownload.Image = ImageResources.DownloadSmall;
            toolStripButtonRename.Image = ImageResources.RenameSmall;
            toolStripButtonDelete.Image = ImageResources.DeleteSmall;

            treeViewImageList.Images.Add(ImageResources.Folder);
            treeViewImageList.Images.Add(ImageResources.FolderUpdate);
            listViewImageListSmall.Images.Add(ImageResources.Folder);
            listViewImageListLarge.Images.Add(ImageResources.folderLarge);
            listViewImageListSmall.Images.Add(ImageResources.file_extension_doc_small);
            listViewImageListLarge.Images.Add(ImageResources.file_extension_doc_large);
            listViewImageListSmall.Images.Add(ImageResources.file_extension_xls_small);
            listViewImageListLarge.Images.Add(ImageResources.file_extension_xls_large);
            listViewImageListSmall.Images.Add(ImageResources.file_extension_pps_small);
            listViewImageListLarge.Images.Add(ImageResources.file_extension_pps_large);
            listViewImageListSmall.Images.Add(ImageResources.file_extension_pdf_small);
            listViewImageListLarge.Images.Add(ImageResources.file_extension_pdf_large);
            listViewImageListSmall.Images.Add(ImageResources.file_extension_jpg_small);
            listViewImageListLarge.Images.Add(ImageResources.file_extension_jpg_large);
            listViewImageListSmall.Images.Add(ImageResources.file_extension_jpeg_small);
            listViewImageListLarge.Images.Add(ImageResources.file_extension_jpeg_large);
            listViewImageListSmall.Images.Add(ImageResources.file_extension_rtf_small);
            listViewImageListLarge.Images.Add(ImageResources.file_extension_rtf_large);
            listViewImageListSmall.Images.Add(ImageResources.file_extension_tif_small);
            listViewImageListLarge.Images.Add(ImageResources.file_extension_tif_large);
            listViewImageListSmall.Images.Add(ImageResources.file_extension_zip_small);
            listViewImageListLarge.Images.Add(ImageResources.file_extension_zip_large);
            listViewImageListSmall.Images.Add(ImageResources.file_extension_rar_small);
            listViewImageListLarge.Images.Add(ImageResources.file_extension_rar_large);
            listViewImageListSmall.Images.Add(ImageResources.file_extension_tmp_small);
            listViewImageListLarge.Images.Add(ImageResources.file_extension_tmp_large);


            this.extensions.Add(new FileExtension()
            {
                DisplayName = "Microsoft Word",
                Extension = ".doc;.docx",
                ImageIndex = 1
            });
            this.extensions.Add(new FileExtension()
            {
                DisplayName = "Microsoft Excel",
                Extension = ".xls;.xlsx",
                ImageIndex = 2
            });
            this.extensions.Add(new FileExtension()
            {
                DisplayName = "Microsoft PowerPoint",
                Extension = ".ppt;.pptx",
                ImageIndex = 3
            });
            this.extensions.Add(new FileExtension()
            {
                DisplayName = "Acrobat Reader",
                Extension = ".pdf",
                ImageIndex = 4
            });
            this.extensions.Add(new FileExtension()
            {
                DisplayName = "Imagen JPG",
                Extension = ".jpg",
                ImageIndex = 5
            });
            this.extensions.Add(new FileExtension()
            {
                DisplayName = "Imagen JPEG",
                Extension = ".jpeg",
                ImageIndex = 6
            });
            this.extensions.Add(new FileExtension()
            {
                DisplayName = "RTF",
                Extension = ".rtf",
                ImageIndex = 7
            });
            this.extensions.Add(new FileExtension()
            {
                DisplayName = "Imagen JPG",
                Extension = ".tif",
                ImageIndex = 8
            });
            this.extensions.Add(new FileExtension()
            {
                DisplayName = "Comprimido (zip)",
                Extension = ".zip",
                ImageIndex = 9
            });
            this.extensions.Add(new FileExtension()
            {
                DisplayName = "Archivo rar",
                Extension = ".rar",
                ImageIndex = 10
            });
            this.extensions.Add(new FileExtension()
            {
                DisplayName = "",
                Extension = "",
                ImageIndex = 11
            });
        }

        private async Task<string> GetTicketAsync()
        {
            this.Cursor = Cursors.WaitCursor;
            ShowProgress();

            try
            {
                return await Session.GetTikectAsync();
            }
            finally
            {
                this.Cursor = Cursors.Default;
                HideProgress();
            }
        }


        private async Task<string> CreateFolderAsync(string communityId, string parentFolderId, string text)
        {
            this.Cursor = Cursors.WaitCursor;
            ShowProgress();
            //bool operationExecuted = true;
            try
            {
                var sessionId = await Session.GetTikectAsync();

                if (String.IsNullOrWhiteSpace(sessionId)) return string.Empty;

                return await this.dataSvc.CreateFolderAsync(sessionId, currentCommunity.id, String.Empty, text);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                HideProgress();
            }

            //if (!operationExecuted)
            //{
            //    //Refrescar el arbol
            //    //return await DisplayCommunitiesAsync();
            //}
        }

        private async Task RenameFileFolderAsync(string communityId, string parentFolderId, string text)
        {
            this.Cursor = Cursors.WaitCursor;
            ShowProgress();

            try
            {
                var sessionId = await Session.GetTikectAsync();

                if (String.IsNullOrWhiteSpace(sessionId)) return;

                await this.dataSvc.RenameFileFolderAsync(sessionId, communityId, parentFolderId, text);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                HideProgress();
            }
        }

        private async Task<List<DokuField>> GetDocumentMetadadaAsync(string fileId)
        {
            var sessionId = await Session.GetTikectAsync();

            if (String.IsNullOrWhiteSpace(sessionId)) return new List<DokuField>();

            var metadataResponse = await this.dataSvc.GetDocumentMetadadaAsync(sessionId, fileId);

            return metadataResponse.elements;
        }


        private async Task DisplayCommunitiesAsync()
        {
            refreshButton.Enabled = refreshTreeItem.Enabled = false;
            var sessionId = await Session.GetTikectAsync();

            if (String.IsNullOrWhiteSpace(sessionId)) return;

            var communities = await this.dataSvc.GetUserGroupsAsync(sessionId);

            treeView.SuspendLayout();
            treeView.Nodes.Clear();

            if (communities.Count > 0)
            {
                foreach (var community in communities)
                {
                    var dokuUserGroup = new DokuUserGroupInfo() { UserGroup = community };
                    var node = new TreeNode()
                    {
                        Text = community.name,
                        ImageIndex = 0,
                        Tag = dokuUserGroup,
                        ContextMenuStrip = this.treeViewContextMenuStrip
                    };

                    dokuUserGroup.Node = node;
                    treeView.Nodes.Add(node);
                }

                //Add fake folders as indicators
                foreach (TreeNode node in treeView.Nodes)
                {
                    node.Nodes.Add(
                        new TreeNode()
                        {
                            Text = "Cargando carpetas...",
                            ImageIndex = 1,
                            SelectedImageIndex = 1,
                            Tag = new FakeFolderObject()
                        }
                        );
                }

                treeView.TopNode = treeView.Nodes[0];
                treeView.SelectedNode = treeView.Nodes[0];
            }

            treeView.ResumeLayout();

            refreshButton.Enabled = refreshTreeItem.Enabled = true;

            //cbxCommunities.DataSource = new BindingList<UserGroup>(communities);

            //if (communities.Count > 0)
            //{
            //    cbxCommunities.SelectedIndex = 0;
            //}
        }

        private async Task DisplayFilesAndFoldersAsync(TreeNode node)
        {
            var sessionId = await Session.GetTikectAsync();

            if (String.IsNullOrWhiteSpace(sessionId)) return;


            if (node.Tag is DokuUserGroupInfo)
            {
                var dokuCommunity = node.Tag as DokuUserGroupInfo;
                dokuCommunity.Files = new List<DokuFileInfo>();
                dokuCommunity.Folders = new List<DokuFolderInfo>();

                var filesAndFolders = await this.dataSvc.GetFilesFoldersAsync(sessionId, this.currentCommunity.id, String.Empty);

                treeView.SuspendLayout();

                node.Nodes.Clear(); //Remove fake folder indicator. :)

                if (filesAndFolders.Count > 0)
                {
                    foreach (var fileFolder in filesAndFolders)
                    {
                        if (fileFolder.type == "F")
                        {
                            var folder = new DokuFolderInfo() { FileFolder = fileFolder };
                            var childNode = new TreeNode()
                            {
                                Text = fileFolder.name,
                                ImageIndex = 0,
                                Tag = folder,
                                ContextMenuStrip = this.treeViewContextMenuStrip
                            };

                            folder.Node = childNode;
                            dokuCommunity.Folders.Add(folder);
                            node.Nodes.Add(childNode);
                        }
                        else
                        {
                            dokuCommunity.Files.Add(new DokuFileInfo() { FileFolder = fileFolder });
                        }
                    }

                    //Add fake folders to child nodes...
                    foreach (TreeNode childNode in node.Nodes)
                    {
                        childNode.Nodes.Add(
                            new TreeNode()
                            {
                                Text = "Cargando carpetas...",
                                ImageIndex = 1,
                                Tag = new FakeFolderObject()
                            }
                            );
                    }
                }

                treeView.ResumeLayout();

            }
            else
            {
                var dokuFolder = node.Tag as DokuFolderInfo;
                dokuFolder.Files = new List<DokuFileInfo>();
                dokuFolder.Folders = new List<DokuFolderInfo>();

                //Buscar la comunidad correspondiente, sera la raiz de la rama
                TreeNode userGroupNode = node.Parent;
                while (!(userGroupNode.Tag is DokuUserGroupInfo))
                {
                    userGroupNode = userGroupNode.Parent;
                }
                var dokuCommunity = userGroupNode.Tag as DokuUserGroupInfo;

                var filesAndFolders = await this.dataSvc.GetFilesFoldersAsync(sessionId, dokuCommunity.UserGroup.id, dokuFolder.FileFolder.id);

                treeView.SuspendLayout();

                node.Nodes.Clear(); //Remove fake folder indicator. :)

                if (filesAndFolders.Count > 0)
                {
                    foreach (var fileFolder in filesAndFolders)
                    {
                        if (fileFolder.type == "F")
                        {
                            var folder = new DokuFolderInfo() { FileFolder = fileFolder };
                            var childNode = new TreeNode()
                            {
                                Text = fileFolder.name,
                                ImageIndex = 0,
                                Tag = folder,
                                ContextMenuStrip = this.treeViewContextMenuStrip
                            };

                            folder.Node = childNode;
                            dokuFolder.Folders.Add(folder);
                            node.Nodes.Add(childNode);
                        }
                        else
                        {
                            dokuFolder.Files.Add(new DokuFileInfo() { FileFolder = fileFolder });
                        }
                    }

                    //Add fake folders to child nodes...
                    foreach (TreeNode childNode in node.Nodes)
                    {
                        childNode.Nodes.Add(
                            new TreeNode()
                            {
                                Text = "Cargando carpetas...",
                                ImageIndex = 1,
                                Tag = new FakeFolderObject()
                            }
                            );
                    }
                }

                treeView.ResumeLayout();
            }


        }

        private void DisplayFilesAndFoldersOnListView(TreeNode node)
        {
            listView.Items.Clear();

            if (node == null || !(node.Tag is DokuNodeInfo))
            {
                return;
            }

            var dokuFolder = node.Tag as DokuNodeInfo;

            listView.SuspendLayout();

            if (dokuFolder.Folders != null)
            {
                foreach (var folder in dokuFolder.Folders)
                {
                    var dateModified = DateTimeHelper.FromUnixEpoch(folder.FileFolder.modifiedTime);

                    var listViewItem = listView.Items.Add(new ListViewItem()
                    {
                        Text = folder.FileFolder.name,
                        ImageIndex = 0,
                        Tag = folder,
                        Group = listView.Groups[0]
                    }
                    );

                    listViewItem.SubItems.Add(dateModified.ToShortDateString());
                    listViewItem.SubItems.Add("Carpeta");
                }
            }


            if (dokuFolder.Files != null)
            {
                foreach (var file in dokuFolder.Files)
                {
                    var imageIndex = 11;
                    var extension = String.Empty;
                    var fileType = String.Empty;
                    var dateModified = DateTimeHelper.FromUnixEpoch(file.FileFolder.modifiedTime);

                    extension = Path.GetExtension(file.FileFolder.name);
                    var currentExtension = this.extensions.FirstOrDefault(e => e.Extension.Contains(extension));

                    if (currentExtension != null)
                    {
                        imageIndex = currentExtension.ImageIndex;
                        fileType = currentExtension.DisplayName;
                    }

                    var size = file.FileFolder.size / 1024;

                    var listViewItem = listView.Items.Add(new ListViewItem()
                    {
                        Text = file.FileFolder.name,
                        ImageIndex = imageIndex,
                        Tag = file,
                        Group = listView.Groups[1]
                    }
                    );

                    listViewItem.SubItems.Add(dateModified.ToShortDateString());
                    listViewItem.SubItems.Add(fileType);
                    listViewItem.SubItems.Add(String.Format("{0} KB", size.ToString()));
                    listViewItem.SubItems.Add(file.FileFolder.version);
                }
            }

            listView.ResumeLayout();
        }

        private async Task OpenFileAsync()
        {
            var tempPath = DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.DownloadDirectory);

            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }

            tempPath = String.Format("{0}\\{1}", tempPath, this.currentFile.FileFolder.name);

            if (File.Exists(tempPath))
            {
                System.Diagnostics.Process.Start(tempPath);
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                ShowProgress();

                try
                {
                    var sessionId = await Session.GetTikectAsync();

                    if (String.IsNullOrWhiteSpace(sessionId)) return;

                    using (var form = new TransferProgressForm())
                    {
                        if (form.DownloadFile(sessionId, this.currentFile.FileFolder.id, tempPath))
                        {
                            System.Diagnostics.Process.Start(tempPath);
                        }
                    }
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    HideProgress();
                }
            }
        }

        private void UpdateControlsState()
        {
            var elementCount = 0;
            var fullPath = treeView.SelectedNode == null ? "(ninguno)" : treeView.SelectedNode.FullPath;

            if (this.treeView.SelectedNode != null)
            {
                if (treeView.SelectedNode.Tag is DokuFolderInfo)
                {
                    if (this.currentFolder.Folders != null)
                    {
                        elementCount += this.currentFolder.Folders.Count;
                    }

                    if (this.currentFolder.Files != null)
                    {
                        elementCount += this.currentFolder.Files.Count;
                    }
                }
                else
                {
                    DokuUserGroupInfo groupInfo = (DokuUserGroupInfo)treeView.SelectedNode.Tag;
                    if (groupInfo.Folders != null)
                    {
                        elementCount += groupInfo.Folders.Count;
                    }

                    if (groupInfo.Files != null)
                    {
                        elementCount += groupInfo.Files.Count;
                    }
                }
            }

            toolStripStatusLabel1.Text = String.Format("   {0} elementos", elementCount);

            newFolderToolStripMenuItem.Enabled = newFolderToolStripMenuItem1.Enabled = this.currentCommunity != null;
            toolStripStatusLabel3.Text = fullPath;
            newFolderToolStripMenuItem2.Enabled = this.currentCommunity != null;
            toolStripButtonNewFolder.Enabled = this.currentCommunity != null;
            deleteToolStripMenuItem.Enabled = this.currentFile != null || this.currentFolder != null;
            deleteToolStripMenuItem1.Enabled = deleteToolStripMenuItem2.Enabled = deleteToolStripMenuItem.Enabled;
            toolStripButtonDelete.Enabled = deleteToolStripMenuItem.Enabled;
            renameToolStripMenuItem.Enabled = this.currentFile != null || this.currentFolder != null;
            renameToolStripMenuItem1.Enabled = renameToolStripMenuItem.Enabled && this.souceSelection == SourceSelection.Tree;
            renameToolStripMenuItem2.Enabled = renameToolStripMenuItem.Enabled && this.souceSelection == SourceSelection.List;
            toolStripButtonRename.Enabled = renameToolStripMenuItem.Enabled;
            addFilesToolStripMenuItem.Enabled = uploadFilesToolStripMenuItem.Enabled = (currentCommunity != null || currentFolder != null);
            addFolderToolStripMenuItem.Enabled = uploadFolderToolStripMenuItem.Enabled = (currentCommunity != null || currentFolder != null);
            downloadToolStripMenuItem.Enabled = downloadToolStripMenuItem1.Enabled = this.currentFile != null || this.currentFolder != null;
            toolStripButtonDownload.Enabled = this.currentFile != null || this.currentFolder != null;
            openToolStripMenuItem.Enabled = openToolStripMenuItem1.Enabled = this.currentFile != null;
            editMetadataToolStripMenuItem.Enabled = metadataToolStripMenuItem1.Enabled = this.currentFile != null;
            syncFolderToolStripMenuItem.Enabled = syncFolderToolStripMenuItem1.Enabled = this.currentFolder != null;
            syncFolderToolStripMenuItem2.Enabled = syncFolderToolStripMenuItem.Enabled;
        }

        private void ClearMetadata()
        {
            metadataControl.BindMetadata(null);
        }

        private void DisplayMetadata(DokuFileInfo dokuFileInfo)
        {
            metadataControl.BindMetadata(dokuFileInfo.Metadata);
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            treeView.HideSelection = false;
            listView.HideSelection = false;

            UpdateControlsState();

            await DisplayCommunitiesAsync();
        }

        public MainForm()
        {
            InitializeComponent();

            this.dataSvc = DataServiceFactory.Create();
            this.extensions = new List<FileExtension>();

            InitializeControls();
        }

        private async void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.FirstNode != null && e.Node.FirstNode.Tag is FakeFolderObject)
            {
                this.Cursor = Cursors.WaitCursor;
                ShowProgress();

                try
                {
                    if (e.Node.Tag.GetType() == typeof(DokuUserGroupInfo))
                    {
                        this.currentCommunity = (e.Node.Tag as DokuUserGroupInfo).UserGroup;
                        if (this.currentCommunity == null) return;
                    }
                    await DisplayFilesAndFoldersAsync(e.Node);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    HideProgress();
                }
            }

            this.currentFile = null;
            if (e.Node.Tag is DokuFolderInfo)
            {
                this.currentFolder = e.Node.Tag as DokuFolderInfo;
            }
            this.souceSelection = SourceSelection.Tree;

            DisplayFilesAndFoldersOnListView(e.Node);
            UpdateControlsState();
        }

        private async void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.FirstNode != null && e.Node.FirstNode.Tag is FakeFolderObject)
            {
                this.Cursor = Cursors.WaitCursor;
                ShowProgress();

                try
                {
                    if (e.Node.Tag.GetType() == typeof(DokuUserGroupInfo))
                    {
                        this.currentCommunity = (e.Node.Tag as DokuUserGroupInfo).UserGroup;
                        if (this.currentCommunity == null) return;
                    }
                    await DisplayFilesAndFoldersAsync(e.Node);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    HideProgress();
                }
            }

            this.currentFile = null;
            this.currentFolder = e.Node.Tag as DokuFolderInfo;

            UpdateControlsState();
        }

        private async void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                if (e.Item.Tag == null)
                {
                    return;
                }

                if (e.Item.Tag is DokuFolderInfo)
                {
                    this.currentFile = null;
                    this.currentFolder = e.Item.Tag as DokuFolderInfo;

                    ClearMetadata();
                }
                else
                {
                    this.currentFile = e.Item.Tag as DokuFileInfo;
                    this.currentFolder = null;

                    if (this.currentFile.Metadata.Count == 0)
                    {
                        this.Cursor = Cursors.WaitCursor;
                        ShowProgress();

                        try
                        {
                            this.currentFile.Metadata.AddRange(await GetDocumentMetadadaAsync(this.currentFile.FileFolder.id));
                        }
                        finally
                        {
                            this.Cursor = Cursors.Default;
                            HideProgress();
                        }
                    }

                    DisplayMetadata(this.currentFile);
                }
            }
            else
            {
                this.currentFile = null;
                this.currentFolder = treeView.SelectedNode == null ? null : treeView.SelectedNode.Tag as DokuFolderInfo;
            }

            this.souceSelection = SourceSelection.List;
            this.currentListViewItem = e.Item;

            UpdateControlsState();
        }

        private void HideProgress()
        {
            toolStripStatusLabel4.Visible = false;
            toolStripProgressBar1.Enabled = false;
            toolStripProgressBar1.Visible = false;
        }

        private void ShowProgress()
        {
            toolStripStatusLabel4.Visible = true;
            toolStripProgressBar1.Enabled = true;
            toolStripProgressBar1.Visible = true;
        }

        private void listView_ItemActivate(object sender, EventArgs e)
        {
            openToolStripMenuItem.PerformClick();
        }

        private async void addFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] fileNames = null;

            using (var openFileDlg = new OpenFileDialog())
            {
                openFileDlg.Filter = "Todos|*.*";
                openFileDlg.Title = "Seleccionar archivos";
                openFileDlg.Multiselect = true;

                if (openFileDlg.ShowDialog() == DialogResult.OK)
                {
                    fileNames = openFileDlg.FileNames;
                }
            }

            if (fileNames == null) return;

            var sessionId = await GetTicketAsync();

            if (String.IsNullOrWhiteSpace(sessionId)) return;

            using (var form = new UploadProgressForm(sessionId, currentCommunity.id, currentFolder != null ? currentFolder.FileFolder.id : String.Empty,
                currentFolder != null ? currentFolder.FileFolder.name : currentCommunity.name, fileNames))
            {
                if (form.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
            if (currentFolder != null)
            {
                this.currentFolder.Folders = null;
                this.currentFolder.Files = null;
                this.currentFolder.Node.Nodes.Clear();
                this.currentFolder.Node.Nodes.Add(
                        new TreeNode()
                        {
                            Text = "Cargando carpetas...",
                            ImageIndex = 1,
                            Tag = new FakeFolderObject()
                        });

                if (this.currentFolder.Node == treeView.SelectedNode)
                {
                    listView.Items.Clear();
                    treeView.SelectedNode = null;
                    treeView.SelectedNode = this.currentFolder.Node;
                }
            }
            else
            {

            }


            UpdateControlsState();
        }

        private async void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folderPath = String.Empty;

            using (var folderBrowserDlg = new FolderBrowserDialog())
            {
                if (folderBrowserDlg.ShowDialog() == DialogResult.OK)
                {
                    folderPath = folderBrowserDlg.SelectedPath;
                }
            }

            if (String.IsNullOrWhiteSpace(folderPath)) return;

            var sessionId = await GetTicketAsync();

            if (String.IsNullOrWhiteSpace(sessionId)) return;

            using (var form = new UploadProgressForm(sessionId, currentCommunity.id, currentFolder.FileFolder.id,
                currentFolder.FileFolder.name, folderPath))
            {
                if (form.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            this.currentFolder.Folders = null;
            this.currentFolder.Files = null;
            this.currentFolder.Node.Nodes.Clear();
            this.currentFolder.Node.Nodes.Add(
                    new TreeNode()
                    {
                        Text = "Cargando carpetas...",
                        ImageIndex = 1,
                        Tag = new FakeFolderObject()
                    });

            if (this.currentFolder.Node == treeView.SelectedNode)
            {
                listView.Items.Clear();
                treeView.SelectedNode = null;
                treeView.SelectedNode = this.currentFolder.Node;
            }

            UpdateControlsState();
        }

        private void listView_DragEnter(object sender, DragEventArgs e)
        {
            // If the data is a file or a bitmap, display the copy cursor.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                listView.MultiSelect = false;
            }
            else
            {
                e.Effect = DragDropEffects.None;
                listView.MultiSelect = true;
            }

        }

        private void listView_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            Point p = listView.PointToClient(MousePosition);
            ListViewItem targetItem = listView.GetItemAt(p.X, p.Y);
            if (targetItem != null)               //if dropping on a target item
            {
                if (targetItem.Tag is DokuFileInfo)
                {
                    return;
                }
                targetItem.Selected = true;
                //if (targetItem.SubItems.Count > 1) e.Effect = DragDropEffects.None;//if IsFile
                //else e.Effect = DragDropEffects.Copy;
                e.Effect = DragDropEffects.Copy;
                return;
            }
            foreach (ListViewItem item in listView.Items) item.Selected = false; //if dragging into current address
            e.Effect = DragDropEffects.Copy;
        }

        private async void listView_DragDrop(object sender, DragEventArgs e)
        {
            // Retrieve the client coordinates of the mouse pointer.
            Point targetPoint = listView.PointToClient(new Point(e.X, e.Y));

            // Retrieve the index of the item closest to the mouse pointer.
            int targetIndex = listView.InsertionMark.NearestIndex(targetPoint);
            // Confirm that the mouse pointer is not over the dragged item.

            Point cp = listView.PointToClient(new Point(e.X, e.Y));
            ListViewItem dragToItem = listView.GetItemAt(cp.X, cp.Y);
            if (dragToItem != null)
            {
                if (dragToItem.Tag is DokuFileInfo)
                {
                    //A la comunidad, no hacer nada, probablemente haya una carpeta seleccionada, en el arbol

                }
                else
                {
                    //A la carpeta dentro de la comunidad
                    currentFolder = dragToItem.Tag as DokuFolderInfo;

                }
            }
            /*
            int dropIndex = dragToItem.Index;

            if (targetIndex > -1)
            {
                //if (targetIndex > 0) targetIndex--; //ajust the item target
            }
            else
            {
                var item = listView.GetItemAt(targetPoint.X, targetPoint.Y);

                if (item != null)
                {
                    targetIndex = item.Index;
                }
            }

            if (targetIndex > -1 && listView.Items[targetIndex].Tag is DokuFolderInfo)
            {
                this.currentFolder = listView.Items[targetIndex].Tag as DokuFolderInfo;
            }
            else
            {
                if (treeView.SelectedNode != null)
                {
                    if (treeView.SelectedNode.Tag is DokuFolderInfo)
                    {
                        this.currentFolder = treeView.SelectedNode.Tag as DokuFolderInfo;
                    }
                    else
                    {
                        this.currentFolder = null;
                    }
                }
            }
            */

            this.currentFile = null;
            this.souceSelection = SourceSelection.List;

            // Handle FileDrop data.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Assign the file names to a string array, in
                // case the user has selected multiple files.
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                /*
                string msg = string.Empty;
                if (this.currentFolder == null)
                {
                    msg = string.Format("Subir a la comunidad: {0}", currentCommunity.name);
                }
                else
                {
                    msg = string.Format("Subir a la carpeta {0} en la comunidad {1}", currentFolder.FileFolder.name, currentCommunity.name);
                }
                MessageBox.Show(msg);
                return;*/
                var sessionId = await GetTicketAsync();

                using (var form = new UploadProgressForm(sessionId, currentCommunity.id, (currentFolder != null ? currentFolder.FileFolder.id : String.Empty),
                        (currentFolder != null ? currentFolder.FileFolder.name : this.currentCommunity.name), paths))
                {
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                }
            }
            foreach (ListViewItem item in listView.Items) item.Selected = false; //if dragging into current address

            TreeNode node = treeView.SelectedNode;
            //if (this.currentFolder != null)
            //{
            //    this.currentFolder.Folders = null;
            //    this.currentFolder.Files = null;
            //    this.currentFolder.Node.Nodes.Clear();
            //}
            //else
            //{
            //    node = treeView.SelectedNode;
            //}
            DokuNodeInfo nodeInfo = (DokuNodeInfo)node.Tag;
            nodeInfo.Folders = null;
            nodeInfo.Files = null;
            node.Nodes.Clear();

            node.Nodes.Add(
                    new TreeNode()
                    {
                        Text = "Cargando carpetas...",
                        ImageIndex = 1,
                        Tag = new FakeFolderObject()
                    });

            node.Collapse();

            treeView.SelectedNode = null;
            treeView.SelectedNode = node;

            UpdateControlsState();
        }

        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {

            switch (this.souceSelection)
            {
                case SourceSelection.None:
                    break;
                case SourceSelection.Tree:
                    {
                        var nodeIndex = 0;

                        treeView.LabelEdit = true;

                        if (treeView.SelectedNode == null)
                        {
                            nodeIndex = treeView.Nodes.Add(
                                new TreeNode()
                                {
                                    Text = StringResources.NewFolder,
                                    ImageIndex = 1,
                                    SelectedImageIndex = 1,
                                    Tag = new FakeFolderObject()
                                }
                                );

                            treeView.SelectedNode = treeView.Nodes[nodeIndex];
                            treeView.SelectedNode.BeginEdit();
                        }
                        else
                        {
                            nodeIndex = treeView.SelectedNode.Nodes.Add(
                                    new TreeNode()
                                    {
                                        Text = StringResources.NewFolder,
                                        ImageIndex = 1,
                                        SelectedImageIndex = 1
                                    }
                                    );

                            treeView.SelectedNode = treeView.SelectedNode.Nodes[nodeIndex];
                            treeView.SelectedNode.BeginEdit();
                        }
                    }
                    break;
                case SourceSelection.List:
                    {
                        listView.LabelEdit = true;

                        var listViewItem = listView.Items.Add(new ListViewItem()
                        {
                            Text = StringResources.NewFolder,
                            ImageIndex = 0,
                            Group = listView.Groups[0]
                        }
                        );

                        listViewItem.SubItems.Add(DateTime.Now.ToShortDateString());
                        listViewItem.SubItems.Add("Carpeta");
                    }
                    break;
                default:
                    break;
            }
        }

        private async void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                e.Node.Text = e.Label;
            }

            if (e.Node.Tag == null)
            {
                this.Cursor = Cursors.WaitCursor;
                ShowProgress();

                try
                {
                    var folderId = String.Empty;
                    var sessionId = await Session.GetTikectAsync();

                    if (String.IsNullOrWhiteSpace(sessionId)) return;

                    if (e.Node.Parent == null)
                    {
                        folderId = await this.dataSvc.CreateFolderAsync(sessionId, currentCommunity.id, String.Empty, e.Node.Text);
                    }
                    else
                    {
                        if (e.Node.Parent.Tag is DokuFolderInfo)
                        {
                            var parentFolder = e.Node.Parent.Tag as DokuFolderInfo;
                            folderId = await this.dataSvc.CreateFolderAsync(sessionId, currentCommunity.id, parentFolder.FileFolder.id, e.Node.Text);
                        }
                        else
                        {
                            folderId = await this.dataSvc.CreateFolderAsync(sessionId, currentCommunity.id, String.Empty, e.Node.Text);
                        }
                    }

                    var folder = new FileFolder() { id = folderId, name = e.Node.Text, type = "F", modifiedTime = DateTimeHelper.ToUnixEpoch(DateTime.Now) };
                    var dokuFolder = new DokuFolderInfo() { FileFolder = folder, Node = e.Node };

                    e.Node.ImageIndex = 0;
                    e.Node.SelectedImageIndex = 0;
                    e.Node.Tag = dokuFolder;
                    e.Node.ContextMenuStrip = treeViewContextMenuStrip;

                    if (e.Node.Parent != null)
                    {
                        DokuNodeInfo parentFolder = e.Node.Parent.Tag as DokuNodeInfo;

                        if (parentFolder.Folders == null)
                        {
                            parentFolder.Folders = new List<DokuFolderInfo>();
                        }

                        parentFolder.Folders.Add(dokuFolder);
                    }

                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    HideProgress();
                }
            }
            else
            {
                var dokuFolder = e.Node.Tag as DokuFolderInfo;

                if (String.Compare(dokuFolder.FileFolder.name, e.Node.Text) == 0) return;

                this.Cursor = Cursors.WaitCursor;
                ShowProgress();

                try
                {
                    var sessionId = await Session.GetTikectAsync();

                    if (String.IsNullOrWhiteSpace(sessionId)) return;

                    await this.dataSvc.RenameFileFolderAsync(sessionId, currentCommunity.id, dokuFolder.FileFolder.id, e.Node.Text);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                    HideProgress();
                }

                dokuFolder.FileFolder.name = e.Node.Text;
            }

            treeView.LabelEdit = false;

            UpdateControlsState();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var tempPath = DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.DownloadDirectory);

            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
        }

        private void viewDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView.View = View.Details;
        }

        private void viewLargeIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView.View = View.LargeIcon;
        }

        private void viewSmallIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView.View = View.SmallIcon;
        }

        private void viewListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView.View = View.List;
        }

        private void listView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (listView.Columns[e.Column].Tag == null)
            {
                listView.Columns[e.Column].Tag = SortOrder.Ascending;
                listView.ListViewItemSorter = new ListViewItemComparer(e.Column);
            }
            else
            {
                var sortOrder = listView.Columns[e.Column].Tag.ToString();

                switch (sortOrder)
                {
                    case "Ascending":
                        listView.Columns[e.Column].Tag = SortOrder.Descending;
                        listView.ListViewItemSorter = new ListViewItemReverseComparer(e.Column);
                        break;

                    case "Descending":
                        listView.Columns[e.Column].Tag = SortOrder.Ascending;
                        listView.ListViewItemSorter = new ListViewItemComparer(e.Column);
                        break;

                    default:
                        break;
                }
            }
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new SettingsForm())
            {
                form.ShowDialog();
            }
        }

        private async void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //this.Close();

            string _ticket = await Session.GetTikectAsync();

            List<SearchUserResult> results = await dataSvc.SearchUserAsync(_ticket, "nu");
            MessageBox.Show("Test");
        }

        private void menuStrip_Enter(object sender, EventArgs e)
        {
            UpdateControlsState();
        }

        private void navigationPaneToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer.Panel1Collapsed = !navigationPaneToolStripMenuItem.Checked;
        }

        private void metadataPaneToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            splitContainerContent.Panel2Collapsed = !metadataPaneToolStripMenuItem.Checked;
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (this.souceSelection)
            {
                case SourceSelection.None:
                    break;
                case SourceSelection.Tree:
                    if (treeView.SelectedNode != null)
                    {
                        treeView.LabelEdit = true;
                        treeView.SelectedNode.BeginEdit();
                    }
                    break;
                case SourceSelection.List:
                    if (this.currentFile != null || this.currentFolder != null)
                    {
                        listView.LabelEdit = true;
                        this.currentListViewItem.BeginEdit();
                    }
                    break;
                default:
                    break;
            }
        }

        private async void listView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {

            if (e.Label != null)
            {
                listView.Items[e.Item].Text = e.Label;
            }

            if (listView.Items[e.Item].Tag == null)
            {

                try
                {
                    var folderId = await CreateFolderAsync(this.currentCommunity.id, this.currentFolder.FileFolder.id, listView.Items[e.Item].Text);

                    var folder = new FileFolder() { id = folderId, name = listView.Items[e.Item].Text, type = "F", modifiedTime = DateTimeHelper.ToUnixEpoch(DateTime.Now) };
                    var node = treeView.SelectedNode.Nodes.Add(listView.Items[e.Item].Text);
                    var dokuFolder = new DokuFolderInfo() { FileFolder = folder, Node = node };

                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                    node.Tag = dokuFolder;

                    var parentFolder = treeView.SelectedNode.Tag as DokuFolderInfo;

                    if (parentFolder.Folders == null)
                    {
                        parentFolder.Folders = new List<DokuFolderInfo>();
                    }

                    parentFolder.Folders.Add(dokuFolder);
                }
                catch
                {
                    //No se pudo crear la carpeta

                }
            }
            else
            {


                if (listView.Items[e.Item].Tag is DokuFolderInfo)
                {
                    var dokuFolder = listView.Items[e.Item].Tag as DokuFolderInfo;

                    if (String.Compare(dokuFolder.FileFolder.name, listView.Items[e.Item].Text) == 0) return;

                    try
                    {
                        await RenameFileFolderAsync(currentCommunity.id, dokuFolder.FileFolder.id, listView.Items[e.Item].Text);

                        dokuFolder.FileFolder.name = listView.Items[e.Item].Text;
                        dokuFolder.Node.Text = listView.Items[e.Item].Text;
                    }
                    catch (Exception)
                    {
                        //No se pudo renombrar la carpeta o el fichero
                    }
                }
                else
                {
                    var dokuFile = listView.Items[e.Item].Tag as DokuFileInfo;

                    if (String.Compare(dokuFile.FileFolder.name, listView.Items[e.Item].Text) == 0)

                        try
                        {
                            await RenameFileFolderAsync(currentCommunity.id, dokuFile.FileFolder.id, listView.Items[e.Item].Text);

                            dokuFile.FileFolder.name = listView.Items[e.Item].Text;
                        }
                        catch (Exception)
                        {
                            //No se pudo renombrar la carpeta o el fichero
                        }
                }
            }

            listView.LabelEdit = false;
        }

        private async void editMetadataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sessionId = String.Empty;

            this.Cursor = Cursors.WaitCursor;
            ShowProgress();

            sessionId = await GetTicketAsync();

            if (String.IsNullOrWhiteSpace(sessionId)) return;

            using (var form = new MetadataForm(sessionId, this.currentFile.FileFolder.id, this.currentFile.FileFolder.name))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;
                    ShowProgress();

                    try
                    {
                        await this.dataSvc.UpdateDocumentMetadataAsync(sessionId, form.DocumentType,
                            this.currentFile.FileFolder.id, form.Metadata.ToArray());

                        this.currentFile.Metadata.Clear();
                        this.currentFile.Metadata.AddRange(form.Metadata);
                        DisplayMetadata(this.currentFile);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                        HideProgress();
                    }
                }
            }
        }

        private async void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.currentFile != null)
            {
                await OpenFileAsync();
            }

            if (this.currentFolder != null)
            {
                treeView.SelectedNode = this.currentFolder.Node;
            }
        }

        private async void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folderPath = String.Empty;

            using (var folderBrowserDlg = new FolderBrowserDialog())
            {
                if (folderBrowserDlg.ShowDialog() == DialogResult.OK)
                {
                    folderPath = folderBrowserDlg.SelectedPath;
                }
            }

            if (String.IsNullOrWhiteSpace(folderPath)) return;

            var files = new List<FileFolder>();

            foreach (ListViewItem item in listView.SelectedItems)
            {
                if (item.Tag is DokuFileInfo)
                {
                    var dokuFile = item.Tag as DokuFileInfo;
                    files.Add(dokuFile.FileFolder);
                }
                else
                {
                    var dokuFolder = item.Tag as DokuFolderInfo;
                    files.Add(dokuFolder.FileFolder);
                }
            }

            if (files.Count == 0) return;

            this.Cursor = Cursors.WaitCursor;
            ShowProgress();

            var sessionId = String.Empty;

            try
            {
                sessionId = await Session.GetTikectAsync();
            }
            finally
            {
                this.Cursor = Cursors.Default;
                HideProgress();
            }

            if (String.IsNullOrWhiteSpace(sessionId)) return;

            using (var form = new DownloadProgressForm(sessionId, currentCommunity.id, folderPath, ref files))
            {
                form.ShowDialog();
            }
        }

        private void menuStrip_MenuActivate(object sender, EventArgs e)
        {
            UpdateControlsState();
        }

        private void changeCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Session.ChangeCredentials();
        }

        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar los elementos seleccionados?",
                this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            this.Cursor = Cursors.WaitCursor;
            ShowProgress();

            try
            {
                var sessionId = await Session.GetTikectAsync();

                if (String.IsNullOrWhiteSpace(sessionId)) return;

                switch (this.souceSelection)
                {
                    case SourceSelection.None:
                        break;
                    case SourceSelection.Tree:
                        {
                            await this.dataSvc.DeleteFolderAsync(sessionId, this.currentCommunity.id, this.currentFolder.FileFolder.id);

                            var parentNode = this.currentFolder.Node.Parent;

                            if (parentNode != null)
                            {
                                var parentFolder = parentNode.Tag as DokuNodeInfo;
                                parentFolder.Folders.Remove(this.currentFolder);
                                treeView.SelectedNode = parentNode;
                            }
                            else
                            {
                                if (treeView.Nodes.Count > 0)
                                {
                                    treeView.SelectedNode = treeView.Nodes[0];
                                }
                            }

                            treeView.Nodes.Remove(this.currentFolder.Node);
                        }
                        break;
                    case SourceSelection.List:
                        {
                            var parentFolder = treeView.SelectedNode.Tag as DokuNodeInfo;

                            foreach (ListViewItem item in listView.SelectedItems)
                            {
                                if (item.Tag is DokuFileInfo)
                                {
                                    var dokuFile = item.Tag as DokuFileInfo;

                                    this.Cursor = Cursors.WaitCursor;
                                    ShowProgress();

                                    await this.dataSvc.DeleteFileAsync(sessionId, this.currentCommunity.id, dokuFile.FileFolder.id);

                                    parentFolder.Files.Remove(dokuFile);
                                }
                                else
                                {
                                    var dokuFolder = item.Tag as DokuFolderInfo;

                                    await this.dataSvc.DeleteFolderAsync(sessionId, this.currentCommunity.id, dokuFolder.FileFolder.id);
                                    parentFolder.Folders.Remove(dokuFolder);
                                    treeView.Nodes.Remove(dokuFolder.Node);
                                }
                            }

                            DisplayFilesAndFoldersOnListView(treeView.SelectedNode);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                //Refrescar el arbol
                await DisplayCommunitiesAsync();
            }
            finally
            {
                this.Cursor = Cursors.Default;
                HideProgress();
            }
        }

        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            // If the data is a file or a bitmap, display the copy cursor.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private async void treeView_DragDrop(object sender, DragEventArgs e)
        {
            var targetPoint = treeView.PointToClient(new Point(e.X, e.Y));
            targetNode = treeView.GetNodeAt(targetPoint);

            if (targetNode == null) return;

            var dokufolder = targetNode.Tag as DokuNodeInfo;

            // Handle FileDrop data.
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Assign the file names to a string array, in
                // case the user has selected multiple files.
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);

                var sessionId = await GetTicketAsync();

                using (var form = new UploadProgressForm(sessionId, (dokufolder is DokuUserGroupInfo ? (dokufolder as DokuUserGroupInfo).UserGroup.id : currentCommunity.id), (dokufolder is DokuFolderInfo ? (dokufolder as DokuFolderInfo).FileFolder.id : String.Empty),
                        (dokufolder is DokuFolderInfo ? (dokufolder as DokuFolderInfo).FileFolder.name : String.Empty), paths))
                {
                    if (form.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }
                }
            }

            /*
            dokufolder.Folders = null;
            dokufolder.Files = null;
            dokufolder.Node.Nodes.Clear();
            dokufolder.Node.Nodes.Add(
                    new TreeNode()
                    {
                        Text = "Cargando carpetas...",
                        ImageIndex = 1,
                        Tag = new FakeFolderObject()
                    });

            dokufolder.Node.Collapse();

            if (dokufolder.Node == treeView.SelectedNode)
            {
                treeView.SelectedNode = null;
                treeView.SelectedNode = dokufolder.Node;
            }
            */
            TreeNode node = targetNode;
            DokuNodeInfo nodeInfo = (DokuNodeInfo)node.Tag;
            nodeInfo.Folders = null;
            nodeInfo.Files = null;
            node.Nodes.Clear();

            node.Nodes.Add(
                    new TreeNode()
                    {
                        Text = "Cargando carpetas...",
                        ImageIndex = 1,
                        Tag = new FakeFolderObject()
                    });

            node.Collapse();

            treeView.SelectedNode = null;
            treeView.SelectedNode = node;
            UpdateControlsState();
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            if (this.currentFolder != null)
            {
                //treeView.SelectedNode = this.currentFolder.Node;
                TreeNode node = this.currentFolder.Node;
                if (this.currentFolder.Node.Parent != null)
                {
                    this.currentFolder.Node.Parent.Expand();
                }
                treeView.SelectedNode = node;
            }
        }

        private void syncFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var appPath = "C:\\Users\\Emilio\\Documents\\Visual Studio 2013\\Projects\\DokuFlex FileSync\\Source\\DokuFlex.FileSync\\bin\\Debug\\DokuFlex.FileSync.exe";
            //var appPath = "C:\\Users\\Emilio\\Documents\\Visual Studio 2013\\Projects\\DokuFlex\\Build\\Debug\\FileSync\\DokuFlex.FileSync.exe";
            var appPath = String.Format("{0}\\FileSync\\DokuFlex.FileSync.exe", Application.StartupPath);
            var pathParams = String.Format("{0} \"{1}\" {2} \"{3}\"",
                currentCommunity.id,
                currentCommunity.name,
                currentFolder.FileFolder.id,
                currentFolder.FileFolder.name);

            ProcessStartInfo startInfo = new ProcessStartInfo(appPath);
            startInfo.WindowStyle = ProcessWindowStyle.Normal;
            startInfo.Arguments = pathParams;
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = String.Format("{0}\\FileSync", Application.StartupPath);

            System.Diagnostics.Process.Start(startInfo);
        }

        private void treeView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode clickedNode = treeView.GetNodeAt(e.Location);
                treeView.SelectedNode = clickedNode;
            }
        }

        private async void refreshTreeItem_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            await DisplayCommunitiesAsync();
        }
    }
}
