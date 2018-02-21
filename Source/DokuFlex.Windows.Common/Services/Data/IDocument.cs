using System;

namespace DokuFlex.Windows.Common.Services.Data
{
    public interface IDocument
    {
        string id { get; set; }

        string name { get; set; }

        double version { get; set; }

        int size { get; set; }

        long modifiedTime { get; set; }
    }
}
