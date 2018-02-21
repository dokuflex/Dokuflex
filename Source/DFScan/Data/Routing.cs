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
    public class Routing
    {
        public string Documentary { get; set; }

        public string DocumentaryName { get; set; }

        public int Homologation { get; set; }

        public bool ConvertToPdf { get; set; }

        public string Certificate { get; set; }

        public string CertificateName { get; set; }

        public string Community { get; set; }

        public string CommunityName { get; set; }
      
        public string Folder { get; set; }

        public string FolderPath { get; set; }

        public string FileId { get; set; }

        public void Assign(Routing routing)
        {
            this.Documentary = routing.Documentary;
            this.DocumentaryName = routing.DocumentaryName;
            this.Certificate = routing.Certificate;
            this.CertificateName = routing.CertificateName;
            this.ConvertToPdf = routing.ConvertToPdf;
            this.Community = routing.Community;
            this.CommunityName = routing.CommunityName;
            this.Folder = routing.Folder;
            this.FolderPath = routing.FolderPath;
        }
    }
}
