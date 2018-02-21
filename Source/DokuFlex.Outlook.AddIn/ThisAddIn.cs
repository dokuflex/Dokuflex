using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Threading.Tasks;
using DokuFlex.Windows.Common.Services;
using System.Windows.Forms;
using System.Diagnostics;
using DokuFlex.WinForms.Common;
using DokuFlex.Windows.Common;
using System.IO;
using DokuFlex.Windows.Common.Log;
using System.Threading;
using System.Globalization;

namespace DokuFlex.Outlook.AddIn
{
    public partial class ThisAddIn
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

        private Microsoft.Office.Interop.Outlook.MailItem GetMailItem()
        {
            return Globals.ThisAddIn.Application.ActiveExplorer().Selection[1] as Microsoft.Office.Interop.Outlook.MailItem;
        }

        public bool IsMailItem
        {
            get
            {
                return Globals.ThisAddIn.Application.ActiveExplorer().Selection[1] as Microsoft.Office.Interop.Outlook.MailItem != null;
            }
        }

        private async void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            SetUILanguage();
            await CheckForUpdates();
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        private async Task CheckForUpdates()
        {
            var service = DataServiceFactory.Create();
            var appUrl = String.Empty;
            float appVersion = 0;

            try
            {
                var appInfo = await service.GetAppInfoAsync("addin_office");
                appVersion = float.Parse(appInfo.version);
                appUrl = appInfo.files.FirstOrDefault(f => f.name.StartsWith("wepo_addins_setup")).url;
            }
            catch (Exception)
            {
                //Silent exception;
            }

            var currentAppVersion = float.Parse(System.Configuration.ConfigurationManager.AppSettings.Get("AppVersion"));

            if (appVersion > currentAppVersion)
            {
                if (MessageBox.Show("Hay disponible una nueva versión de DokuFlex Addins for Office!\n\n ¿Desea descargarla?", "DokuFlex",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Process.Start(appUrl);
                }
            }
        }

        public void UploadEmailAsEmlFile()
        {
            var ticket = Session.GetTikect();
            var groupId = string.Empty;
            var folderId = string.Empty;

            using (var folderBrowse = new BrowseFolderForm(ticket))
            {
                if (folderBrowse.ShowFolderBrowserDialog())
                {
                    groupId = folderBrowse.Group.id;
                    folderId = folderBrowse.Folder.id;
                }
                else
                {
                    return;
                }
            }

            var fileName = string.Format("{0}.msg", GetMailItem().Subject);
            var uploadDirectory = DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.UploadDirectory);
            var uploadPath = string.Format("{0}\\{1}", uploadDirectory, fileName);

            try
            {
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }

                //Delete any file withing directory with the same name and extension.
                if (File.Exists(uploadPath))
                {
                    File.Delete(uploadPath);
                }

            }
            catch (Exception e)
            {
                LogFactory.CreateLog().LogError(e);
                throw;
            }

            //Save attach to the uploadDirectory
            GetMailItem().SaveAs(uploadPath);

            var fileInfo = new FileInfo(uploadPath);

            using (var form = new TransferProgressForm())
            {
                if (form.UploadFile(ticket, groupId, folderId, String.Empty, uploadPath, false))
                {
                    File.Delete(uploadPath);
                }
            }
        }

        private bool BrowseForFolder(string ticket)
        {
            

            return true;
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
