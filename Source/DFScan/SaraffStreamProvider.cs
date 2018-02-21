using Saraff.Twain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokuFlex.Scan
{
    internal sealed class SaraffStreamProvider : Component, IStreamProvider
    {

        public Stream GetStream()
        {
            return new FileStream(Path.GetTempFileName(), FileMode.Create, FileAccess.ReadWrite, FileShare.Read, 64 * 1024, FileOptions.DeleteOnClose);
        }
    }
}
