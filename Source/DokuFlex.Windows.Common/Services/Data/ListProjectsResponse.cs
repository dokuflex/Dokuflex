using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokuFlex.Windows.Common.Services.Data
{
    public class ListProjectsResponse
        : RestResponse
    {
        public List<Project> elements { get; set; }

        public ListProjectsResponse()
        {
            elements = new List<Project>();
        }
    }
}
