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
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class LoginView : Form
    {
        private LoginPresenter _presenter;

        private string _ticket;

        private void DisplayLogingInfo()
        {
            textEmailAddress.Enabled = false;
            textPassword.Enabled = false;
            chkRememberMe.Enabled = false;
            llbForgotMe.Enabled = false;
            btnLogin.Enabled = false;

            lbLoging.Enabled = true;
            lbLoging.Visible = true;
            lbLoging.Text = string.Format(Resources.LogingWithCredentialsText, textEmailAddress.Text);

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

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            LoginPicture.Image = Resources.LoginPicture;

            if (_presenter.RememberMyIdAndPassword && _presenter.Credentials.ContainCredententials())
            {
                DisplayLogingInfo();

                try
                {
                    _ticket = await _presenter.LoginAsync();

                    if (String.IsNullOrWhiteSpace(_ticket))
                    {
                        MessageBox.Show(Resources.LoginFailedText, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        HideLogingInfo();
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("{0}\n\n{1}\n{2}", Messages.LoginFailedException, ex.Message, ex.InnerException.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    HideLogingInfo();
                }
            }
        }

        public string Ticket
        {
            get
            {
                return _ticket;
            }
        }

        public Credentials LoginCredentials
        {
            get
            {
                return _presenter.Credentials;
            }
        }

        public bool ShowLoginDialog()
        {
            return this.ShowDialog() == DialogResult.OK;
        }

        public LoginView()
        {
            InitializeComponent();

            _ticket = String.Empty;
            _presenter = new LoginPresenter();

            textEmailAddress.Text = _presenter.Credentials.UserName;
            textPassword.Text = _presenter.Credentials.Password;
        }

        private void llbForgotMe_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _presenter.ForgotMe();
        }

        private void LoginView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK
                && chkRememberMe.Checked)
            {
                _presenter.Credentials.UserName = textEmailAddress.Text;
                _presenter.Credentials.Password = textPassword.Text;
                _presenter.RememberMyIdAndPassword = chkRememberMe.Checked;
                _presenter.RememberMe();
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            DisplayLogingInfo();

            _presenter.Credentials.UserName = textEmailAddress.Text;
            _presenter.Credentials.Password = textPassword.Text;

            try
            {
                _ticket = await _presenter.LoginAsync();

                if (String.IsNullOrWhiteSpace(_ticket))
                {
                    MessageBox.Show(Resources.LoginFailedText, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    HideLogingInfo();
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}\n\n{1}\n{2}", Messages.LoginFailedException, ex.Message, ex.InnerException.Message), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                HideLogingInfo();
            }
        }
    }
}
