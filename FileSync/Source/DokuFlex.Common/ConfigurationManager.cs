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
    using System.IO;
    using System.Xml.Linq;

    public static class ConfigurationManager
    {
        private static string _filename = string.Empty;

        private static XElement _configuration;

        public static void Refresh()
        {
            if (File.Exists(_filename))
            {
                _configuration = XElement.Load(_filename);
            }
            else
            {
                _configuration = new XElement("root",
                    new XElement("LoginUserName", string.Empty),
                    new XElement("LoginPassword", string.Empty),
                    new XElement("LoginRemenberMyIdAndPassword", false.ToString()),
                    new XElement("ProxyUseProxy", false.ToString()),
                    new XElement("ProxyUserName", string.Empty),
                    new XElement("ProxyPassword", string.Empty),
                    new XElement("ProxyDomain", string.Empty),
                    new XElement("RestServiceUrl", "https://secure.dokuflex.com/services/rest"),
                    new XElement("UILanguage", "English (Default)"),
                    new XElement("SyncDirectoryPath", string.Empty),
                    new XElement("AutoSync", false.ToString()),
                    new XElement("SyncInterval", "0"),
                    new XElement("SyncHour", "")
                    );

                _configuration.Save(_filename);
            }
        }

        static ConfigurationManager()
        {
            var _appDataPath = string.Format("{0}\\DokuFlex",
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            if (!Directory.Exists(_appDataPath))
            {
                try
                {
                    Directory.CreateDirectory(_appDataPath);
                }
                catch
                {
                    //Silent exception
                }
            }

            _filename = string.Format("{0}\\Settings.config", _appDataPath);

            Refresh();
        }

        public static void Save()
        {
            _configuration.Save(_filename);
        }

        public static string GetValue(string key)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("Key");
            }

            if (_configuration.Element(key).IsEmpty)
            {
                return string.Empty;
            }

            return _configuration.Element(key).Value;
        }

        public static void SetValue(string key, string value)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("Key");
            }

            _configuration.Element(key).SetValue(value);
        }
    }
}
