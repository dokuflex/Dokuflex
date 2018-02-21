// =================================================================================================================
// Paina Solutions
// DokuFlex
// =================================================================================================================
// ©2013 DokuFlex. All rights reserved. Certain content used with permission from contributors.
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is 
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and limitations under the License.
// =================================================================================================================

namespace DokuFlex.Scan.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System.Collections.Generic; 

    public class ScanSettingsManager
    {
        private readonly List<ScanSetting> _settings;

        public ScanSettingsManager()
        {
            _settings = new List<ScanSetting>();
            LoadFormXml();
        }

        public List<ScanSetting> Settings
        {
            get
            {
                return _settings;
            }
        }

        public void Add(ScanSetting item)
        {
            _settings.Add(item);
        }

        public void Remove(ScanSetting item)
        {
            _settings.Remove(item);
        }

        public string GetDefaultSettingName()
        {
            var setting = _settings.FirstOrDefault(s => s.IsDefault);

            return setting != null ? setting.Name : string.Empty;
        }

        public IEnumerable<string> GetSettingNames()
        {
            return _settings.Select(s => s.Name);
        }

        public ScanSetting GetSettingByName(string name)
        {
            return _settings.FirstOrDefault(s => s.Name.Equals(name));
        }

        public void SaveToXml()
        {
            var xml = new XElement("root");

            foreach (var item in _settings)
            {
                xml.Add(new XElement("Setting",
                    new XElement("Name", item.Name),
                    new XElement("Scanner", item.Scanner),
                    new XElement("Color", item.ColorFormat),
                    new XElement("FileType", item.FileType),
                    new XElement("Resolution", item.Resolution.ToString()),
                    new XElement("IsDefault", item.IsDefault.ToString()),
                    new XElement("Routing", new XElement("Documentary", item.Routing.Documentary),
                        new XElement("DocumentaryName", item.Routing.DocumentaryName),
                        new XElement("Homologation", item.Routing.Homologation.ToString()),
                        new XElement("Certificate", item.Routing.Certificate),
                        new XElement("CertificateName", item.Routing.CertificateName),
                        new XElement("ConvertToPdf", item.Routing.ConvertToPdf.ToString()),
                        new XElement("Community", item.Routing.Community),
                        new XElement("CommunityName", item.Routing.CommunityName),
                        new XElement("Folder", item.Routing.Folder),
                        new XElement("FolderPath", item.Routing.FolderPath))
                    ));

            }

            var path = string.Format("{0}\\DokuFlex\\ScanSettings.xml",
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            xml.Save(path);
        }

        public void LoadFormXml()
        {
            var path = string.Format("{0}\\DokuFlex\\ScanSettings.xml",
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            if (!File.Exists(path)) return;

            var scanSetting = (ScanSetting)null;
            var xml = XElement.Load(path);

            foreach (var element in xml.Elements("Setting"))
            {
                scanSetting = new ScanSetting()
                {
                    Name = element.Element("Name").Value,
                    Scanner = element.Element("Scanner").Value,
                    ColorFormat = element.Element("Color").Value,
                    FileType = element.Element("FileType").Value,
                    Resolution = float.Parse(element.Element("Resolution").Value),
                    IsDefault = bool.Parse(element.Element("IsDefault").Value)
                };

                scanSetting.Routing.Documentary = element.Element("Routing").Element("Documentary").Value;
                scanSetting.Routing.DocumentaryName = element.Element("Routing").Element("DocumentaryName").Value;
                scanSetting.Routing.Homologation = int.Parse(element.Element("Routing").Element("Homologation").Value);
                scanSetting.Routing.Certificate = element.Element("Routing").Element("Certificate").Value;
                scanSetting.Routing.CertificateName = element.Element("Routing").Element("CertificateName").Value;
                scanSetting.Routing.ConvertToPdf = bool.Parse(element.Element("Routing").Element("ConvertToPdf").Value);
                scanSetting.Routing.Community = element.Element("Routing").Element("Community").Value;
                scanSetting.Routing.CommunityName = element.Element("Routing").Element("CommunityName").Value;
                scanSetting.Routing.Folder = element.Element("Routing").Element("Folder").Value;
                scanSetting.Routing.FolderPath = element.Element("Routing").Element("FolderPath").Value;

                this.Add(scanSetting);
            }
        }       
    }
}
