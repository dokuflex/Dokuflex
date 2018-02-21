//=======================================================================================
// PulsarSoft Inc.
//=======================================================================================
// EL SOFTWARE SE ENTREGA "TAL CUAL", SIN GARANTÍA DE NINGÚN TIPO, EXPRESAS O IMPLÍCITAS,
// INCLUYENDO PERO NO LIMITADAS A LAS GARANTÍAS DE COMERCIALIZACIÓN, APTITUD PARA UN
// PROPÓSITO PARTICULAR Y NO INFRACCIÓN. EN NINGÚN CASO, LOS AUTORES O TITULARES DEL
// COPYRIGHT SERÁN RESPONSABLES POR CUALQUIER RECLAMACIÓN, DAÑO U OTRA RESPONSABILIDAD,
// YA SEA EN UNA ACCIÓN DE CONTRATO, AGRAVIO O CUALQUIER OTRA FORMA, QUE SURJAN DE O EN
// CONEXION CON EL SOFTWARE O EL USO U OTROS TRATOS EN EL SOFTWARE.
//=======================================================================================
// Copyright (c) PulsarSoft Inc. Reservados todos los derechos.
// Este código es liberado bajo los términos de la licencia Apache v2.0,
// vea el archivo de texto licencia-es.txt para más información.
//=======================================================================================

