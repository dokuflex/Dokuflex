using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokuFlex.Windows.Common.Services.Data
{
    public class Process
    {
        public string defaultFormId { get; set; }
        public string idTemplate { get; set; }
        public string initialLayoutId { get; set; }
        public int type { get; set; }
        public string version { get; set; }
        public string id { get; set; }
        public AppCategory appCategory { get; set; }
        public string title { get; set; }
        public int linkable { get; set; }
        public string appOrigen { get; set; }
        public int active { get; set; }
        public string communityId { get; set; }
    }
}
