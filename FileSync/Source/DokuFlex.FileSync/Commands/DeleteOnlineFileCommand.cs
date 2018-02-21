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
    using DokuFlex.Common;
    using System.Threading.Tasks;

    public class DeleteOnlineFileCommand
        : Command
    {
        private string _ticket;
        private SyncTableItem _item;

        public DeleteOnlineFileCommand(string ticket, SyncTableItem item)
        {
            _ticket = ticket;
            _item = item;
        }

        public override bool IsAsync
        {
            get { return true; }
        }

        protected override async Task<bool> DoExecuteAsync()
        {
            try
            {
                var result = await DokuFlexService.DeleteFileAsync(_ticket, _item.GroupId, _item.FileId);
                SyncTableManager.Remove(_item);
                SyncTableManager.Save();

                return result;
            }
            catch (Exception ex)
            {
                var newMsg = string.Format("Delete online file, raise an exception with: {0}, Exception: {1}", _item.Path, ex.Message);
                throw new Exception(newMsg);
            }
        }
    }
}