namespace DokuFlex.WinForms.Common
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    using DokuFlex.Windows.Common;
    using DokuFlex.Windows.Common.Extensions;
    using DokuFlex.Windows.Common.Services.Data;
    using DokuFlex.Windows.Common.Services;
    using DokuFlex.WinForms.Common.Resources;

    public partial class OpenFileForm : Form
    {
        private string _ticket;

        private bool _taskCancelled;

        private UserGroup _currentGroup;

        private FileFolder _currentFolder;

        private FileFolder _currentFile;

        private FileExtension _currentExtension;

        private IList<FileExtension> _fileExtensions;

        public void ShowMetadata(string fileId, string fileName)
        {
            using (var form = new MetadataForm(_ticket, fileId, fileName))
            {
                form.ShowDialog();
            }
        }

        private void TaskAsyncExceptionHandle(AggregateException e)
        {
            MessageBox.Show(string.Format("{0}\n\n{1}\n{2}",
                ErrorMessages.AsyncTaskError, e.Message, e.InnerException.Message),
                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            _taskCancelled = false;

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DisplayGroupsAsyncBegin()
        {
            //Show loading information.
            lbLoadingGroups.Visible = true;
            lbLoadingGroups.BringToFront();

            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task<IList<UserGroup>>.Factory.StartNew(() => DokuFlexService.GetUserGroups(_ticket));

            task.ContinueWith(t => DisplayGroupsAsyncEnd(t.Result), taskScheduler);
            task.ContinueWith(t => TaskAsyncExceptionHandle(t.Exception),
                new CancellationTokenSource().Token, TaskContinuationOptions.OnlyOnFaulted,
                taskScheduler);
        }

        private void DisplayGroupsAsyncEnd(IList<UserGroup> groups)
        {
            cbxGroups.DataSource = new BindingList<UserGroup>(groups);
            cbxGroups.SelectedIndex = 0;

            //Hide loading information controls.
            lbLoadingGroups.Visible = false;
            lbLoadingGroups.SendToBack();
        }

        private void DisplayRootsFoldersAsyncBegin()
        {
            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task<IList<FileFolder>>.Factory.StartNew(() => DokuFlexService.GetFolders(_ticket, _currentGroup.id, string.Empty));

            task.ContinueWith(t => DisplayRootsFoldersAsyncEnd(t.Result), taskScheduler);
            task.ContinueWith(t => TaskAsyncExceptionHandle(t.Exception),
                new CancellationTokenSource().Token, TaskContinuationOptions.OnlyOnFaulted,
                taskScheduler);
        }

        private void DisplayRootsFoldersAsyncEnd(IList<FileFolder> filesFolders)
        {
            if (_taskCancelled) return;

            treeView.SuspendLayout();
            treeView.Nodes.Clear();

            if (filesFolders.Count > 0)
            {
                foreach (var fileFolder in filesFolders)
                {
                    if (fileFolder.type == "F")
                    {
                        treeView.Nodes.Add(
                        new TreeNode()
                        {
                            Text = fileFolder.name,
                            ImageIndex = 0,
                            Tag = fileFolder
                        }
                        );
                    }
                }

                //Add fake folders as indicators
                foreach (TreeNode node in treeView.Nodes)
                {
                    node.Nodes.Add(
                        new TreeNode()
                        {
                            Text = StringResources.LoadingFolders,
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
        }

        private void DisplayChildsFoldersAsyncBegin(TreeNode node)
        {
            var folderId = (node.Tag as FileFolder).id;
            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task<IList<FileFolder>>.Factory.StartNew(() => DokuFlexService.GetFilesFolders(_ticket, _currentGroup.id, folderId));

            task.ContinueWith(t => DisplayChildsFoldersAsyncEnd(t.Result, node), taskScheduler);
            task.ContinueWith(t => TaskAsyncExceptionHandle(t.Exception),
                new CancellationTokenSource().Token, TaskContinuationOptions.OnlyOnFaulted,
                taskScheduler);

        }

        private void DisplayChildsFoldersAsyncEnd(IList<FileFolder> filesFolders, TreeNode node)
        {
            if (_taskCancelled) return;

            treeView.SuspendLayout();

            node.Nodes.Clear(); //Remove fake folder indicator. :)

            if (filesFolders.Count > 0)
            {
                foreach (var fileFolder in filesFolders)
                {
                    if (fileFolder.type == "F")
                    {
                        node.Nodes.Add(
                        new TreeNode()
                        {
                            Text = fileFolder.name,
                            ImageIndex = 0,
                            Tag = fileFolder
                        }
                        );
                    }
                }

                //Add fake folders to child nodes...
                foreach (TreeNode childNode in node.Nodes)
                {
                    childNode.Nodes.Add(
                        new TreeNode()
                        {
                            Text = StringResources.LoadingFolders,
                            ImageIndex = 1,
                            Tag = new FakeFolderObject()
                        }
                        );
                }
            }

            //Convert current node to Folder type
            var folder = new Folder()
            {
                FileFolder = node.Tag as FileFolder,
                FilesFolders = filesFolders
            };

            node.Tag = folder;

            treeView.ResumeLayout();

            if (node == treeView.SelectedNode)
            {
                DisplayFilesFoldersOnListView(node);
            }
        }

        /// <summary>
        /// This method is usefull for show progress to the user.
        /// </summary>
        private void DisplayRootLoadingFolder()
        {
            treeView.SuspendLayout();
            treeView.Nodes.Clear();

            var nodeIndex = treeView.Nodes.Add(
                new TreeNode()
                {
                    Text = StringResources.LoadingFolders,
                    ImageIndex = 1,
                    Tag = new FakeFolderObject()
                }
                );

            treeView.TopNode = treeView.Nodes[nodeIndex];
            treeView.ResumeLayout();
        }

        private void DisplayFilesFoldersOnListView(TreeNode node)
        {
            listView.Items.Clear();

            if (node == null || !(node.Tag is Folder))
            {
                return;
            }

            var filesFolders = (node.Tag as Folder).FilesFolders;

            listView.SuspendLayout();

            foreach (var fileFolder in filesFolders)
            {
                if (fileFolder.type == "C")
                {
                    var extension = System.IO.Path.GetExtension(fileFolder.name);

                    if (_currentExtension.Extension.Contains(extension))
                    {
                        var imageIndex = _fileExtensions.FirstOrDefault(e => e.Extension.Equals(extension)).ImageIndex;
                        var dateModified = fileFolder.modifiedTime.FromUnixEpoch();
                        var listViewItem = listView.Items.Add(new ListViewItem()
                        {
                            Text = fileFolder.name,
                            ImageIndex = imageIndex,
                            Tag = fileFolder
                        }
                        );

                        listViewItem.SubItems.Add(dateModified.ToShortDateString());
                    }
                }
            }

            listView.ResumeLayout();

            if (listView.Items.Count == 0)
            {
                lbFolderEmptyInfo.Visible = true;
                lbFolderEmptyInfo.BringToFront();
            }
            else
            {
                lbFolderEmptyInfo.Visible = false;
                lbFolderEmptyInfo.SendToBack();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            cbxView.SelectedIndex = 0;

            DisplayGroupsAsyncBegin();
        }

        public UserGroup Group
        {
            get
            {
                return _currentGroup;
            }
        }

        public FileFolder Folder
        {
            get
            {
                return _currentFolder;
            }
        }

        public FileFolder File
        {
            get
            {
                return _currentFile;
            }
        }

        public FileExtension Extension
        {
            get
            {
                return _currentExtension;
            }
        }

        public bool ShowOpenFileDialog()
        {
            return this.ShowDialog() == DialogResult.OK;
        }

        public OpenFileForm(string ticket, IList<FileExtension> fileExtensions)
        {
            InitializeComponent();

            if (fileExtensions == null)
            {
                throw new ArgumentNullException("fileExtensions");
            }

            _fileExtensions = fileExtensions;
            _ticket = ticket;
            _taskCancelled = false;
            _currentGroup = null;
            _currentFolder = null;
            _currentFile = null;

            cbxFileExtensions.DataSource = new BindingList<FileExtension>(_fileExtensions);
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is FileFolder)
            {
                _currentFolder = e.Node.Tag as FileFolder;
            }
            else
            {
                if (e.Node.Tag is Folder)
                {
                    _currentFolder = (e.Node.Tag as Folder).FileFolder;
                }
            }

            if (e.Node.FirstNode != null && e.Node.FirstNode.Tag is FakeFolderObject)
            {
                DisplayChildsFoldersAsyncBegin(e.Node);
            }

            DisplayFilesFoldersOnListView(e.Node);
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.FirstNode != null && e.Node.FirstNode.Tag is FakeFolderObject)
            {
                DisplayChildsFoldersAsyncBegin(e.Node);
            }
        }

        private void cbxFileExtensions_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentExtension = cbxFileExtensions.SelectedItem as FileExtension;

            DisplayFilesFoldersOnListView(treeView.SelectedNode);
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                _currentFile = e.Item.Tag as FileFolder;
                textFileName.Text = _currentFile.name;
            }
        }

        private void OpenFileView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (_currentFile == null)
                {
                    MessageBox.Show(ErrorMessages.FileNotValidError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }

            _taskCancelled = true;
        }

        private void cbxView_SelectedValueChanged(object sender, EventArgs e)
        {
            var value = cbxView.SelectedItem.ToString();

            switch (value)
            {
                case "Iconos pequeños":
                    listView.View = View.SmallIcon;
                    break;

                case "Iconos grandes":
                    listView.View = View.LargeIcon;
                    break;

                case "Lista":
                    listView.View = View.List;
                    break;

                default:
                    listView.View = View.Details;
                    break;
            }
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

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_currentFile != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentFile != null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void metadataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_currentFile != null)
            {
                ShowMetadata(_currentFile.id, _currentFile.name);
            }
        }

        private void cbxGroups_SelectionChangeCommitted(object sender, EventArgs e)
        {
            _currentGroup = cbxGroups.SelectedItem as UserGroup;

            if (_currentGroup != null)
            {
                listView.Items.Clear();
                DisplayRootLoadingFolder();
                DisplayRootsFoldersAsyncBegin();
            }
        }

        private void cbxGroups_SelectedValueChanged(object sender, EventArgs e)
        {
            _currentGroup = cbxGroups.SelectedItem as UserGroup;

            if (_currentGroup != null)
            {
                listView.Items.Clear();
                DisplayRootLoadingFolder();
                DisplayRootsFoldersAsyncBegin();
            }
        }
    }
}
