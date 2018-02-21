//=======================================================================================
// PulsarSoft Inc.
//=======================================================================================
// EL SOFTWARE SE ENTREGA "TAL CUAL", SIN GARANTÍA DE NINGÚN TIPO, EXPRESAS O IMPLÍCITAS,
// INCLUYENDO PERO NO LIMITADAS A LAS GARANTÍAS DE COMERCIALIZACIÓN, APTITUD PARA UN
// PROPÓSITO PARTICULAR Y NO INFRACCIÓN. EN NINGÚN CASO, LOS AUTORES O TITULARES DEL
// COPYRIGHT SERÁN RESPONSABLES POR CUALQUIER RECLAMACIÓN, DAÑO U OTRA RESPONSABILIDAD,
// YA SEA EN UNA ACCIÓN DE CONTRATO, AGRAVIO O CUALQUIER OTRA FORMA, QUE SURJAN DE O EN
// CONEXION CON EL SOFTWARE O EL USO U OTROS TRATOS EN EL SOFTWARE.
//=======================================================================================
// Copyright (c) PulsarSoft Inc. Reservados todos los derechos.
// Este código es liberado bajo los términos de la licencia Apache v2.0,
// vea el archivo de texto licencia-es.txt para más información.
//=======================================================================================

namespace DokuFlex.Outlook.AddIn
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Configuration;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Outlook = Microsoft.Office.Interop.Outlook;

    using DokuFlex.Windows.Common;
    using DokuFlex.Windows.Common.Services;
    using DokuFlex.Windows.Common.Log;
    using DokuFlex.Windows.Common.Services.Data;
    using DokuFlex.WinForms.Common;
    using System.Threading.Tasks;

    public class UploadEMailPresenter
    {
        private Outlook.MailItem _mailItem;

        private string _ticket;

        private string _groupId;

        private string _folderId;

        private string _fullPath;

        public string FullPath
        {
            get
            {
                return _fullPath;
            }
        }

        public string ReceivedTime
        {
            get
            {
                return _mailItem.ReceivedTime.ToLongDateString();
            }
        }

        public string From
        {
            get
            {
                return _mailItem.SenderName;
            }
        }

        public string Subject
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_mailItem.Subject))
                {
                    return String.Empty;
                }
                else
                {
                    if (_mailItem.Subject.Length > 210)
                    {
                        return _mailItem.Subject.Remove(211);
                    }

                    return _mailItem.Subject;
                }
            }
        }

        public string Ticket
        {
            get
            {
                return _ticket;
            }
        }

        public string ProcessId { get; set; }

        public UploadEMailPresenter()
        {
            if (Globals.ThisAddIn.Application.ActiveExplorer().Selection.Count > 0)
            {
                _mailItem = Globals.ThisAddIn.Application.ActiveExplorer().Selection[1] as Outlook.MailItem;
            }
            else
            {
                _mailItem = null;
            }
            _ticket = string.Empty;
            _groupId = string.Empty;
            _folderId = string.Empty;
            _fullPath = string.Empty;
        }

        public IEnumerable<Attach> GetAttachments()
        {

            var attachments = new List<Attach>(_mailItem.Attachments.Count);

            for (int index = 0; index < _mailItem.Attachments.Count; index++)
            {
                attachments.Add(
                    new Attach()
                    {
                        Index = _mailItem.Attachments[index + 1].Index,
                        DisplayName = _mailItem.Attachments[index + 1].DisplayName
                    }
                    );
            }

            return attachments;
        }

        public bool BrowseForFolder()
        {
            _ticket = Session.GetTikect();

            using (var folderBrowse = new BrowseFolderForm(_ticket))
            {
                if (folderBrowse.ShowFolderBrowserDialog())
                {
                    _groupId = folderBrowse.Group.id;
                    _folderId = folderBrowse.Folder.id;
                    _fullPath = folderBrowse.FullPath;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public string UploadAttach(string displayName, string linkedFileId, bool messageAsComments)
        {
            var attach = GetAttachments().FirstOrDefault(a => a.DisplayName.Equals(displayName));

            var uploadDirectory = DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.UploadDirectory);
            var uploadPath = string.Format("{0}\\{1}", uploadDirectory, _mailItem.Attachments[attach.Index].FileName);

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
            _mailItem.Attachments[attach.Index].SaveAsFile(uploadPath);

            var fileInfo = new FileInfo(uploadPath);
            var result = (UploadResult)null;

            if (messageAsComments)
            {
                var mailBody = String.Empty;

                if (!String.IsNullOrWhiteSpace(_mailItem.Body))
                {
                    if (_mailItem.Body.Length > 2100)
                    {
                        mailBody = _mailItem.Body.Remove(2100);
                    }
                    else
                    {
                        mailBody = _mailItem.Body;
                    }
                }

                result = DokuFlexService.Upload(_ticket, _groupId, String.Empty, _folderId,
                    mailBody, String.Empty, false, String.Empty, "plugin",
                    false, fileInfo);
            }
            else
            {
                result = DokuFlexService.Upload(_ticket, _groupId, String.Empty, _folderId,
                    String.Empty, String.Empty, false, String.Empty, "plugin",
                    false, fileInfo);
            }

            if (!String.IsNullOrWhiteSpace(result.nodeId))
            {
                //Delete the file from temp folder :)
                try
                {
                    File.Delete(uploadPath);
                }
                catch (Exception e)
                {
                    LogFactory.CreateLog().LogError(e);
                }

                return result.nodeId;
            }
            else
            {
                return String.Empty;
            }
        }

        public async Task<string> UploadAttachAsync(string displayName, string linkedFileId, bool messageAsComments)
        {
            var attach = GetAttachments().FirstOrDefault(a => a.DisplayName.Equals(displayName));

            var uploadDirectory = DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.UploadDirectory);
            var uploadPath = string.Format("{0}\\{1}", uploadDirectory, _mailItem.Attachments[attach.Index].FileName);

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
            _mailItem.Attachments[attach.Index].SaveAsFile(uploadPath);

            var fileInfo = new FileInfo(uploadPath);
            var result = (UploadResult)null;

            if (string.IsNullOrWhiteSpace(ProcessId))
            {
                if (messageAsComments)
                {
                    result = await DokuFlexService.UploadAsync(_ticket, _groupId, String.Empty, _folderId,
                        _mailItem.Body, String.Empty, false, String.Empty, "plugin",
                        false, fileInfo);
                }
                else
                {
                    result = await DokuFlexService.UploadAsync(_ticket, _groupId, String.Empty, _folderId,
                        String.Empty, String.Empty, false, String.Empty, "plugin",
                        false, fileInfo);
                }
            }
            else
            {
                result = await DokuFlexService.UploadAsync(_ticket, _groupId, String.Empty, _folderId,
                        _mailItem.Body, String.Empty, false, String.Empty, "plugin",
                        false, fileInfo);
            }
            

            if (!String.IsNullOrWhiteSpace(result.nodeId))
            {
                //Delete the file from temp folder :)
                try
                {
                    File.Delete(uploadPath);
                }
                catch (Exception e)
                {
                    LogFactory.CreateLog().LogError(e);
                }

                return result.nodeId;
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
