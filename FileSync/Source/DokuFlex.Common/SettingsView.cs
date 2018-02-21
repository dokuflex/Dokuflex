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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class SettingsView : Form
    {
        private SettingsPresenter _presenter;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            textServerUrl.Text = _presenter.RestServiceUrl;
            textUserName.Text = _presenter.Credentials.UserName;
            textPassword.Text = _presenter.Credentials.Password;
            textDomain.Text = _presenter.Credentials.Domain;
            chkUseProxyServer.Checked = _presenter.UseProxyServer;
            cbxLenguage.SelectedIndex = cbxLenguage.Items.IndexOf(_presenter.UILanguage);
            chkAutoSync.Checked = _presenter.AutoSync;
            cbxSyncInterval.SelectedIndex = _presenter.SyncInterval;
            dtpHour.Value = string.IsNullOrWhiteSpace(_presenter.SyncHour) ? DateTime.Now : DateTime.Parse(_presenter.SyncHour);
        }

        public SettingsView()
        {
            InitializeComponent();

            _presenter = new SettingsPresenter();
        }

        private void SettingsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (string.Compare(_presenter.UILanguage, cbxLenguage.Text) != 0)
                {
                    MessageBox.Show(Resources.LanguageChangedText, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                _presenter.UILanguage = cbxLenguage.Text;
                _presenter.RestServiceUrl = textServerUrl.Text;
                _presenter.UseProxyServer = chkUseProxyServer.Checked;
                _presenter.Credentials.UserName = textUserName.Text;
                _presenter.Credentials.Password = textPassword.Text;
                _presenter.Credentials.Domain = textDomain.Text;
                _presenter.AutoSync = chkAutoSync.Checked;
                _presenter.SyncInterval = cbxSyncInterval.SelectedIndex;
                _presenter.SyncHour = dtpHour.Value.ToShortTimeString();
                _presenter.SaveConfiguration();
            }
        }

        private void cbxSyncInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbResetApp.Visible = _presenter.SyncInterval != cbxSyncInterval.SelectedIndex
                || _presenter.AutoSync != chkAutoSync.Checked;

            dtpHour.Enabled = cbxSyncInterval.SelectedIndex == 6;
        }

        private void chkAutoSync_CheckedChanged(object sender, EventArgs e)
        {
            lbResetApp.Visible = _presenter.SyncInterval != cbxSyncInterval.SelectedIndex
               || _presenter.AutoSync != chkAutoSync.Checked;

            cbxSyncInterval.Enabled = chkAutoSync.Checked;
        }
    }
}
