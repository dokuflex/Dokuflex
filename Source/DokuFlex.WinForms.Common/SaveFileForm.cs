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
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;

    using DokuFlex.Windows.Common.Services;
    using DokuFlex.Windows.Common.Services.Data;
    using DokuFlex.WinForms.Common.Resources;

    public partial class SaveFileForm : Form
    {
        private string _ticket;

        private bool _taskCancelled;

        private UserGroup _currentGroup;

        private FileFolder _currentFolder;

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
            if (_taskCancelled) return;

            cbxGroups.DataSource = new BindingList<UserGroup>(groups);
            cbxGroups.SelectedIndex = 0;

            //Hide loading information controls.
            lbLoadingGroups.Visible = false;
            lbLoadingGroups.SendToBack();
        }

        private void DisplayRootsFoldersAsyncBegin()
        {
            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task<IList<FileFolder>>.Factory.StartNew(() => DokuFlexService.GetFolders(_ticket, _currentGroup.id, String.Empty));

            task.ContinueWith(t => DisplayRootsFoldersAsyncEnd(t.Result), taskScheduler);
            task.ContinueWith(t => TaskAsyncExceptionHandle(t.Exception),
                new CancellationTokenSource().Token, TaskContinuationOptions.OnlyOnFaulted,
                taskScheduler);
        }

        private void DisplayRootsFoldersAsyncEnd(IList<FileFolder> folders)
        {
            if (_taskCancelled) return;

            treeView.SuspendLayout();
            treeView.Nodes.Clear();

            if (folders.Count > 0)
            {
                foreach (var folder in folders)
                {
                    treeView.Nodes.Add(
                        new TreeNode()
                        {
                            Text = folder.name,
                            ImageIndex = 0,
                            Tag = folder
                        }
                        );
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
            var task = Task<IList<FileFolder>>.Factory.StartNew(() => DokuFlexService.GetFolders(_ticket, _currentGroup.id, folderId));

            task.ContinueWith(t => DisplayChildsFoldersAsyncEnd(t.Result, node), taskScheduler);
            task.ContinueWith(t => TaskAsyncExceptionHandle(t.Exception),
                new CancellationTokenSource().Token, TaskContinuationOptions.OnlyOnFaulted,
                taskScheduler);
        }

        private void DisplayChildsFoldersAsyncEnd(IList<FileFolder> folders, TreeNode node)
        {
            if (_taskCancelled) return;

            treeView.SuspendLayout();

            node.Nodes.Clear(); //Remove fake folder indicator. :)

            if (folders.Count > 0)
            {
                foreach (var folder in folders)
                {
                    node.Nodes.Add(
                        new TreeNode()
                        {
                            Text = folder.name,
                            ImageIndex = 0,
                            Tag = folder
                        }
                        );
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

            treeView.ResumeLayout();
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

        private void CreateRootFolderAsyncBegin(TreeNode node)
        {
            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task<string>.Factory.StartNew(() => DokuFlexService.CreateFolder(_ticket, _currentGroup.id, string.Empty, node.Text));

            task.ContinueWith(t => CreateFolderAsyncEnd(t.Result, node), taskScheduler);
            task.ContinueWith(t => TaskAsyncExceptionHandle(t.Exception),
                new CancellationTokenSource().Token, TaskContinuationOptions.OnlyOnFaulted,
                taskScheduler);
        }

        private void CreateChildFolderAsyncBegin(TreeNode node)
        {
            var parentFolderId = (node.Parent.Tag as FileFolder).id;
            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task<string>.Factory.StartNew(() => DokuFlexService.CreateFolder(_ticket, _currentGroup.id, parentFolderId, node.Text));

            task.ContinueWith(t => CreateFolderAsyncEnd(t.Result, node), taskScheduler);
            task.ContinueWith(t => TaskAsyncExceptionHandle(t.Exception),
                new CancellationTokenSource().Token, TaskContinuationOptions.OnlyOnFaulted,
                taskScheduler);
        }

        private void CreateFolderAsyncEnd(string folderId, TreeNode node)
        {
            if (_taskCancelled) return;

            if (string.IsNullOrWhiteSpace(folderId))
            {
                MessageBox.Show(ErrorMessages.AsyncTaskError, this.Text, MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);

                node.Remove();
            }
            else
            {
                _currentFolder = new FileFolder() { id = folderId, name = node.Text, type = "F" };

                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
                node.Tag = _currentFolder;

                treeView.SelectedNode = node;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

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

        public bool SaveAsNewVersion
        {
            get
            {
                return chkSaveAsNewVersion.Checked;
            }
        }

        public string FullPath
        {
            get
            {
                return treeView.SelectedNode.FullPath;
            }
        }

        public bool ShowSaveFileDialog()
        {
            return this.ShowDialog() == DialogResult.OK;
        }

        public SaveFileForm(string ticket)
        {
            InitializeComponent();

            _ticket = ticket;
            _taskCancelled = false;
            _currentGroup = null;
            _currentFolder = null;
        }

        private void cbxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentGroup = cbxGroups.SelectedItem as UserGroup;

            if (_currentGroup != null)
            {
                DisplayRootLoadingFolder();
                DisplayRootsFoldersAsyncBegin();
            }
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _currentFolder = e.Node.Tag as FileFolder;

            if (e.Node.FirstNode != null && e.Node.FirstNode.Tag is FakeFolderObject)
            {
                DisplayChildsFoldersAsyncBegin(e.Node);
            }
        }

        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.FirstNode != null && e.Node.FirstNode.Tag is FakeFolderObject)
            {
                DisplayChildsFoldersAsyncBegin(e.Node);
            }
        }

        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.Node.Text = e.Label;

            if (e.Node.Tag == null)
            {
                if (e.Node.Parent == null)
                {
                    CreateRootFolderAsyncBegin(e.Node);
                }
                else
                {
                    CreateChildFolderAsyncBegin(e.Node);
                }
            }

            treeView.LabelEdit = false;
        }

        private void btnNewFolder_Click(object sender, EventArgs e)
        {
            treeView.LabelEdit = true;

            if (treeView.SelectedNode == null)
            {
                var nodeIndex = treeView.Nodes.Add(
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
                var nodeIndex = 0;

                switch (MessageBox.Show(string.Format(StringResources.CreateNewFolder, treeView.SelectedNode.Text, "\n\n"), this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
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

                        break;

                    case DialogResult.No:
                        nodeIndex = treeView.Nodes.Add(
                        new TreeNode()
                        {
                            Text = StringResources.NewFolder,
                            ImageIndex = 1,
                            SelectedImageIndex = 1
                        }
                        );

                        treeView.SelectedNode = treeView.Nodes[nodeIndex];
                        treeView.SelectedNode.BeginEdit();
                        break;

                    default:
                        break;
                }
            }
        }

        private void SaveFileView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                var node = treeView.SelectedNode;
                this._currentFolder = node != null ? node.Tag as FileFolder : null;

                if (this._currentFolder == null)
                {
                    MessageBox.Show(ErrorMessages.FolderNotValidError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }

            _taskCancelled = true;
        }
    }
}
