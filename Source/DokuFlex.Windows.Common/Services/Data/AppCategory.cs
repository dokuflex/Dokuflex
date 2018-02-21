using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokuFlex.Windows.Common.Services.Data
{
    public class AppCategory
    {
        public string id { get; set; }
        public string userCreated { get; set; }
        public string title { get; set; }
        public string dateCreated { get; set; }
        public string description { get; set; }
        public bool trackChanges { get; set; }
        public int type { get; set; }
        public string userModified { get; set; }
        public string datModified { get; set; }
    }
}
