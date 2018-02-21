using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

namespace PrinterPlusPlusSetupCustomAction
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            LogHelper.Log("Install Started.");
            LogHelper.Log("Keys:");
            foreach (var key in Context.Parameters.Keys)
            {
                LogHelper.Log(string.Format("{0}: {1}", key.ToString(), Context.Parameters[key.ToString()].ToString()));
            }

            string printerName = "Doku4Signatures";

            if (printerName != string.Empty)
            {
                //string printerName = Context.Parameters["PRINTERNAME"].ToString();
                //string key = Context.Parameters["PROCESSOR"].ToString();
                //string serial = Context.Parameters["SERIALNUMBER"].ToString();

                LogHelper.Log(string.Format("PrinterName: {0}", printerName));
                //LogHelper.Log(string.Format("Processor: {0}", key));
                //LogHelper.Log(string.Format("Serial: {0}", serial));
                try
                {
                    SpoolerHelper sh = new SpoolerHelper();
                    SpoolerHelper.GenericResult result = sh.AddVPrinter(printerName, printerName);
                    if (result.Success == false)
                    {
                        LogError(result.Method, result.Message, result.Exception);
                        throw new InstallException(string.Format("Source: {0}\nMessage: {1}", result.Method, result.Message), result.Exception);
                    }
                    AutorunHelper.AddToStartup();
                }
                catch (Exception ex)
                {
                    LogError("AddVPrinter", ex.Message, ex);
                }
            }
            else
            {
                LogHelper.Log("Incomplete Parameters.");
                throw new InstallException("Incomplete Parameters.");
            }

            LogHelper.Log("Install Finished.");
        }

        private static void LogError(string exceptionSource, string message, Exception innerException)
        {
            string eventMessage = string.Format("Source: {0}\nMessage: {1}\nInnerException: {2}", exceptionSource, message, innerException);
            LogHelper.Log(eventMessage);
        }
    }
}
