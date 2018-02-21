using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokuFlex.Windows.Common.Services.Data
{
    public class SearchResult : IDocument
    {
        public string id { get; set; }

        public string name { get; set; }

        public double version { get; set; }

        public int size { get; set; }

        public long modifiedTime { get; set; }
    }
}
