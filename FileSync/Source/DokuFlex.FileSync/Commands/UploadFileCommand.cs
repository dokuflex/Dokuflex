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
    using DokuFlex.Common.ServiceAgents;
    using System.Threading.Tasks;

    public class UploadFileCommand
        : Command
    {
        private string _ticket;
        private string _groupId;
        private string _folderId;
        private string _fileId;
        private FileInfo _fileInfo;
        private string _topLevelPath;

        public UploadFileCommand(string ticket, string groupId, string folderId,
            string fileId, FileInfo fileInfo, string topLevelPath)
        {
            _ticket = ticket;
            _groupId = groupId;
            _folderId = folderId;
            _fileId = fileId;
            _fileInfo = fileInfo;
            _topLevelPath = topLevelPath;
        }

        public override bool IsAsync
        {
            get { return true; }
        }

        protected override async Task<bool> DoExecuteAsync()
        {
            var sourcePath = Command.SourcePath;
            var targetPath = _fileInfo.FullName;

            //remove this code when possible.
            if (string.Equals(sourcePath, targetPath))
            {
                var newMsg = string.Format("The source file was already processed in a previous call. File: {0}", sourcePath);
                throw new Exception(newMsg);
            }

            var item = SyncTableManager.GetByPath(_fileInfo.FullName);

            if (item == null)
            {
                item = new SyncTableItem()
                {
                    Name = _fileInfo.Name,
                    Path = _fileInfo.FullName,
                    LastWriteTime = 0,
                    Type = "C",
                    GroupId = _groupId,
                    FolderId = _folderId,
                    FileId = _fileId,
                    ModifiedTime = 0,
                    SyncFolder = false
                };

                SyncTableManager.Add(item);
            }
            else
            {
                item.SyncStatus = SyncTableItemStatus.SyncInProgress;
            }

            try
            {
                var uploadInfo = await DokuFlexService.UploadFileAsync(_ticket, _groupId, _folderId, _fileId, _fileInfo, true);

                //Set additional attributes
                item.LastWriteTime = _fileInfo.LastWriteTime.ToFileTimeUtc();
                item.FileId = uploadInfo.nodeId;
                item.ModifiedTime = uploadInfo.modifiedTime;
                item.SyncStatus = SyncTableItemStatus.InSync;

                SyncTableManager.Save();

                //This too!
                Command.SourcePath = _fileInfo.FullName;
                return uploadInfo != null;
            }
            catch (Exception ex)
            {
                var newMsg = string.Format("File: {0}, Exception: {1}", _fileInfo.FullName, ex.Message);
                item.LastWriteTime = 0; //Enable this file to by uploaded again in some point of the time
                item.SyncStatus = SyncTableItemStatus.ErrorConflict;

                throw new Exception(newMsg);
            }
        }
    }
}
