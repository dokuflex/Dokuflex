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

    public class DeleteDirectoryCommand
        : Command, ICommandFacade
    {
        private string _path;

        public DeleteDirectoryCommand(string path)
        {
            _path = path;
        }

        public override bool IsAsync
        {
            get { return false; }
        }

        protected override void DoExecute()
        {
            try
            {
                Directory.Delete(_path, true);
            }
            catch (Exception ex)
            {
                var newMsg = string.Format("Delete directory, raise an exception with: {0}, Exception: {1}", _path, ex.Message);
                throw new Exception(newMsg);
            }

            SyncTableManager.RemoveAllByPath(_path);
            SyncTableManager.Save();
        }

        public bool CanExecute()
        {
            return Directory.Exists(_path);
        }
    }
}
