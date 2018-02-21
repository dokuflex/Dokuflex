

namespace DokuFlex.Outlook.AddIn
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Outlook = Microsoft.Office.Interop.Outlook;

    using DokuFlex.Windows.Common;
    using DokuFlex.Windows.Common.Log; 
    using DokuFlex.Windows.Common.Services.Data;
    using DokuFlex.Windows.Common.Services;
    using DokuFlex.WinForms.Common;

    class UpdateTaskPresenter
    {
        private Outlook.MailItem _mailItem;

        private FileFolder _folder;
        private string _fullPath;

        public UpdateTaskPresenter()
        {
            _mailItem = Globals.ThisAddIn.Application.ActiveExplorer().Selection[1] as Outlook.MailItem;
        }

        public IList<UserGroup> GetUserGroups(string ticket)
        {
            return DokuFlexService.GetUserGroups(ticket);
        }

        public IList<Category> ListCategories(string ticket, string communityId, string projectId, string categoryType)
        {
            return DokuFlexService.ListCategories(ticket, communityId, projectId, categoryType);
        }

        public UpdateTaskResponse UpdateTask(string ticket, string communityId, string title, 
            long startDate, long endDate, string description, string categoryId, 
            string subCategoryId, string categoryStatusId, string projectId)
        {
           var result = DokuFlexService.UpdateTask(ticket, communityId, title, startDate, 
                endDate, description, categoryId, subCategoryId, categoryStatusId, projectId);

           return result;
        }

        public string UploadAttach(string displayName, string ticket, string communityId)
        {
            var attach = GetAttachments().FirstOrDefault(a => a.DisplayName.Equals(displayName));
            var uploadDir = DFEnvironment.GetSpecialFolder(DFEnvironment.SpecialFolder.UploadDirectory);

            if (!Directory.Exists(uploadDir)) Directory.CreateDirectory(uploadDir);

            var filePath = String.Format("{0}\\{1}", uploadDir,
                _mailItem.Attachments[attach.Index].FileName);             

            //Save attach to the uploadDirectory
            _mailItem.Attachments[attach.Index].SaveAsFile(filePath);

            var result = DokuFlexService.Upload(ticket, communityId,
                String.Empty, _folder.id, String.Empty, String.Empty,
                false, String.Empty, "plugin", false, new FileInfo(filePath));

            return result.nodeId;
        }

        public void LinkFileToTask(string ticket, string taskId, string fileId)
        {
            DokuFlexService.LinkDocToTask(ticket, taskId, fileId);
        }

        public bool BrowseForFolders(string ticket, string communityId)
        {
            using (var form = new BrowseFolderForm(ticket, communityId))
            {
                if (form.ShowFolderBrowserDialog())
                {
                    _folder = form.Folder;
                    _fullPath = form.FullPath;

                    return true;
                }
            }

            return false;
        }

        public string FolderPath
        {
            get
            {
                return _fullPath;
            }
        }

        public string Subject
        {
            get
            {
                if (_mailItem.Subject == null) return String.Empty;

                if (_mailItem.Subject.Length > 255)
                {
                    return _mailItem.Subject.Remove(255);
                }
                else
                {
                    return _mailItem.Subject;
                }             
            }
        }

        public string MessageBody
        {
            get
            {
                if (_mailItem.Body == null) return String.Empty;

                if (_mailItem.Body.Length > 2046)
                {
                    return _mailItem.Body.Remove(2046);
                }

                return _mailItem.Body;       
            }
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
    }
}
