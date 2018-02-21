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
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;
    using System.Collections.Generic;

    public static class SyncTableManager
    {
        private static readonly List<SyncTableItem> _syncTable;

        static SyncTableManager()
        {
            _syncTable = new List<SyncTableItem>();

            LoadFromXml();
        }

        private static void LoadFromXml()
        {
            var appDataPath = string.Format("{0}\\DokuFlex",
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            var xmlPath = string.Format("{0}\\SyncTable.xml", appDataPath);

            if (File.Exists(xmlPath))
            {
                var xmlList = XElement.Load(xmlPath);

                foreach (var element in xmlList.Elements())
                {
                    _syncTable.Add(
                        new SyncTableItem()
                        {
                            Name = element.Element("Name").Value,
                            Path = element.Element("Path").Value,
                            LastWriteTime = long.Parse(element.Element("LastWriteTime").Value),
                            Type = element.Element("Type").Value,
                            GroupId = element.Element("GroupId").Value,
                            FolderId = element.Element("FolderId").Value,
                            FileId = element.Element("FileId").Value,
                            ModifiedTime = long.Parse(element.Element("ModifiedTime").Value),
                            SyncFolder = bool.Parse(element.Element("SyncFolder").Value),
                            SyncStatus = (SyncTableItemStatus) Enum.Parse(typeof(SyncTableItemStatus), element.Element("SyncStatus").Value)
                        }
                        );
                }
            }
        }

        public static void Save()
        {
            var appDataPath = string.Format("{0}\\DokuFlex",
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            var xmlPath = string.Format("{0}\\SyncTable.xml", appDataPath);
            var xmlList = new XElement("root");

            foreach (var item in _syncTable)
            {
                xmlList.Add(
                    new XElement("FileSyncTableItem",
                        new XElement("Name", item.Name),
                        new XElement("Path", item.Path),
                        new XElement("LastWriteTime", item.LastWriteTime.ToString()),
                        new XElement("Type", item.Type),
                        new XElement("GroupId", item.GroupId),
                        new XElement("FolderId", item.FolderId),
                        new XElement("FileId", item.FileId),
                        new XElement("ModifiedTime", item.ModifiedTime.ToString()),
                        new XElement("SyncFolder", item.SyncFolder.ToString()),
                        new XElement("SyncStatus", item.SyncStatus.ToString())
                        )
                    );
            }

            xmlList.Save(xmlPath);
        }

        public static void Reload()
        {
            _syncTable.Clear();
            LoadFromXml();
        }

        public static IEnumerable<SyncTableItem> GetFolders()
        {
            return _syncTable.Where(f => f.Type == "F");
        }

        public static IEnumerable<SyncTableItem> GetFiles()
        {
            return _syncTable.Where(f => f.Type == "C");
        }

        public static IEnumerable<SyncTableItem> GetFiles(string folderId)
        {
            return _syncTable.Where(f => f.FolderId.Equals(folderId) && f.Type == "C").ToList();
        }

        public static void Remove(SyncTableItem item)
        {
            _syncTable.Remove(item);
        }

        public static SyncTableItem GetByPath(string path)
        {
            return _syncTable.FirstOrDefault(f => f.Path.Equals(path));
        }

        public static void Add(SyncTableItem item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            //Check is item exist
            if (_syncTable.IndexOf(item) > 0) return;

            _syncTable.Add(item);
        }

        public static SyncTableItem GetByFileId(string fileId)
        {
            return _syncTable.FirstOrDefault(f => f.FileId == fileId);
        }

        public static void Remove(string path)
        {
            var item = GetByPath(path);

            if (item != null)
            {
                _syncTable.Remove(item);
            }
        }

        public static void RemoveAllByGroupId(string id)
        {
            _syncTable.RemoveAll(f => f.GroupId.Equals(id));
        }

        public static void RemoveAllByPath(string path)
        {
            _syncTable.RemoveAll(f => f.Path.Contains(path));
        }

        public static IEnumerable<SyncTableItem> GetAllByPath(string path)
        {
            return _syncTable.Where(f => f.Path.Contains(path));
        }

        public static List<SyncTableItem> GetSyncFolders()
        {
            return _syncTable.Where(f => f.SyncFolder).ToList();
        }

        public static bool ContainsFolders()
        {
            return _syncTable.Any(f => f.SyncFolder);
        }

        public static object GetByGroupId(string id)
        {
            return _syncTable.FirstOrDefault(f => f.GroupId.Equals(id));
        }

        public static bool ContainsPath(string path)
        {
            return _syncTable.Any(f => f.Path.Equals(path));
        }

        public static void ChangeSyncStatusToPending(string path)
        {
            var item = GetByPath(path);

            if (item == null) return;

            item.SyncStatus = SyncTableItemStatus.Pending;
        }

        public static void ChangeSyncStatusToInSync(string path)
        {
            var item = GetByPath(path);

            if (item == null) return;

            item.SyncStatus = SyncTableItemStatus.InSync;
        }

        public static void ChangeSyncStatusToSyncInProgress(string path)
        {
            var item = GetByPath(path);

            if (item == null) return;

            item.SyncStatus = SyncTableItemStatus.SyncInProgress;
        }

        public static void ChangeSyncStatusToErrorConflict(string path)
        {
            var item = GetByPath(path);

            if (item == null) return;

            item.SyncStatus = SyncTableItemStatus.ErrorConflict;
        }

        public static IEnumerable<SyncTableItem> GetNotSyncItems()
        {
            return _syncTable.Where(i => i.SyncStatus != SyncTableItemStatus.InSync &&
            i.Type.Equals("C"));
        }
    }
}
