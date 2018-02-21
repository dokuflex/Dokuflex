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
    using DokuFlex.Common;
    using System.Threading.Tasks;

    public class CreateOnlineDirectoryCommand
        : Command
    {
        private string _ticket;
        private string _groupId;
        private string _parentFolderId;
        private string _path;
        private string _topLevelPath;

        public CreateOnlineDirectoryCommand(string ticket, string groupId, string parentFolderId,
            string path, string topLevelPath)
        {
            _ticket = ticket;
            _groupId = groupId;
            _parentFolderId = parentFolderId;
            _path = path;
            _topLevelPath = topLevelPath;
        }

        public override bool IsAsync
        {
            get { return true; }
        }

        protected override async Task<bool> DoExecuteAsync()
        {
            var directory = new DirectoryInfo(_path);

            //Add item to SyncTable and set the SyncProgress state
            var item = new SyncTableItem()
            {
                Name = directory.Name,
                Path = _path,
                LastWriteTime = directory.LastWriteTimeUtc.ToFileTimeUtc(),
                Type = "F",
                GroupId = _groupId,
                FolderId = String.Empty,
                FileId = String.Empty,
                ModifiedTime = 0,
                SyncFolder = false,
            };

            SyncTableManager.Add(item);
            SyncTableManager.Save();

            try
            {
                var nodeId = await DokuFlexService.CreateFolderAsync(_ticket, _groupId, _parentFolderId, directory.Name);

                //Set nodeId property
                item.FolderId = nodeId;
                SyncTableManager.Save();

                return !String.IsNullOrWhiteSpace(nodeId);
            }
            catch (Exception ex)
            {
                //Rollback the Add item action
                SyncTableManager.Remove(item);
                SyncTableManager.Save();

                var newMsg = string.Format("Create online directory, raise an exception with: {0}, Exception: {1}", _path, ex.Message);
                throw new Exception(newMsg);
            }
        }
    }
}
