using DokuFlex.Windows.Common.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileImporter.ViewModels
{
    public class ProgressViewModel
    {
        public string CommunityId { get; set; }
        public string FolderId { get; set; }
        public string DocumentaryId { get; set; }
        public int ItemIndex { get; internal set; }
        public bool Importing { get; set; }
        public Dictionary<string, List<DokuField>> UploadList { get; set; }
    }
}
