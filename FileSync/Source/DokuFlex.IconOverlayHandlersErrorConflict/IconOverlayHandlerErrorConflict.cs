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

namespace DokuFlex.IconOverlayHandlersErrorConflict
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Win32;
    using System.IO;
    using DokuFlex.IconOverlayHandlersBase;
    using DokuFlex.FileSync;

    [ComVisible(true)]
    [Guid("48f2ba3c-6a97-46e6-9142-659a6137732a")]
    public class IconOverlayHandlerErrorConflict
        : IconOverlayHandlerBase
    {
        protected override string OverlayIconFilePath
        {
            get
            {
                return Path.Combine(base.OverlayIconFilePath, @"ErrorConflict.ico");
            }
        }

        protected override int Priority
        {
            get
            {
                return 2;  // 0-100 (0 is highest priority)
            }
        }

        protected override SyncTableItemState ItemState
        {
            get
            {
                return SyncTableItemState.ErrorConflict;
            }
        }
    }
}
