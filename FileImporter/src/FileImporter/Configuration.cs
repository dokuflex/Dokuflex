using DokuFlex.Windows.Common.Services.Data;
using FileImporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileImporter
{
    public class Configuration
    {
        public bool ProcessStopped { get; set; }
        public int LastItemProcessed { get; set; }
        public string UserGroupId { get; set; }
        public string DocumentaryTypeId { get; set; }
        public string FolderId { get; set; }
        public Dictionary<string, List<DokuField>> DataFields { get; set; } = new Dictionary<string, List<DokuField>>();
    }
}
