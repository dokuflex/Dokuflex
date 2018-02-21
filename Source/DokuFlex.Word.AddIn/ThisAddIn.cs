//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
//
// Copyright (c) Paina Solutions. All right reserved.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
//
//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=

namespace DokuFlex.Word.AddIn
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using Word = Microsoft.Office.Interop.Word;
    using Office = Microsoft.Office.Core;
    using Microsoft.Office.Tools.Word;

    using DokuFlex.Windows.Common;
    using DokuFlex.WinForms.Common;
    using DokuFlex.Windows.Common.Services;
    using DokuFlex.Windows.Common.Log;
    using DokuFlex.Windows.Common.Services.Data;
    using System.Diagnostics;
    using System.Threading;
    using System.Globalization;

    public partial class ThisAddIn
    {
        private bool _tracking;
        private IList<FileExtension> _fileExtensions;
        private bool pendingChanges = false;

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

        private async void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            SetUILanguage();

            _fileExtensions = new List<FileExtension>()
            {
                new FileExtension()
                {
                    DisplayName = "Todas",
                    Extension = ".doc;.docx",
                    ImageIndex = 23
                },
                new FileExtension()
                {
                    DisplayName ="DOCX",
                    Extension = ".docx",
                    ImageIndex = 23
                },
                new FileExtension()
                {
                    DisplayName ="DOC",
                    Extension = ".doc",
                    ImageIndex = 23
                }
            };

            TrackOn();

            Globals.ThisAddIn.Application.DocumentOpen += DocumentOpen;

            await CheckForUpdates();
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
                    System.Diagnostics.Process.Start(appUrl);
                }
            }
        }

        private void DocumentOpen(Word.Document Doc)
        {
            if (!IsTracking) return;

            TrackingListManager.Reload();

            var trackingItem = TrackingListManager.GetByPath(Doc.FullName);

            if (trackingItem == null) return; //Not tracked by DokuFlex yet!

            var ticket = Session.GetTikect();

            if (String.IsNullOrWhiteSpace(ticket)) return;

            var documentMetadata = DokuFlexService.GetDocumentMetadada(ticket, trackingItem.FileId);

            if (documentMetadata == null) return;

            if (trackingItem.ModifiedTime < documentMetadata.dateModified)
            {
                if (MessageBox.Show(String.Format("Existe una versión más reciente de {0} en DokuFlex.\n\n{1}",
                    Doc.Name.ToUpperInvariant(), "¿Desea actualizar el documento con la versión de DokuFlex?"),
                    Globals.ThisAddIn.Application.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var currentPath = Doc.FullName;

                    Doc.Close(false);

                    try
                    {
                        File.Delete(currentPath);
                    }
                    catch (Exception)
                    {
                        return;
                    }

                    using (var form = new TransferProgressForm())
                    {
                        if (form.DownloadFile(ticket, trackingItem.FileId, currentPath))
                        {
                            TrackOff();
                            Globals.ThisAddIn.Application.Documents.Open(currentPath);
                            TrackOn();
                            trackingItem.ModifiedTime = documentMetadata.dateModified;
                            TrackingListManager.Save();
                        }
                    }
                }
            }
        }

        public void Search()
        {
            using (var form = new SearchForm(String.Empty))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }

        public void ShowMetadata()
        {
            TrackingListManager.Reload();

            var trackingItem = TrackingListManager.GetByPath(Globals.ThisAddIn.Application.ActiveDocument.FullName);

            if (trackingItem == null)
            {
                MessageBox.Show(String.Format("El documento no contiene metadatos porque no existe en DokuFlex.\n\n{0}",
                    "Guarde el documento en DokuFlex y repita la acción"),
                    this.Application.Name, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {

                var ticket = Session.GetTikect();

                using (var form = new MetadataForm(ticket, trackingItem.FileId, trackingItem.Name))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        DokuFlexService.UpdateDocumentMetadata(ticket, form.DocumentType,
                                    trackingItem.FileId, form.Metadata.ToArray());
                    }
                }
            }
        }

        private void TrackOn()
        {
            _tracking = true;
        }

        private void TrackOff()
        {
            _tracking = false;
        }

        public bool IsTracking
        {
            get
            {
                return _tracking;
            }
        }

        public async Task AddToFavorites()
        {
            var path = Globals.ThisAddIn.Application.ActiveDocument.FullName;

            TrackingListManager.Reload();

            var trackingItem = TrackingListManager.GetByPath(path);

            if (trackingItem == null)
            {
                MessageBox.Show(String.Format("El documento no se puede añadir a los favoritos porque no existe en DokuFlex.\n\n{0}",
                    "Guarde el documento en DokuFlex y repita la acción"),
                    this.Application.Name, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                var ticket = await Session.GetTikectAsync();
                await DokuFlexService.UpdateFavoriteAsync(ticket, trackingItem.FileId, "A", 1);
            }
        }

        public void SaveDocument()
        {
            var newFile = false;

            if (String.IsNullOrWhiteSpace(this.Application.ActiveDocument.Path))
            {
                if (MessageBox.Show(String.Format("El documento aún no ha sido guardado en disco.\n\n{0}",
                    "¿Desea guardar el documento en su PC para continuar?"),
                    this.Application.Name, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Application.ActiveDocument.Save();

                    newFile = true;

                    if (string.IsNullOrWhiteSpace(this.Application.ActiveDocument.Path)) return;
                }
            }
            else
            {
                if (!this.Application.ActiveDocument.Saved &&
                    MessageBox.Show(String.Format("¿Desea guardar los cambios en {0} antes continuar?",
                    this.Application.ActiveDocument.Name.ToUpperInvariant()),
                    this.Application.Name, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Application.ActiveDocument.Save();
                }
            }

            var ticket = Session.GetTikect();

            if (String.IsNullOrWhiteSpace(ticket)) return;

            var fileId = UploadFile(ticket, this.Application.ActiveDocument.FullName);

            if (newFile && !String.IsNullOrWhiteSpace(fileId))
            {
                using (var form = new MetadataForm(ticket, fileId, this.Application.ActiveDocument.Name))
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        DokuFlexService.UpdateDocumentMetadata(ticket, form.DocumentType,
                                    fileId, form.Metadata.ToArray());
                    }
                }
            }
        }

        private string UploadFile(string ticket, string fullName)
        {
            //Step 1: Create a copy of the file and build the path to there.
            var uploadPath = string.Empty;
            var uploadDir = DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.UploadDirectory);
            var fileName = string.Empty;

            try
            {
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                var fileInfo = new FileInfo(fullName);

                uploadPath = string.Format("{0}\\{1}", uploadDir, fileInfo.Name);
                fileName = fileInfo.Name;

                fileInfo.CopyTo(uploadPath, true);

            }
            catch (Exception ex)
            {
                LogFactory.CreateLog().LogError(ex);
                MessageBox.Show(ex.Message, this.Application.Name, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error);

                return String.Empty;
            }

            //Step 3: Upload the file

            TrackingListManager.Reload();

            var trackingItem = TrackingListManager.GetByPath(fullName);

            if (trackingItem != null)
            {
                using (var form = new TransferProgressForm())
                {
                    if (form.UploadFile(ticket, trackingItem.GroupId, trackingItem.FolderId,
                        trackingItem.FileId, uploadPath, true))
                    {
                        trackingItem.FileId = form.FileId;
                        trackingItem.ModifiedTime = form.ModifiedTime;

                        TrackingListManager.Save();
                    }
                }
            }
            else
            {
                var community = (UserGroup)null;
                var folder = (FileFolder)null;
                var saveAsNewVersion = false;

                using (var saveFileView = new SaveFileForm(ticket))
                {
                    if (saveFileView.ShowSaveFileDialog())
                    {
                        community = saveFileView.Group;
                        folder = saveFileView.Folder;
                        saveAsNewVersion = saveFileView.SaveAsNewVersion;
                    }
                    else
                    {
                        return String.Empty;
                    }
                }

                //Upload file to the server.
                using (var form = new TransferProgressForm())
                {
                    if (form.UploadFile(ticket, community.id,
                        folder.id, string.Empty, uploadPath, saveAsNewVersion))
                    {
                        trackingItem = new TrackingItem()
                        {
                            Name = Path.GetFileName(fullName),
                            LastWriteTime = DateTime.Now.ToFileTimeUtc(),
                            Path = fullName,
                            Type = "F",
                            GroupId = community.id,
                            FolderId = folder.id,
                            FileId = form.FileId,
                            ModifiedTime = form.ModifiedTime
                        };

                        TrackingListManager.Add(trackingItem);
                        TrackingListManager.Save();
                    }
                }
            }

            try
            {
                File.Delete(uploadPath);
            }
            catch (Exception e)
            {
                LogFactory.CreateLog().LogError(e);
            }

            return trackingItem.FileId;
        }

        public void OpenRecentDocument(string ticket, IDocument document)
        {
            TrackingListManager.Reload();

            var trackingItem = TrackingListManager.GetByFileId(document.id);

            if (trackingItem != null)
            {
                if (trackingItem.ModifiedTime < document.modifiedTime)
                {
                    using (var form = new TransferProgressForm())
                    {
                        if (form.DownloadFile(ticket, trackingItem.FileId, trackingItem.Path))
                        {
                            TrackOff();
                            Globals.ThisAddIn.Application.Documents.Open(trackingItem.Path);
                            TrackOn();
                            trackingItem.ModifiedTime = document.modifiedTime;
                            TrackingListManager.Save();
                        }
                    }
                }
                else
                {
                    //Open the document.
                    TrackOff();
                    this.Application.Documents.Open(trackingItem.Path);
                    TrackOn();
                }
            }
            else
            {
                //Step 3: Create directory and file paths
                var currentDir = String.Format("{0}\\{1}", DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.Documents), "Huerfanos");
                var currentPath = string.Format("{0}\\{1}", currentDir, document.name);

                try
                {
                    if (!Directory.Exists(currentDir))
                    {
                        try
                        {
                            Directory.CreateDirectory(currentDir);
                        }
                        catch (Exception)
                        {
                            //Silent exception
                        }
                    }

                }
                catch (Exception e)
                {
                    LogFactory.CreateLog().LogError(e);
                    MessageBox.Show(e.Message, this.Application.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                //Step 4: download the file
                using (var form = new TransferProgressForm())
                {
                    if (form.DownloadFile(ticket, document.id, currentPath))
                    {
                        TrackOff();
                        Globals.ThisAddIn.Application.Documents.Open(currentPath);
                        TrackOn();

                        trackingItem = new TrackingItem()
                        {
                            Name = document.name,
                            LastWriteTime = DateTime.Now.ToFileTimeUtc(),
                            Path = currentPath,
                            Type = "F",
                            FileId = document.id,
                            ModifiedTime = document.modifiedTime
                        };

                        TrackingListManager.Add(trackingItem);
                        TrackingListManager.Save();
                    }
                }
            }
        }

        public void OpenDocument()
        {
            //Step 1: Login to DokuFlex to get the ticket;
            var ticket = Session.GetTikect();

            //Step 2: get addional info
            var community = (UserGroup)null;
            var folder = (FileFolder)null;
            var file = (FileFolder)null;

            using (var form = new OpenFileForm(ticket, _fileExtensions))
            {
                if (form.ShowOpenFileDialog())
                {
                    community = form.Group;
                    folder = form.Folder;
                    file = form.File;
                }
                else
                {
                    return;
                }
            }

            TrackingListManager.Reload();

            var trackingItem = TrackingListManager.GetByFileId(file.id);

            if (trackingItem != null && File.Exists(trackingItem.Path))
            {
                if (trackingItem.ModifiedTime < file.modifiedTime)
                {
                    using (var form = new TransferProgressForm())
                    {
                        if (form.DownloadFile(ticket, trackingItem.FileId, trackingItem.Path))
                        {
                            TrackOff();
                            Globals.ThisAddIn.Application.Documents.Open(trackingItem.Path);
                            TrackOn();
                            trackingItem.ModifiedTime = file.modifiedTime;
                            TrackingListManager.Save();
                        }
                    }
                }
                else
                {
                    //Open the document.
                    TrackOff();
                    this.Application.Documents.Open(trackingItem.Path);
                    TrackOn();
                }
            }
            else
            {
                //Step 3: Create directory and file paths
                var currentDir = String.Format("{0}\\{1}", DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.Documents), community.name);
                var currentPath = string.Format("{0}\\{1}", currentDir, file.name);

                try
                {
                    if (!Directory.Exists(currentDir))
                    {
                        try
                        {
                            Directory.CreateDirectory(currentDir);
                        }
                        catch (Exception)
                        {
                            //Silent exception
                        }
                    }

                }
                catch (Exception e)
                {
                    LogFactory.CreateLog().LogError(e);
                    MessageBox.Show(e.Message, this.Application.Name, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                //Step 4: download the file
                using (var form = new TransferProgressForm())
                {
                    if (form.DownloadFile(ticket, file.id, currentPath))
                    {
                        TrackOff();
                        Globals.ThisAddIn.Application.Documents.Open(currentPath);
                        TrackOn();

                        trackingItem = new TrackingItem()
                        {
                            Name = file.name,
                            LastWriteTime = DateTime.Now.ToFileTimeUtc(),
                            Path = currentPath,
                            Type = "F",
                            GroupId = community.id,
                            FolderId = folder.id,
                            FileId = file.id,
                            ModifiedTime = file.modifiedTime
                        };

                        TrackingListManager.Add(trackingItem);
                        TrackingListManager.Save();
                    }
                }
            }
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
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
