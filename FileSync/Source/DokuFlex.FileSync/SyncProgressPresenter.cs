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
    using System.Collections.Generic;
    using DokuFlex.Common;
    using DokuFlex.Common.ServiceAgents;
    using System.Threading.Tasks;

    class SyncProgressPresenter
    {

        public SyncProgressPresenter()
        {

        }

        public async Task<List<FileFolderInfo>> SyncFolderAsync(string ticket, string syncDir,
            UserGroup group, FileFolder folder)
        {
            var fileFolderList = new List<FileFolderInfo>();
            var folderIndex = 0;
            var currentFolder = new FileFolderInfo()
            {
                GroupId = group.id,
                FolderId = string.Empty,
                FileFolder = folder,
                Path = String.Format("{0}\\{1}", syncDir, folder.name),
                SyncFolder = false
            };

            //Add the first entry from parameter
            fileFolderList.Add(currentFolder);

            //Create directory tree
            do
            {
                currentFolder = fileFolderList[folderIndex];

                if (currentFolder != null)
                {
                    var folders = await DokuFlexService.GetFoldersAsync(ticket, group.id, currentFolder.FileFolder.id);

                    foreach (var folderItem in folders)
                    {
                        fileFolderList.Add(
                            new FileFolderInfo()
                            {
                                GroupId = group.id,
                                FolderId = currentFolder.FileFolder.id,
                                FileFolder = folderItem,
                                Path = string.Format("{0}\\{1}", currentFolder.Path, folderItem.name),
                                SyncFolder = false
                            }
                            );
                    }
                }

                folderIndex++;

            } while (fileFolderList.Count > folderIndex);

            var localFileList = new List<FileFolderInfo>();

            for (int i = 0; i < fileFolderList.Count; i++)
            {
                currentFolder = fileFolderList[i];

                var files = await DokuFlexService.GetFilesAsync(ticket, group.id, currentFolder.FileFolder.id);

                //Files for the current folder
                foreach (var fileItem in files)
                {
                    //Create path
                    var path = string.Format("{0}\\{1}", currentFolder.Path, fileItem.name);

                    //Add entry
                    localFileList.Add(
                        new FileFolderInfo()
                        {
                            GroupId = group.id,
                            FolderId = currentFolder.FileFolder.id,
                            FileFolder = fileItem,
                            Path = path,
                            SyncFolder = false
                        }
                        );
                }
            }

            fileFolderList.AddRange(localFileList);

            return fileFolderList;
        }

        public async Task<List<FileFolderInfo>> SyncFilesAsync(string ticket, string syncDir,
            string groupId, string folderId)
        {
            var fileList = new List<FileFolderInfo>();
            var files = await DokuFlexService.GetFilesAsync(ticket, groupId, folderId);

            //Files for the current folder
            foreach (var fileItem in files)
            {
                //Create path
                var path = string.Format("{0}\\{1}", syncDir, fileItem.name);

                //Add entry
                fileList.Add(
                    new FileFolderInfo()
                    {
                        GroupId = groupId,
                        FolderId = folderId,
                        FileFolder = fileItem,
                        Path = path,
                        SyncFolder = false
                    }
                    );
            }

            return fileList;

        }

        internal async Task<List<FileFolder>> GetFoldersAsync(string ticket, UserGroup group)
        {
            return await DokuFlexService.GetFoldersAsync(ticket, group.id, string.Empty);
        }
    }
}
