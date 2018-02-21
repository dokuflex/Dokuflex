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
    using System.Threading.Tasks;

    public class LoginPresenter
    {
        private Credentials _credentials;

        public Credentials Credentials
        {
            get
            {
                return _credentials;
            }
        }

        private bool _RememberMyIdAndPassword;

        public bool RememberMyIdAndPassword
        {
            get
            {
                return _RememberMyIdAndPassword;
            }
            set
            {
                _RememberMyIdAndPassword = value;
            }
        }

        public LoginPresenter()
            : base()
        {
            _credentials = new Credentials();
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            _credentials.UserName = ConfigurationManager.GetValue(Resources.LoginEmailAddressKey);
            _credentials.SetEncryptedPassword(ConfigurationManager.GetValue(Resources.LoginPasswordKey));

            if (!bool.TryParse(ConfigurationManager.GetValue(Resources.LoginRemenberMyIdAndPasswordKey), out _RememberMyIdAndPassword))
            {
                _RememberMyIdAndPassword = false;
            }
        }

        public void ForgotMe()
        {
            ConfigurationManager.SetValue(Resources.LoginEmailAddressKey, string.Empty);
            ConfigurationManager.SetValue(Resources.LoginPasswordKey, string.Empty);
            ConfigurationManager.SetValue(Resources.LoginRemenberMyIdAndPasswordKey, false.ToString());
            ConfigurationManager.Save();
        }

        public void RememberMe()
        {
            ConfigurationManager.SetValue(Resources.LoginEmailAddressKey, _credentials.UserName);
            ConfigurationManager.SetValue(Resources.LoginPasswordKey, _credentials.Password);
            ConfigurationManager.SetValue(Resources.LoginRemenberMyIdAndPasswordKey, _RememberMyIdAndPassword.ToString());
            ConfigurationManager.Save();
        }

        public async Task<string> LoginAsync()
        {
            return await DokuFlexService.LoginAsync(_credentials);
        }

        public string Login()
        {
            return DokuFlexService.Login(_credentials);
        }
    }
}
