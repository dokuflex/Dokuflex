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

namespace DokuFlex.FileSync.Commands
{
    using System;
    using System.IO;
    using DokuFlex.Common.ServiceAgents;

    public class CreateDirectoryCommand
        : Command, ICommandFacade
    {
        private string _groupId;
        private FileFolder _fileFolder;
        private string _path;
        private bool _synFolder;
        private string _type;

        public CreateDirectoryCommand(string groupId, FileFolder fileFolder, string path, bool syncFolder = false)
        {
            _groupId = groupId;
            _fileFolder = fileFolder;
            _path = path;
            _synFolder = syncFolder;
            _type = fileFolder.type;
        }

        public CreateDirectoryCommand(string groupId, string path, bool syncFolder = true)
        {
            _groupId = groupId;
            _path = path;
            _synFolder = syncFolder;
            _type = "G";
        }

        public override bool IsAsync
        {
            get { return false; }
        }

        protected override void DoExecute()
        {
            try
            {
                var directoryInfo = Directory.Exists(_path) ? new DirectoryInfo(_path) : Directory.CreateDirectory(_path);
                //Add to sync table
                var item = new SyncTableItem()
                {
                    Name = directoryInfo.Name,
                    Path = directoryInfo.FullName,
                    LastWriteTime = directoryInfo.LastWriteTime.ToFileTime(),
                    Type = _type,
                    GroupId = _groupId,
                    SyncFolder = _synFolder
                };

                if (string.Compare("F", _type) == 0)
                {
                    item.FolderId = _fileFolder.id;
                    item.ModifiedTime = _fileFolder.modifiedTime;
                }

                SyncTableManager.Add(item);
                SyncTableManager.Save();
            }
            catch (Exception ex)
            {
                var newMsg = string.Format("Create directory, raise an exception with: {0}, Exception: {1}", _path, ex.Message);
                throw new Exception(newMsg);
            }
        }

        public bool CanExecute()
        {
            return !Directory.Exists(_path);
        }
    }
}
