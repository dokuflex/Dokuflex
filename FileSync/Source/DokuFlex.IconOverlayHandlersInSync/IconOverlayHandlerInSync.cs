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

namespace DokuFlex.IconOverlayHandlersInSync
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Win32;
    using System.IO;
    using DokuFlex.IconOverlayHandlersBase;
    using DokuFlex.FileSync;

    [ComVisible(true)]
    [Guid("6fd1152e-c95d-465b-8b51-e9ad20556ace")]
    public class IconOverlayHandlerInSync
        : IconOverlayHandlerBase
    {
        protected override string OverlayIconFilePath
        {
            get
            {
                return Path.Combine(base.OverlayIconFilePath, @"InSync.ico");
            }
        }
    }
}
