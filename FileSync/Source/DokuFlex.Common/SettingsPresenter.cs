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
    using System.Net;

    public class SettingsPresenter
    {
        public bool AutoSync { get; set; }
        public string UILanguage { get; set; }
        public string RestServiceUrl { get; set; }
        public NetworkCredential Credentials { get; protected set; }
        public bool UseProxyServer { get; set; }
        public int SyncInterval { get; set; }
        public string SyncHour { get; set; }

        private void LoadConfiguration()
        {
            UILanguage = ConfigurationManager.GetValue(Resources.UILanguageKey);
            RestServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            AutoSync = bool.Parse(ConfigurationManager.GetValue(Resources.AutoSync));
            SyncInterval = int.Parse(ConfigurationManager.GetValue(Resources.SyncInterval));
            SyncHour = ConfigurationManager.GetValue(Resources.SyncHour);
            Credentials.UserName = ConfigurationManager.GetValue(Resources.ProxyUserNameKey);
            Credentials.Password = ConfigurationManager.GetValue(Resources.ProxyPasswordKey);
            Credentials.Domain = ConfigurationManager.GetValue(Resources.ProxyDomainKey);
            UseProxyServer = bool.Parse(ConfigurationManager.GetValue(Resources.ProxyUseProxyKey));
        }

        public SettingsPresenter()
        {
            Credentials = new NetworkCredential();

            LoadConfiguration();
        }

        public void SaveConfiguration()
        {
            ConfigurationManager.SetValue(Resources.UILanguageKey, UILanguage);
            ConfigurationManager.SetValue(Resources.RestServiceUrlKey, RestServiceUrl);
            ConfigurationManager.SetValue(Resources.ProxyUserNameKey, Credentials.UserName);
            ConfigurationManager.SetValue(Resources.ProxyPasswordKey, Credentials.Password);
            ConfigurationManager.SetValue(Resources.ProxyDomainKey, Credentials.Domain);
            ConfigurationManager.SetValue(Resources.ProxyUseProxyKey, UseProxyServer.ToString());
            ConfigurationManager.SetValue(Resources.AutoSync, AutoSync.ToString());
            ConfigurationManager.SetValue(Resources.SyncHour, SyncHour);
            ConfigurationManager.SetValue(Resources.SyncInterval, SyncInterval.ToString());
            ConfigurationManager.Save();
        }
    }
}
