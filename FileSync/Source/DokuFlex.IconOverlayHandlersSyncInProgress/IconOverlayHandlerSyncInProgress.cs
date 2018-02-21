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

namespace DokuFlex.IconOverlayHandlersSyncInProgress
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Win32;
    using System.IO;
    using DokuFlex.IconOverlayHandlersBase;
    using DokuFlex.FileSync;

    [ComVisible(true)]
    [Guid("80411425-fa0a-4bef-ad48-cf62af147a6a")]
    public class IconOverlayHandlerSyncInProgress
        : IconOverlayHandlerBase
    {
        protected override string OverlayIconFilePath
        {
            get
            {
                return Path.Combine(base.OverlayIconFilePath, @"SyncInProgress.ico");
            }
        }

        protected override int Priority
        {
            get
            {
                return 1;  // 0-100 (0 is highest priority)
            }
        }

        protected override SyncTableItemState ItemState
        {
            get
            {
                return SyncTableItemState.SynInProgress;
            }
        }
    }
}
