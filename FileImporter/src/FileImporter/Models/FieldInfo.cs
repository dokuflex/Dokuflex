using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileImporter.Models
{
    public class FieldInfo
    {
        public string DokuField { get; set; }
        public string SourceField { get; set; }
        public DataType DataType { get; set; }
        public bool IsFilePath { get; set; }
    }
}
