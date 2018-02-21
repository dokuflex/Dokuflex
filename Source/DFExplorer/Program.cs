using DokuFlex.Windows.Common.Log;
using DokuFlex.Windows.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DokuFlex.Explorer
{
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
            // Add the event handler for handling UI thread exceptions to the event.
            Application.ThreadException += new ThreadExceptionEventHandler(UIThreadException);
            // Set the unhandled exception mode to force all Windows Forms errors to go through 
            // our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            // Add the event handler for handling non-UI thread exceptions to the event. 
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


            Application.Run(new MainForm());
        }

        private static void UIThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var ex = (Exception)e.Exception;            
            //LogFactory.CreateLog().LogError(ex);
            LogFactory.CreateLog().LogError(ex);
            //DataServiceFactory.Create().SendAppLog(string.Empty, string.Empty, Application.ProductName + " " + Application.ProductVersion, string.Empty, ex.Source, ex.ToString(), ex.StackTrace);
            //log4net.Appender.wr
            MessageBox.Show(string.Format("{0}\n\nInformación:\n{1}", "Ha ocurrido un error mientras se procesaba la acción.", ex.Message), "DokuFlex Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            //LogFactory.CreateLog().LogError(ex);
            LogFactory.CreateLog().LogError(ex);
            //DataServiceFactory.Create().SendAppLog(string.Empty, string.Empty, Application.ProductName + " " + Application.ProductVersion, string.Empty, ex.Source, ex.ToString(), ex.StackTrace);

            MessageBox.Show(string.Format("{0}\n\nInformación:\n{1}", "Ha ocurrido un error mientras se procesaba la acción.", ex.Message), "DokuFlex Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
