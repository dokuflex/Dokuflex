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

namespace DokuFlex.Windows.Common
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System.Collections.Generic;

    public static class TrackingListManager
    {
        private static readonly List<TrackingItem> _trackingList;

        static TrackingListManager()
        {
            _trackingList = new List<TrackingItem>();

            LoadFromXml();
        }

        private static void LoadFromXml()
        {
            var appDataPath = string.Format("{0}\\DokuFlex",
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            var xmlPath = string.Format("{0}\\TrackingList.xml", appDataPath);

            if (File.Exists(xmlPath))
            {
                var xmlList = XElement.Load(xmlPath);

                foreach (var element in xmlList.Elements())
                {
                    _trackingList.Add(
                        new TrackingItem()
                        {
                            Name = element.Element("Name").Value,
                            Path = element.Element("Path").Value,
                            LastWriteTime = long.Parse(element.Element("LastWriteTime").Value),
                            Type = element.Element("Type").Value,
                            GroupId = element.Element("GroupId").Value,
                            FolderId = element.Element("FolderId").Value,
                            FileId = element.Element("FileId").Value,
                            ModifiedTime = long.Parse(element.Element("ModifiedTime").Value)
                        }
                        );
                }
            }
        }

        public static void Save()
        {
            var appDataPath = string.Format("{0}\\DokuFlex",
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            var xmlPath = string.Format("{0}\\TrackingList.xml", appDataPath);
            var xmlList = new XElement("root");

            foreach (var item in _trackingList)
            {
                xmlList.Add(
                    new XElement("Tracking",
                        new XElement("Name", item.Name),
                        new XElement("Path", item.Path),
                        new XElement("LastWriteTime", item.LastWriteTime.ToString()),
                        new XElement("Type", item.Type),
                        new XElement("GroupId", item.GroupId),
                        new XElement("FolderId", item.FolderId),
                        new XElement("FileId", item.FileId),
                        new XElement("ModifiedTime", item.ModifiedTime.ToString())
                        )
                    );
            }

            xmlList.Save(xmlPath);
        }

        public static void Reload()
        {
            _trackingList.Clear();
            LoadFromXml();
        }

        public static void Remove(TrackingItem item)
        {
            _trackingList.Remove(item);
        }

        public static TrackingItem GetByPath(string path)
        {
            return _trackingList.FirstOrDefault(f => f.Path.Equals(path));
        }

        public static void Add(TrackingItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            //Check is item exist
            if (_trackingList.IndexOf(item) > 0)
            {
                return;
            }

            _trackingList.Add(item);
        }

        public static TrackingItem GetByFileId(string fileId)
        {
            return _trackingList.FirstOrDefault(f => f.FileId.Equals(fileId));
        }

        public static void Remove(string path)
        {
            var item = GetByPath(path);

            if (item != null)
            {
                _trackingList.Remove(item);
            }
        }

        public static void RemoveAllByPath(string path)
        {
            _trackingList.RemoveAll(f => f.Path.Contains(path));
        }

        public static IEnumerable<TrackingItem> GetAllByPath(string path)
        {
            return _trackingList.Where(f => f.Path.Contains(path));
        }
    }
}
