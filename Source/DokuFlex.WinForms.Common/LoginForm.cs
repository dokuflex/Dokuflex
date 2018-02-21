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
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    using DokuFlex.WinForms.Common.Resources;
    using DokuFlex.Windows.Common;
    using DokuFlex.Windows.Common.Services;

    public partial class LoginForm : Form
    {
        private Credentials _credentials;
        private bool _remindMe;
        private string _ticket;

        private bool _taskCancelled;

        private void DisplayLogingInfo()
        {
            textEmailAddress.Enabled = false;
            textPassword.Enabled = false;
            chkRememberMe.Enabled = false;
            llbForgotMe.Enabled = false;
            btnLogin.Enabled = false;

            lbLoging.Enabled = true;
            lbLoging.Visible = true;
            lbLoging.Text = string.Format(StringResources.LogonWithCredentials, textEmailAddress.Text);

            pbLoging.Enabled = true;
            pbLoging.Visible = true;
        }

        private void HideLogingInfo()
        {
            textEmailAddress.Enabled = true;
            textPassword.Enabled = true;
            chkRememberMe.Enabled = true;
            llbForgotMe.Enabled = true;
            btnLogin.Enabled = true;

            lbLoging.Enabled = false;
            lbLoging.Visible = false;

            pbLoging.Enabled = false;
            pbLoging.Visible = false;
        }

        private void LoginAsyncBegin()
        {
            DisplayLogingInfo();

            var taskScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            var task = Task<string>.Factory.StartNew(() => DokuFlexService.Login(_credentials));
            task.ContinueWith(t => LoginAsyncEnd(t.Result), taskScheduler);
            task.ContinueWith(t => TaskAsyncExceptionHandle(t.Exception), new CancellationTokenSource().Token, TaskContinuationOptions.OnlyOnFaulted, taskScheduler);
        }

        private void LoginAsyncEnd(string ticket)
        {
            if (_taskCancelled) return;

            if (string.IsNullOrWhiteSpace(ticket))
            {
                MessageBox.Show(ErrorMessages.LoginError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                HideLogingInfo();
                return;
            }

            _ticket = ticket;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void TaskAsyncExceptionHandle(AggregateException e)
        {
            MessageBox.Show(string.Format("{0}\n\n{1}", ErrorMessages.LoginError, ErrorMessages.CheckSettingsInfo), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            HideLogingInfo();
        }

        private void LoadCredentials()
        {
            _credentials.UserName = ConfigurationManager.GetValue(Constants.LoginUserName);
            _credentials.Password = ConfigurationManager.GetValue(Constants.LoginPassword);

            bool.TryParse(ConfigurationManager.GetValue(Constants.LoginRemindMe), out _remindMe);
        }

        public void ForgotCredentials()
        {
            ConfigurationManager.SetValue(Constants.LoginUserName, string.Empty);
            ConfigurationManager.SetValue(Constants.LoginPassword, string.Empty);
            ConfigurationManager.SetValue(Constants.LoginRemindMe, false.ToString());
            ConfigurationManager.Save();
        }

        public void RememberCredentials()
        {
            ConfigurationManager.SetValue(Constants.LoginUserName, _credentials.UserName);
            ConfigurationManager.SetValue(Constants.LoginPassword, _credentials.Password);
            ConfigurationManager.SetValue(Constants.LoginRemindMe, _remindMe.ToString());
            ConfigurationManager.Save();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoadCredentials();

            LoginPicture.Image = ImageResources.LoginPicture;

            textEmailAddress.Text = _credentials.UserName;
            textPassword.Text = _credentials.Password;
            chkRememberMe.Checked = _remindMe;
        }

        public string Ticket
        {
            get
            {
                return _ticket;
            }
        }

        public bool ShowLoginDialog()
        {
            return this.ShowDialog() == DialogResult.OK;
        }

        public LoginForm()
        {
            InitializeComponent();

            _credentials = new Credentials();
            _ticket = String.Empty;
            _taskCancelled = false;
        }

        private void llbForgotMe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotCredentials();
        }

        private void LoginView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (chkRememberMe.Checked)
                {
                    RememberCredentials();
                }
                else
                {
                    ForgotCredentials();
                }
            }

            _taskCancelled = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DisplayLogingInfo();

            _credentials.UserName = textEmailAddress.Text;
            _credentials.Password = textPassword.Text;

            LoginAsyncBegin();
        }
    }
}
