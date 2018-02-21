using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokuFlex.Windows.Common.Services.Data
{
    public class ListProcessesResponse : RestResponse
    {
        public List<Process> elements { get; set; }

        public ListProcessesResponse()
        {
            elements = new List<Process>();
        }
    }
}
