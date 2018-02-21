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

namespace DokuFlex.Windows.Common
{
    using System;

    public class TrackingItem
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public long LastWriteTime { get; set; }

        public string Type { get; set; }

        public string GroupId { get; set; }

        public string FolderId { get; set; }

        public string FileId { get; set; }

        public long ModifiedTime { get; set; }
    }
}
