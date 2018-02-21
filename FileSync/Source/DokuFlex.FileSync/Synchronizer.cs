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
    using System.Collections.Generic;
    using DokuFlex.FileSync.Commands;
    using DokuFlex.Common;
    using DokuFlex.Common.ServiceAgents;
    using System.Threading.Tasks;

    public class BeforeSynchronizeEventArgs
        : EventArgs
    {

    }

    public class SynchronizationCompletedEventArgs
        : EventArgs
    {

    }

    public class SynchronizeErrorEventArgs
        : EventArgs
    {
        private Exception _exceptionObject;

        public SynchronizeErrorEventArgs(Exception exceptionObject)
        {
            _exceptionObject = exceptionObject;
        }

        public Exception ExceptionObject
        {
            get
            {
                return _exceptionObject;
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _exceptionObject == null ? String.Empty : _exceptionObject.Message;
            }
        }
    }

    public delegate void BeforeSynchronizeEventHandler(object sender, BeforeSynchronizeEventArgs e);

    public delegate void SynchronizationCompletedEventHandler(object sender, SynchronizationCompletedEventArgs e);

    public delegate void SynchronizeErrorEventHandler(object sender, SynchronizeErrorEventArgs e);

    public class Synchronizer
    {
        private bool _paused;
        private string _ticket;
        private string _path;
        private CommandProcessor _commandProcessor;

        private void SynchronizeDeletedDirectories()
        {
            //Synchronize file sync table items of type folder vs on disk directories
            var folders = SyncTableManager.GetFolders();

            //Detect delete directories from disk
            foreach (var folder in folders)
            {
                if (!Directory.Exists(folder.Path))
                {
                    var command = new DeleteOnlineDirectoryCommand(_ticket, folder);
                    command.ExecuteError += new ExecuteErrorEventHandler(OnExecuteError);
                    _commandProcessor.AddCommand(command);
                }
            }
        }

        private async Task<bool> SynchronizeLocalDirectoriesAsync()
        {
            var result = false;
            //Synchronize on disk directories vs file sync table items of type folder
            var directoryPaths = Directory.EnumerateDirectories(_path, "*", SearchOption.AllDirectories);

            //Detect create directories on disk
            foreach (var directoryPath in directoryPaths)
            {
                if (SyncTableManager.ContainsPath(directoryPath)) continue;

                var parentDirectory = Directory.GetParent(directoryPath);
                var parentFolder = SyncTableManager.GetByPath(parentDirectory.FullName);

                if (parentFolder != null)
                {
                    var command = new CreateOnlineDirectoryCommand(_ticket, parentFolder.GroupId, parentFolder.FolderId, directoryPath, _path);
                    command.ExecuteError += new ExecuteErrorEventHandler(OnExecuteError);
                    _commandProcessor.AddCommand(command);
                    result = await _commandProcessor.ExecuteCommandsAsync();
                }
            }

            return result;

        }

        private void SynchronizeLocalFiles()
        {
            //Synchronize file sync table items of type file vs on disk files
            var files = SyncTableManager.GetFiles();

            foreach (var file in files)
            {
                if (File.Exists(file.Path))
                {
                    var fileInfo = new FileInfo(file.Path);

                    if (!fileInfo.IsLocked())
                    {
                        //if on disk file is more new that file table item then... upload
                        if (fileInfo.LastWriteTime.ToFileTime() > file.LastWriteTime)
                        {
                            SyncTableManager.ChangeSyncStatusToPending(file.Path);
                            var command = new UploadFileCommand(_ticket, file.GroupId, file.FolderId, file.FileId, fileInfo, _path);
                            command.ExecuteError += new ExecuteErrorEventHandler(OnExecuteError);
                            _commandProcessor.AddCommand(command);
                        }
                    }
                }
                else
                {
                    SyncTableManager.ChangeSyncStatusToPending(file.Path);
                    //Delete file from file sync table and dokuflex
                    var command = new DeleteOnlineFileCommand(_ticket, file);
                    command.ExecuteError += new ExecuteErrorEventHandler(OnExecuteError);
                    _commandProcessor.AddCommand(command);
                }
            }

            //Synchronize on disk files vs file sync table items of type file
            var filePaths = Directory.EnumerateFiles(_path, "*", SearchOption.AllDirectories);

            foreach (var filePath in filePaths)
            {
                if (filePath.Contains("~")) continue;
                if (filePath.Contains("Thumbs.")) continue;

                //Detect new files and upload
                if (!files.Any(f => f.Path.Equals(filePath)))
                {
                    var fileInfo = new FileInfo(filePath);

                    if (fileInfo.Length == 0) continue;
                    if (fileInfo.IsLocked()) continue;

                    var parentDirectory = Directory.GetParent(filePath);
                    var parentFolder = SyncTableManager.GetByPath(parentDirectory.FullName);

                    if (parentFolder != null)
                    {
                       // Add item as pending
                        var item = new SyncTableItem
                        {
                            Name = fileInfo.Name,
                            Path = fileInfo.FullName,
                            LastWriteTime = 0,
                            Type = "C",
                            GroupId = parentFolder.GroupId,
                            FolderId = parentFolder.FolderId,
                            FileId = string.Empty,
                            ModifiedTime = 0,
                            SyncFolder = false,
                            SyncStatus = SyncTableItemStatus.Pending
                        };

                        SyncTableManager.Add(item);

                        var command = new UploadFileCommand(_ticket, parentFolder.GroupId, parentFolder.FolderId, String.Empty, fileInfo, _path);
                        command.ExecuteError += new ExecuteErrorEventHandler(OnExecuteError);
                        _commandProcessor.AddCommand(command);
                    }
                }
            }
        }

        private async Task SynchronizeOnlineFilesFolders()
        {
            var folders = SyncTableManager.GetFolders();

            foreach (var folder in folders)
            {
                try
                {
                    var filesFolders = await DokuFlexService.GetFilesFoldersAsync(_ticket, folder.GroupId, folder.FolderId);

                    foreach (var fileFolder in filesFolders)
                    {
                        if (fileFolder.type == "C")
                        {
                            //Check if file exists in sync table
                            var file = SyncTableManager.GetByFileId(fileFolder.id);

                            if (file != null)
                            {
                                if (fileFolder.modifiedTime > file.ModifiedTime)
                                {
                                    SyncTableManager.ChangeSyncStatusToPending(file.Path);
                                    var command = new DownloadFileCommand(_ticket, folder.GroupId, folder.FolderId, fileFolder, file.Path, _path);
                                    command.ExecuteError += new ExecuteErrorEventHandler(OnExecuteError);
                                    _commandProcessor.AddCommand(command);
                                }
                            }
                            else
                            {
                                var path = string.Format("{0}\\{1}", folder.Path, fileFolder.name);

                                //Add item as pending
                                var item = new SyncTableItem
                                {
                                    Name = fileFolder.name,
                                    Path = path,
                                    LastWriteTime = 0,
                                    Type = "C",
                                    GroupId = folder.GroupId,
                                    FolderId = folder.FolderId,
                                    FileId = string.Empty,
                                    ModifiedTime = 0,
                                    SyncFolder = false,
                                    SyncStatus = SyncTableItemStatus.Pending
                                };

                                SyncTableManager.Add(item);

                                var command = new DownloadFileCommand(_ticket, folder.GroupId, folder.FolderId, fileFolder, path, _path);
                                command.ExecuteError += new ExecuteErrorEventHandler(OnExecuteError);
                                _commandProcessor.AddCommand(command);
                            }
                        }
                        else
                        {
                            //Check if folder exists
                            if (!folders.Any(f => f.FolderId.Equals(fileFolder.id) && f.Type == "F"))
                            {
                                var path = String.Format("{0}\\{1}", folder.Path, fileFolder.name);
                                var command = new CreateDirectoryCommand(folder.GroupId, fileFolder, path);
                                command.ExecuteError += new ExecuteErrorEventHandler(OnExecuteError);
                                _commandProcessor.AddCommand(command);
                            }
                        }
                    }

                    var files = SyncTableManager.GetFiles(folder.FolderId);

                    foreach (var file in files)
                    {
                        if (!filesFolders.Any(f => f.id.Equals(file.FileId)))
                        {
                            SyncTableManager.ChangeSyncStatusToPending(file.Path);
                            var command = new DeleteFileCommand(file.Path);
                            command.ExecuteError += new ExecuteErrorEventHandler(OnExecuteError);
                            _commandProcessor.AddCommand(command);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Check is folder doesn't exists
                    if (ex is RestResponseException)
                    {
                        var exception = ex as RestResponseException;

                        //Folder doesn't exists
                        if (exception.ErrorCode == 1)
                        {
                            var command = new DeleteDirectoryCommand(folder.Path);
                            command.ExecuteError += new ExecuteErrorEventHandler(OnExecuteError);
                            _commandProcessor.AddCommand(command);
                        }
                    }
                }
            }
        }

        private void OnExecuteError(object sender, ExecuteErrorEventArgs e)
        {
            _paused = false;

            if (SynchronizeError != null)
            {
                SynchronizeError(this, new SynchronizeErrorEventArgs(e.ExceptionObject));
            }
        }

        public event BeforeSynchronizeEventHandler BeforeSynchronize;

        public event SynchronizationCompletedEventHandler SynchronizationCompleted;

        public event SynchronizeErrorEventHandler SynchronizeError;

        public bool Paused
        {
            get
            {
                return _paused;
            }
        }

        public Synchronizer()
        {
            _commandProcessor = new CommandProcessor();
            _ticket = String.Empty;
            _path = String.Empty;
        }

        /// <summary>
        /// Add item list to the queue
        /// </summary>
        /// <param name="fileFolderList"></param>
        public void Synchronize(List<FileFolderInfo> fileFolderList)
        {
            //Create commands
            foreach (var item in fileFolderList)
            {
                if (item.FileFolder == null)
                {
                    var command = new CreateDirectoryCommand(item.GroupId, item.Path, item.SyncFolder);
                    command.ExecuteError += new ExecuteErrorEventHandler(OnExecuteError);
                    _commandProcessor.AddCommand(command);
                }
                else
                    if (item.FileFolder.type == "F")
                    {
                        var command = new CreateDirectoryCommand(item.GroupId, item.FileFolder, item.Path, item.SyncFolder);
                        command.ExecuteError += new ExecuteErrorEventHandler(OnExecuteError);
                        _commandProcessor.AddCommand(command);
                    }
                    else
                    {
                        var command = new DownloadFileCommand(_ticket, item.GroupId, item.FolderId, item.FileFolder, item.Path, _path);
                        command.ExecuteError += new ExecuteErrorEventHandler(OnExecuteError);
                        _commandProcessor.AddCommand(command);
                    }
            }
        }

        /// <summary>
        /// Add command to the queue
        /// </summary>
        /// <param name="command"></param>
        public void Synchronize(Command command)
        {
            _commandProcessor.AddCommand(command);
        }

        public async Task<bool> SynchronizeAsync(Command command)
        {
            if (BeforeSynchronize != null)
            {
                BeforeSynchronize(this, new BeforeSynchronizeEventArgs());
            }

            try
            {
                _commandProcessor.AddCommand(command);

                return await _commandProcessor.ExecuteCommandsAsync();

            }
            finally
            {
                if (SynchronizationCompleted != null)
                {
                    SynchronizationCompleted(this, new SynchronizationCompletedEventArgs());
                }
            }
        }

        public async Task<bool> SynchronizeAsync(string ticket, string topLevelPath)
        {
            _ticket = ticket;
            _path = topLevelPath;

            if (BeforeSynchronize != null)
            {
                BeforeSynchronize(this, new BeforeSynchronizeEventArgs());
            }

            var result = false;

            try
            {
                //Process pending commands...
                _commandProcessor.ExecuteCommands();
                result = await _commandProcessor.ExecuteCommandsAsync();

                //On disk directories vs DokuFlex folders and vice-versa
                SynchronizeDeletedDirectories();
                result = await SynchronizeLocalDirectoriesAsync();
                _commandProcessor.ExecuteCommands();
                result = await _commandProcessor.ExecuteCommandsAsync();

                if (_paused) return false;

                //On disk files vs DokuFlex files and vice versa
                SynchronizeLocalFiles();
                _commandProcessor.ExecuteCommands();
                result = await _commandProcessor.ExecuteCommandsAsync();

                if (_paused) return false;

                //DokuFlex files and folders vs On disk files and directories
                await SynchronizeOnlineFilesFolders();
                _commandProcessor.ExecuteCommands();
                result = await _commandProcessor.ExecuteCommandsAsync();
            }
            catch (Exception ex)
            {
                if (SynchronizeError != null)
                {
                    SynchronizeError(this, new SynchronizeErrorEventArgs(ex));
                }
            }
            finally
            {
                if (SynchronizationCompleted != null)
                {
                    SynchronizationCompleted(this, new SynchronizationCompletedEventArgs());
                }
            }

            return result;
        }

        public async Task<bool> SynchronizeAsync(string ticket, string topLevelPath, List<FileFolderInfo> fileFolderList)
        {
            _ticket = ticket;
            _path = topLevelPath;

            if (BeforeSynchronize != null)
            {
                BeforeSynchronize(this, new BeforeSynchronizeEventArgs());
            }

            Synchronize(fileFolderList);

            if (_commandProcessor.Executing) return true;

            try
            {
                _commandProcessor.ExecuteCommands();
                return await _commandProcessor.ExecuteCommandsAsync();
            }
            finally
            {
                if (SynchronizationCompleted != null)
                {
                    SynchronizationCompleted(this, new SynchronizationCompletedEventArgs());
                }
            }
        }

        public void OnPauseSync(object sender, PauseSyncEventArgs e)
        {
            _paused = true;
            _commandProcessor.Pause();
        }

        public async void OnResumeSync(object sender, ResumeSyncEventArgs e)
        {
            _paused = false;

            if (BeforeSynchronize != null)
            {
                BeforeSynchronize(this, new BeforeSynchronizeEventArgs());
            }

            try
            {
                var result = await _commandProcessor.ExecuteCommandsAsync();
            }
            finally
            {
                if (SynchronizationCompleted != null)
                {
                    SynchronizationCompleted(this, new SynchronizationCompletedEventArgs());
                }
            }
        }
    }
}
