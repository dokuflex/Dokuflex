﻿// =================================================================================================================
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
    using DokuFlex.Common;
    using System.Threading.Tasks;

    public class RenameFileCommand
        : Command
    {

        private string _ticket;
        private string _newName;
        private string _fullPath;
        private string _oldFullPath;
        private string _topLevelPath;

        public RenameFileCommand(string ticket, string newName, string fullPath,
            string oldFullPath, string topLevelPath)
        {
            _ticket = ticket;
            _newName = newName;
            _fullPath = fullPath;
            _oldFullPath = oldFullPath;
            _topLevelPath = topLevelPath;
        }

        public override bool IsAsync
        {
            get { return true; }
        }

        protected override async Task<bool> DoExecuteAsync()
        {
            var item = SyncTableManager.GetByPath(_oldFullPath);

            if (item != null)
            {
                item.Name = _newName;
                item.Path = item.Path.Replace(_oldFullPath, _fullPath);
            }
            else
            {
                return false;
            }

            try
            {
                return await DokuFlexService.RenameFileFolderAsync(_ticket, item.GroupId, item.FileId, _newName);
            }
            catch (Exception ex)
            {
                var newMsg = string.Format("Rename file, raise an exception with: {0}, Exception: {1}", item.Path, ex.Message);
                throw new Exception(newMsg);
            }
        }
    }
}
