using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokuFlexVPrinter
{
    using Ghostscript.NET;

    public class GsStdio : GhostscriptStdIO
    {
        public GsStdio() : base(true, true, true) { }

        public override void StdIn(out string input, int count)
        {
            input = string.Empty;
        }

        public override void StdOut(string output)
        {
            System.Diagnostics.Debug.WriteLine("GS out: " + output);
        }

        public override void StdError(string error)
        {
            System.Diagnostics.Debug.WriteLine("GS err: " + error);
        }
    }
}
