using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokuFlex.Windows.Common.Services.Data
{
    public class GetAppInfoResponse : 
        RestResponse
    {
        public string id { get; set; }

        public string version { get; set; }

        public List<AppInfo> files { get; set; }

        public int total { get; set; }

        public GetAppInfoResponse()
        {
            files = new List<AppInfo>();
        }
    }
}
