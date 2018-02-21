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

    public class DownloadFileCommand
        : Command
    {
        private string _ticket;
        private string _groupId;
        private string _folderId;
        private FileFolder _fileFolder;
        private string _filePath;
        private string _topLevelPath;

        public DownloadFileCommand(string ticket, string groupId, string folderId,
            FileFolder fileFolder, string filePath, string topLevelPath)
        {
            _ticket = ticket;
            _groupId = groupId;
            _folderId = folderId;
            _fileFolder = fileFolder;
            _filePath = filePath;
            _topLevelPath = topLevelPath;
        }

        public override bool IsAsync
        {
            get { return true; }
        }

        protected override async Task<bool> DoExecuteAsync()
        {
            try
            {
                //Create the target file to download
                File.Create(_filePath).Close();

                var item = SyncTableManager.GetByPath(_filePath);

                if (item == null)
                {
                    //Add to file sync table
                    item = new SyncTableItem()
                    {
                        Name = string.Empty,
                        Path = _filePath,
                        LastWriteTime = 0,
                        Type = _fileFolder.type,
                        GroupId = _groupId,
                        FolderId = _folderId,
                        FileId = _fileFolder.id,
                        ModifiedTime = _fileFolder.modifiedTime,
                        SyncFolder = false
                    };

                    SyncTableManager.Add(item);
                }
                else
                {
                    item.FileId = _fileFolder.id;
                    item.ModifiedTime = _fileFolder.modifiedTime;
                    item.SyncStatus = SyncTableItemStatus.SyncInProgress;
                }

                var result = await DokuFlexService.DownloadFileAsync(_ticket, _fileFolder.id, _filePath);
                var fileInfo = new FileInfo(_filePath);

                item.Name = fileInfo.Name;
                item.LastWriteTime = fileInfo.LastWriteTime.ToFileTime();
                item.SyncStatus = SyncTableItemStatus.InSync;

                SyncTableManager.Save();

                return result;
            }
            catch (Exception ex)
            {
                if (File.Exists(_filePath))
                {
                    var fileInfo = new FileInfo(_filePath);
                    var item = SyncTableManager.GetByPath(_filePath);
                    item.Name = fileInfo.Name;
                    item.LastWriteTime = fileInfo.LastWriteTime.ToFileTime();
                    item.SyncStatus = SyncTableItemStatus.ErrorConflict;
                    SyncTableManager.Save();
                }
                else
                {
                    var newMsg = string.Format("Download file, raise an exception with: {0}, Exception: {1}", _filePath, ex.Message);
                    throw new Exception(newMsg);
                }
            }

            return false;
        }
    }
}
