// Copyright (c) Dokuflex, Co. All rights reserved.
// See License.txt in the project root for license information.

namespace Dokuflex.Doku4Signatures
{
    using System;
    using System.Windows.Forms;
    using WinControls;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PdfSignerForm());
        }
    }
}
