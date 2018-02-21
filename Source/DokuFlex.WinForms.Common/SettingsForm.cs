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

namespace DokuFlex.WinForms.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    using DokuFlex.WinForms.Common.Resources;
    using DokuFlex.Windows.Common;

    public partial class SettingsForm : Form
    {

        private void LoadConfiguration()
        {
            cbxLenguage.SelectedValue = ConfigurationManager.GetValue(Constants.UILanguage);
            textServerUrl.Text = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);

            textUserName.Text = ConfigurationManager.GetValue(Constants.ProxyUserName);
            textPassword.Text = ConfigurationManager.GetValue(Constants.ProxyPassword);
            textDomain.Text = ConfigurationManager.GetValue(Constants.ProxyDomain);

            var useProxy = false;

            bool.TryParse(ConfigurationManager.GetValue(Constants.UseProxy), out useProxy);

            chkUseProxyServer.Checked = useProxy;
        }

        public void SaveConfiguration()
        {
            ConfigurationManager.SetValue(Constants.UILanguage, cbxLenguage.Text);
            ConfigurationManager.SetValue(Constants.RESTfulServiceUrl, textServerUrl.Text);
            ConfigurationManager.SetValue(Constants.ProxyUserName, textUserName.Text);
            ConfigurationManager.SetValue(Constants.ProxyPassword, textPassword.Text);
            ConfigurationManager.SetValue(Constants.ProxyDomain, textDomain.Text);
            ConfigurationManager.SetValue(Constants.UseProxy, chkUseProxyServer.Checked.ToString());

            ConfigurationManager.Save();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            cbxLenguage.SelectedIndex = 0;

            LoadConfiguration();
        }

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                SaveConfiguration();
            }
        }
    }
}
