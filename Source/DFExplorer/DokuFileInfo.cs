

namespace DokuFlex.Explorer
{
    using System;
    using System.Collections.Generic;
    using DokuFlex.Windows.Common.Services.Data;

    public class DokuFileInfo
    {
        public FileFolder FileFolder { get; set; }

        public string DocumentaryType { get; set; }

        public List<DokuField> Metadata { get; set; }

        public DokuFileInfo()
        {
            Metadata = new List<DokuField>();
        }
    }
}
