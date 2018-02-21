// =================================================================================================================
// DokuFlex
// =================================================================================================================
// ©2013 DokuFlex. All rights reserved. Certain content used with permission from contributors
// http://www.dokuflex.com/allwinproducts/license/contributors
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.
// =================================================================================================================

namespace DokuFlex.FileSync
{
    using System;
    using System.IO;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Timers;
    using DokuFlex.FileSync.Commands;
    using DokuFlex.Common;
    using DokuFlex.Common.ServiceAgents;
    using System.Diagnostics;
    using System.Threading;
    using System.Globalization;
    using Microsoft.VisualBasic.ApplicationServices;

    internal class Program
    {
        private void SetUILanguage()
        {
            var uiLanguage = ConfigurationManager.GetValue("UILanguage");

            switch (uiLanguage)
            {
                case "Spanish":

                case "Español":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");
                    break;

                default:
                    break;
            }
        }

        public Program()
        {
            SetUILanguage();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            new Program();

            string[] args = Environment.GetCommandLineArgs();

            SingleInstanceController controller = new SingleInstanceController();
            controller.Run(args);
        }
    }

    public class SingleInstanceController : WindowsFormsApplicationBase
    {
        public SingleInstanceController()
        {
            IsSingleInstance = true;

            StartupNextInstance += this_StartupNextInstance;
        }

        async void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            if (e.CommandLine.Count > 1)
            {
                var main = MainForm as Main;
                await main.SyncANewUserGroupAsync(e.CommandLine[1], e.CommandLine[2],
                    e.CommandLine[3]);
            }
        }

        protected override async void OnCreateMainForm()
        {
            MainForm = new Main();

            if (this.CommandLineArgs.Count > 1)
            {
                var main = MainForm as Main;
                await main.SyncANewUserGroupAsync(this.CommandLineArgs[1], this.CommandLineArgs[2],
                    this.CommandLineArgs[3]);
            }
        }

        protected override bool OnUnhandledException(Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Ha ocurrido un error. Por favor repita la acción.");

            return true;
        }
    }
}
