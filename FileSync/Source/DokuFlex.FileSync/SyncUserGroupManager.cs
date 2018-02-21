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
    using DokuFlex.Common.ServiceAgents;

    public static class SyncUserGroupManager
    {
        private static readonly List<UserGroup> _userGroups;

        static SyncUserGroupManager()
        {
            _userGroups = new List<UserGroup>();

            LoadFromXml();
        }

        private static void LoadFromXml()
        {
            var appDataPath = string.Format("{0}\\DokuFlex",
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            var xmlPath = string.Format("{0}\\SyncUserGroup.xml", appDataPath);

            if (File.Exists(xmlPath))
            {
                var xmlList = XElement.Load(xmlPath);

                foreach (var element in xmlList.Elements())
                {
                    _userGroups.Add(
                        new UserGroup()
                        {
                            id = element.Element("Id").Value,
                            name = element.Element("Name").Value
                        }
                        );
                }
            }
        }

        public static void Save()
        {
            var appDataPath = string.Format("{0}\\DokuFlex",
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
            var xmlPath = string.Format("{0}\\SyncUserGroup.xml", appDataPath);
            var xmlList = new XElement("root");

            foreach (var item in _userGroups)
            {
                xmlList.Add(
                    new XElement("UserGroup",
                        new XElement("Name", item.name),
                        new XElement("Id", item.id)
                        )
                    );
            }

            xmlList.Save(xmlPath);
        }

        public static void Reload()
        {
            _userGroups.Clear();
            LoadFromXml();
        }

        public static IList<UserGroup> GetAll()
        {
            return _userGroups.ToList();
        }

        public static void Remove(UserGroup userGroup)
        {
            _userGroups.Remove(userGroup);
        }

        public static void Remove(string id)
        {
            _userGroups.RemoveAll(ug => ug.id.Equals(id));
        }

        public static void Add(UserGroup userGroup)
        {
            if (userGroup == null)
            {
                throw new ArgumentNullException("userGroup");
            }

            //Check is item exist
            if (_userGroups.IndexOf(userGroup) > 0)
            {
                return;
            }

            _userGroups.Add(userGroup);
        }

        public static UserGroup GetById(string id)
        {
            return _userGroups.FirstOrDefault(ug => ug.id.Equals(id));
        }

        internal static bool ContainsFolders()
        {
            return _userGroups.Count > 0;
        }
    }
}
