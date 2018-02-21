// =================================================================================================================
// Paina Solutions
// DokuFlex
// =================================================================================================================
// ©2013 DokuFlex. All rights reserved. Certain content used with permission from contributors.
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.
// =================================================================================================================

namespace DokuFlex.Scan.Data
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    using DokuFlex.Windows.Common.Services.Data;

    public class ScannedImage
    {
        private readonly Routing _routing;
        private readonly List<DokuField> _metadata;

        public string Name { get; set; }

        public DateTime DateScanned { get; set; }

        public string FileType { get; set; }

        public int Pages { get; set; }

        public Image Image { get; set; }

        public string Path { get; set; }

        public Routing Routing
        {
            get
            {
                return _routing;
            }
        }

        public List<DokuField> Metadata
        {
            get
            {
                return _metadata;
            }
        }

        public ScannedImage()
        {
            _routing = new Routing();
            _metadata = new List<DokuField>();

            this.Name = DateTime.UtcNow.ToString("s").Replace(":", String.Empty);
            this.DateScanned = DateTime.Now;
            this.Pages = 1;
        }
    }
}
