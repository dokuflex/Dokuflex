/*
Printer++ Virtual Printer Processor
Copyright (C) 2012 - Printer++

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

using PrinterPlusPlusSDK;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace PrinterPlusPlus
{
    internal class Program
    {
        private NotifyIcon _notifyIcon;
        private ContextMenu _contextMenu;
        private IOMonitorHelper _iomh;

        public Program()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            _iomh = new IOMonitorHelper();
            _contextMenu = new ContextMenu();

            var exitMenuItem = new MenuItem { Text = Strings.ExitText };
            exitMenuItem.Click += exitMenuItem_Click;

            _contextMenu.MenuItems.AddRange(new MenuItem[]{exitMenuItem});

            _notifyIcon = new NotifyIcon
            {
                ContextMenu = _contextMenu,
                Text = Strings.ApplicationTitleText,
                Icon = Icon.ExtractAssociatedIcon("Doku4Signatures.ico"),
                Visible = true
            };
            //_iomh.StartMonitor(Path.Combine(IO.AppRootDirectory(), "Temp"));
           _iomh.StartMonitor("C:\\Doku4Signatures\\Temp");
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            if (_iomh != null)
                _iomh.StopMonitor();

            Application.Exit();
        }

        private void changeCredentialsMenuItem_Click(object sender, EventArgs e)
        {

        }

        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            new Program();

            Application.ThreadException += Application_ThreadException;
            //Application.Run(new frmMain(silentMode, noMonitor, key, fileName));
            Application.Run();
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            IO.Log(e.Exception);

            MessageBox.Show("Ha ocurrido un error mientras se procesaba la solicitud, intentelo nuevamente.");
        }
    }
}
