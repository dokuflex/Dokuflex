// =================================================================================================================
// Paina Solutions
// DokuFlex
// =================================================================================================================
// ©2013 DokuFlex. All rights reserved. Certain content used with permission from contributors.
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.
// =================================================================================================================

namespace DokuFlex.Scan
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Windows.Forms;
    using DokuFlex.Windows.Common.Log;
    using Windows.Common;

    static class Program
    {
        private static void SetUILanguage()
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

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            // Set the unhandled exception mode to force all Windows Forms errors to go through
            // our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            // Add the event handler for handling non-UI thread exceptions to the event.
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            SetUILanguage();

            Application.Run(new MainForm());
        }

        private static void UIThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var ex = (Exception)e.Exception;
            LogFactory.CreateLog().LogError(ex);
            MessageBox.Show(string.Format("{0}\n\nInformación:\n{1}", ErrorMessages.AsyncTaskError, ex.Message), "Doku4Invoices", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            LogFactory.CreateLog().LogError(ex);
            MessageBox.Show(string.Format("{0}\n\nInformación:\n{1}", ErrorMessages.AsyncTaskError, ex.Message), "Doku4Invoices", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
