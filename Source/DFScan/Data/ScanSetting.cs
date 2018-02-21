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

    public class ScanSetting
    {
        public string Name { get; set; }

        public string Scanner { get; set; }

        public string ColorFormat { get; set; }

        public string FileType { get; set; }

        public float Resolution { get; set; } 

        public bool IsDefault { get; set; }

        private readonly Routing _routing;

        public Routing Routing
        {
            get
            {
                return _routing;
            }
        }

        public ScanSetting()
        {
            _routing = new Routing();
        }
    }
}
