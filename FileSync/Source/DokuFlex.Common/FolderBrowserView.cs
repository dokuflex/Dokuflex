// =================================================================================================================
// DokuFlex
// =================================================================================================================
// ©2013 DokuFlex. All rights reserved. Certain content used with permission from contributors
// http://www.dokuflex.com/allwinproducts/license/contributors
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and limitations under the License.
// =================================================================================================================

namespace DokuFlex.Common
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using DokuFlex.Common.ServiceAgents;

    public partial class FolderBrowserView : Form
    {
        private readonly FolderBrowserPresenter _presenter;

        private string _ticket;

        private bool _taskCancelled;

        private UserGroup _currentGroup;

        private FileFolder _currentFolder;

        private void DisplayGroupsAsyncExceptionHandle(AggregateException e)
        {
            MessageBox.Show(string.Format("{0}\n\n{1}\n{2}",
                Messages.LoadGroupsException, e.Message, e.InnerException.Message),
                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

            _taskCancelled = true;

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DisplayGroupsAsyncBegin()
        {
            //Show loading information.
            lbLoadingGroups.Visible = true;
            lbLoadingGroups.BringToFront();

            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task<List<UserGroup>>.Factory.StartNew(() => _presenter.GetUserGroups(_ticket));

            task.ContinueWith(t => DisplayGroupsAsyncEnd(t.Result), taskScheduler);
            task.ContinueWith(t => DisplayGroupsAsyncExceptionHandle(t.Exception),
                new CancellationTokenSource().Token, TaskContinuationOptions.OnlyOnFaulted,
                taskScheduler);
        }

        private void DisplayGroupsAsyncEnd(List<UserGroup> groups)
        {
            if (_taskCancelled)
            {
                return;
            }

            cbxGroups.DataSource = new BindingList<UserGroup>(groups);
            cbxGroups.SelectedIndex = 0;

            //Hide loading information controls.
            lbLoadingGroups.Visible = false;
            lbLoadingGroups.SendToBack();
        }

        /// <summary>
        /// This method is usefull for show progress to the user.
        /// </summary>
        private void DisplayRootFolderLoading()
        {
            treeView.SuspendLayout();
            treeView.Nodes.Clear();

            var nodeIndex = treeView.Nodes.Add(
                new TreeNode()
                {
                    Text = Resources.LoadingFoldersText,
                    ImageIndex = 1,
                    Tag = new FakeFolderObject()
                }
                );

            treeView.TopNode = treeView.Nodes[nodeIndex];
            treeView.ResumeLayout();
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

        public string FullPath
        {
            get
            {
                return treeView.SelectedNode.FullPath;
            }
        }

        public bool ShowFolderBrowserDialog()
        {
            return this.ShowDialog() == DialogResult.OK;
        }

        public FolderBrowserView(string ticket)
        {
            InitializeComponent();

            _presenter = new FolderBrowserPresenter();
            _ticket = ticket;
            _taskCancelled = false;
            _currentGroup = null;
            _currentFolder = null;
        }

        private async void cbxGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentGroup = cbxGroups.SelectedItem as UserGroup;

            if (_currentGroup != null)
            {
                DisplayRootFolderLoading();

                try
                {
                    var folders = await _presenter.GetFoldersAsync(_ticket, _currentGroup.id, String.Empty);

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
                                    Text = Resources.LoadingFoldersText,
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
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("{0}\n\n{1}\n{2}",
                    Messages.LoadFoldersException, ex.Message, ex.InnerException.Message),
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _currentFolder = e.Node.Tag as FileFolder;

            if (e.Node.FirstNode != null && e.Node.FirstNode.Tag is FakeFolderObject)
            {
                try
                {
                    var folderId = (e.Node.Tag as FileFolder).id;
                    var folders = await _presenter.GetFoldersAsync(_ticket, _currentGroup.id, folderId);

                    if (_taskCancelled)
                    {
                        return;
                    }

                    treeView.SuspendLayout();

                    e.Node.Nodes.Clear(); //Remove fake folder indicator. :)

                    if (folders.Count > 0)
                    {
                        foreach (var folder in folders)
                        {
                            e.Node.Nodes.Add(
                                new TreeNode()
                                {
                                    Text = folder.name,
                                    ImageIndex = 0,
                                    Tag = folder
                                }
                                );
                        }

                        //Add fake folders to child nodes...
                        foreach (TreeNode childNode in e.Node.Nodes)
                        {
                            childNode.Nodes.Add(
                                new TreeNode()
                                {
                                    Text = Resources.LoadingFoldersText,
                                    ImageIndex = 1,
                                    Tag = new FakeFolderObject()
                                }
                                );
                        }
                    }

                    treeView.ResumeLayout();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("{0}\n\n{1}\n{2}",
                    Messages.LoadFoldersException, ex.Message, ex.InnerException.Message),
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.FirstNode != null && e.Node.FirstNode.Tag is FakeFolderObject)
            {
                try
                {
                    var folderId = (e.Node.Tag as FileFolder).id;
                    var folders = await _presenter.GetFoldersAsync(_ticket, _currentGroup.id, folderId);

                    if (_taskCancelled)
                    {
                        return;
                    }

                    treeView.SuspendLayout();

                    e.Node.Nodes.Clear(); //Remove fake folder indicator. :)

                    if (folders.Count > 0)
                    {
                        foreach (var folder in folders)
                        {
                            e.Node.Nodes.Add(
                                new TreeNode()
                                {
                                    Text = folder.name,
                                    ImageIndex = 0,
                                    Tag = folder
                                }
                                );
                        }

                        //Add fake folders to child nodes...
                        foreach (TreeNode childNode in e.Node.Nodes)
                        {
                            childNode.Nodes.Add(
                                new TreeNode()
                                {
                                    Text = Resources.LoadingFoldersText,
                                    ImageIndex = 1,
                                    Tag = new FakeFolderObject()
                                }
                                );
                        }
                    }

                    treeView.ResumeLayout();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("{0}\n\n{1}\n{2}",
                    Messages.LoadFoldersException, ex.Message, ex.InnerException.Message),
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            e.Node.Text = String.IsNullOrWhiteSpace(e.Label) ? Resources.NewFolderText : e.Label;

            treeView.LabelEdit = false;

            var folderId = String.Empty;

            if (e.Node.Tag == null)
            {
                try
                {
                    if (e.Node.Parent == null)
                    {
                        folderId = await _presenter.CreateFolderAsync(_ticket, _currentGroup.id, String.Empty, e.Node.Text);
                    }
                    else
                    {
                        var parentFolderId = (e.Node.Parent.Tag as FileFolder).id;

                        folderId = await _presenter.CreateFolderAsync(_ticket, _currentGroup.id, parentFolderId, e.Node.Text);
                    }

                    if (_taskCancelled)
                    {
                        return;
                    }

                    _currentFolder = new FileFolder() { id = folderId, name = e.Node.Text, type = "F" };

                    e.Node.ImageIndex = 0;
                    e.Node.SelectedImageIndex = 0;
                    e.Node.Tag = _currentFolder;

                    treeView.SelectedNode = e.Node;
                }
                catch
                {
                    MessageBox.Show(Messages.NewFolderFailedException, this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    treeView.Nodes.Remove(e.Node);
                }            
            }       
        }

        private void btnNewFolder_Click(object sender, EventArgs e)
        {
            treeView.LabelEdit = true;

            if (treeView.SelectedNode == null)
            {
                var nodeIndex = treeView.Nodes.Add(
                    new TreeNode()
                    {
                        Text = Resources.NewFolderText,
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

                switch (MessageBox.Show(string.Format(Resources.CreateNewFolderText, treeView.SelectedNode.Text, "\n\n"), this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        nodeIndex = treeView.SelectedNode.Nodes.Add(
                        new TreeNode()
                        {
                            Text = Resources.NewFolderText,
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
                            Text = Resources.NewFolderText,
                            ImageIndex = 1,
                            SelectedImageIndex = 1
                        }
                        );

                        treeView.SelectedNode = treeView.Nodes[nodeIndex];
                        treeView.SelectedNode.BeginEdit();
                        break;
                }
            }
        }

        private void FolderBrowserView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (this._currentFolder == null)
                {
                    e.Cancel = true;
                }
                else
                {
                    _taskCancelled = true;
                }
            }
            else
            {
                _taskCancelled = false;
            }
        }
    }
}
