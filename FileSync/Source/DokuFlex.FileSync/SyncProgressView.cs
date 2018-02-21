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
    using System.Linq;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Windows.Forms;
    using DokuFlex.Common.ServiceAgents;
    using DokuFlex.Common;

    public partial class SyncProgressView : Form
    {
        private SyncProgressPresenter _presenter;

        private string _ticket;
        private string _syncDirectory;
        private UserGroup _group;
        private bool _processStarted;
        private List<FileFolderInfo> _fileFolderList;

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                List<FileFolder> folderList = await _presenter.GetFoldersAsync(_ticket, _group);
                List<FileFolderInfo> tempFileFolderList = null;

                //Sync folders
                for (int index = 0; index < folderList.Count; index++)
                {
                    tempFileFolderList = await _presenter.SyncFolderAsync(_ticket, _syncDirectory,
                        _group, folderList[index]);

                    _fileFolderList.AddRange(tempFileFolderList);
                }

                //Sync files in the root
                tempFileFolderList = await _presenter.SyncFilesAsync(_ticket, _syncDirectory,
                        _group.id, string.Empty);

                _fileFolderList.AddRange(tempFileFolderList);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}\n\n{1}\n{2}",
                Messages.SynchronizeException, ex.Message, ex.InnerException.Message),
                this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<FileFolderInfo> FileFolderList
        {
            get
            {
                return _fileFolderList;
            }
        }

        internal bool SyncNewUserGroup(string ticket, string syncDirectory, UserGroup group)
        {
            _ticket = ticket;
            _syncDirectory = syncDirectory;
            _group = group;

            lklSyncPath.Text = group.name;

            return this.ShowDialog() == DialogResult.OK;
        }

        public SyncProgressView()
        {
            InitializeComponent();

            _processStarted = true;
            _fileFolderList = new List<FileFolderInfo>();
            _presenter = new SyncProgressPresenter();
        }

        private void SyncProgressView_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !_processStarted;
        }

        private void lklSyncPath_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var syncDirectoryPath = ConfigurationManager.GetValue(Resources.SyncDirectoryPathKey);
            Process.Start(syncDirectoryPath);
        }
    }
}
