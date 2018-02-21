using System;
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

namespace DokuFlex.Common
{
    using System;
    using System.Collections.Generic;
    using DokuFlex.Common.ServiceAgents;
    using System.Threading.Tasks;

    public class FolderBrowserPresenter
    {
        public List<UserGroup> GetUserGroups(string ticket)
        {
            return DokuFlexService.GetUserGroups(ticket);
        }

        public async Task<List<FileFolder>> GetFoldersAsync(string ticket, string groupId, string parentFolderId)
        {
            return await DokuFlexService.GetFoldersAsync(ticket, groupId, parentFolderId);
        }

        public async Task<string> CreateFolderAsync(string ticket, string groupId, string parentNodeId, string folderName)
        {
            return await DokuFlexService.CreateFolderAsync(ticket, groupId, parentNodeId, folderName);
        }
    }
}
