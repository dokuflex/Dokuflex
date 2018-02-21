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

namespace DokuFlex.FileSync
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using DokuFlex.Common.ServiceAgents;

    public partial class SyncNewUserGroupView : Form
    {
        private SyncNewUserGroupPresenter _presenter;

        public UserGroup Group
        {
            get
            {
                return _presenter.Group;
            }
            set
            {
                _presenter.Group = value;
            }
        }

        public string SyncDirectory
        {
            get
            {
                return _presenter.SyncDirectory;
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var userGroupList = await _presenter.GetUserGroupsAsync();
            cbxUserGroup.DataSource = new BindingList<UserGroup>(userGroupList);

            if (Directory.Exists(_presenter.SyncDirectory))
            {
                lkStoreLocation.Text = _presenter.SyncDirectory;
            }
            else
            {
                lkStoreLocation.Text = Resources.SelectSyncDirectoryText;
            }
        }

        public SyncNewUserGroupView()
        {
            InitializeComponent();
        }

        public SyncNewUserGroupView(string ticket, UserGroup group)
        {
            InitializeComponent();

            _presenter = new SyncNewUserGroupPresenter(ticket);
            this.Group = group;

            cbxUserGroup.Enabled = false;
        }

        public SyncNewUserGroupView(string ticket)
        {
            InitializeComponent();
            _presenter = new SyncNewUserGroupPresenter(ticket);
        }

        private void lkStoreLocation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (SyncTableManager.ContainsFolders()) return;

            using (var folderDlg = new FolderBrowserDialog())
            {
                folderDlg.SelectedPath = _presenter.SyncDirectory;

                if (folderDlg.ShowDialog() == DialogResult.OK)
                {
                    _presenter.SyncDirectory = folderDlg.SelectedPath;
                    lkStoreLocation.Text = folderDlg.SelectedPath;
                }
            }
        }

        private void SyncNewView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (String.Compare(_presenter.SyncDirectory, string.Empty) == 0)
                {
                    e.Cancel = true;
                    MessageBox.Show(Resources.SyncDirectoryNotSetText, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (cbxUserGroup.SelectedItem == null)
                {
                    e.Cancel = true;
                    MessageBox.Show(Resources.SourceFolderNoSetText, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _presenter.SaveSyncDirPath();
            }
        }

        private void cbxUserGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            _presenter.Group = cbxUserGroup.SelectedItem as UserGroup;
        }
    }
}
