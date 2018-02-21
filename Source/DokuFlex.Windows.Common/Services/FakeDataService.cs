using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using DokuFlex.Windows.Common.Services.Data;
using DokuFlex.Windows.Common.Helpers;

namespace DokuFlex.Windows.Common.Services
{
    public class FakeDataService : IDataService
    {
        public string CreateFolder(string ticket, string groupId, string parentNodeId, string folderName)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateFolderAsync(string ticket, string groupId, string parentNodeId, string folderName)
        {
            return Task.Run(() =>
            {
                return Guid.NewGuid().ToString("N");
            });
        }

        public bool DeleteFile(string ticket, string groupId, string fileId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFileAsync(string ticket, string groupId, string fileId)
        {
            return Task.Run(() =>
            {
                return true;
            });
        }

        public bool DeleteFolder(string ticket, string groupId, string folderId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFolderAsync(string ticket, string groupId, string folderId)
        {
            return Task.Run(() =>
            {
                return true;
            });
        }

        public Stream Download(string ticket, string fileId)
        {
            throw new NotImplementedException();
        }

        public bool Download(string ticket, string fileId, string path)
        {
            throw new NotImplementedException();
        }

        public Task<Stream> DownloadAsync(string ticket, string fileId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DownloadAsync(string ticket, string fileId, string path)
        {
            return Task.Run(() =>
            {
                return true;
            });
        }

        public List<Documentary> GetDocumentaryTypes(string ticket)
        {
            throw new NotImplementedException();
        }

        public Task<List<Documentary>> GetDocumentaryTypesAsync(string ticket)
        {
            return Task.Run(() =>
            {
                var documentaryTypes = new List<Documentary>();
                documentaryTypes.Add(new Documentary()
                {
                    homologation = 1,
                    id = Guid.NewGuid().ToString("N"),
                    key = "Predeterminado",
                    name = "Predeterminado"

                });

                documentaryTypes.Add(new Documentary()
                {
                    homologation = 2,
                    id = Guid.NewGuid().ToString("N"),
                    key = "Facturas",
                    name = "Facturas"

                });

                return documentaryTypes;
            });
        }

        public GetDocumentMetadataResponse GetDocumentMetadada(string ticket, string uuid)
        {
            throw new NotImplementedException();
        }

        public Task<GetDocumentMetadataResponse> GetDocumentMetadadaAsync(string ticket, string uuid)
        {
            return Task.Run(() =>
            {
                var metadataResponse = new GetDocumentMetadataResponse();
                metadataResponse.elements.Add(new DokuField()
                {
                    id = "f1a3174a-d2c5-4433-9bf8-7edffb79bc97",
                    dokuField = "textField1",
                    text = "Nº recepción",
                    order = 1,
                    mandatory = 1,
                    value = null,
                    type = "T",
                    key = "NUMERO_RECEPCION"
                });
                metadataResponse.elements.Add(new DokuField()
                {
                    id = "bb04ea4f-5dde-4e2d-bf8c-28005deab9b5",
                    dokuField = "dateField1",
                    text = "Fecha expedición",
                    order = 2,
                    mandatory = 1,
                    value = null,
                    type = "F",
                    key = "FECHA_EXPEDICION"
                });

                return metadataResponse;
            });
        }

        public List<RecentFile> GetFavoriteDocuments(string ticket, string filterText)
        {
            throw new NotImplementedException();
        }

        public Task<List<RecentFile>> GetFavoriteDocumentsAsync(string ticket, string filterText)
        {
            throw new NotImplementedException();
        }

        public List<RecentFile> GetFavouriteItems(string ticket, int type, string filterText)
        {
            throw new NotImplementedException();
        }

        public Task<List<RecentFile>> GetFavouriteItemsAsync(string ticket, int type, string filterText)
        {
            throw new NotImplementedException();
        }

        public List<FileFolder> GetFiles(string ticket, string groupId, string parentFolderId)
        {
            throw new NotImplementedException();
        }

        public Task<List<FileFolder>> GetFilesAsync(string ticket, string groupId, string parentFolderId)
        {
            throw new NotImplementedException();
        }

        public List<FileFolder> GetFilesFolders(string ticket, string groupId, string parentFolderId)
        {
            throw new NotImplementedException();
        }

        public Task<List<FileFolder>> GetFilesFoldersAsync(string ticket, string groupId, string parentFolderId)
        {
            return Task.Run(() =>
            {
                return new List<FileFolder>()
                {
                    new FileFolder()
                    {
                        id = Guid.NewGuid().ToString("N"),
                        name = "Folder 1",
                        type = "F",
                        modifiedTime = DateTimeHelper.ToUnixEpoch(DateTime.Now)
                    },
                    new FileFolder()
                    {
                        id = Guid.NewGuid().ToString("N"),
                        name = "Folder 2",
                        type = "F",
                        modifiedTime = DateTimeHelper.ToUnixEpoch(DateTime.Now)
                    },
                    new FileFolder()
                    {
                        id = Guid.NewGuid().ToString("N"),
                        name = "Folder 3",
                        type = "F",
                        modifiedTime = DateTimeHelper.ToUnixEpoch(DateTime.Now)
                    },
                    new FileFolder()
                    {
                        id = Guid.NewGuid().ToString("N"),
                        name = "File1.ppt",
                        type = "C",
                        modifiedTime = DateTimeHelper.ToUnixEpoch(DateTime.Now),
                        size = 23451345,
                        version = "1"
                    },
                    new FileFolder()
                    {
                        id = Guid.NewGuid().ToString("N"),
                        name = "File2.docx",
                        type = "C",
                        modifiedTime = DateTimeHelper.ToUnixEpoch(DateTime.Now),
                        size = 142545676,
                        version = "2.4"
                    },
                    new FileFolder()
                    {
                        id = Guid.NewGuid().ToString("N"),
                        name = "File3.xlsx",
                        type = "C",
                        modifiedTime = DateTimeHelper.ToUnixEpoch(DateTime.Now),
                        size = 23445,
                        version = "1.3"
                    },
                    new FileFolder()
                    {
                        id = Guid.NewGuid().ToString("N"),
                        name = "File4.msi",
                        type = "C",
                        modifiedTime = DateTimeHelper.ToUnixEpoch(DateTime.Now),
                        size = 23455435,
                        version = "1"
                    }
                };
            });
        }

        public List<FileFolder> GetFolders(string ticket, string groupId, string parentFolderId)
        {
            throw new NotImplementedException();
        }

        public Task<List<FileFolder>> GetFoldersAsync(string ticket, string groupId, string parentFolderId)
        {
            return Task.Run(() =>
            {
                return new List<FileFolder>()
                {
                    new FileFolder()
                    {
                        id = "1",
                        name = "Root Folder 1",
                        type = "F",
                        modifiedTime = DateTimeHelper.ToUnixEpoch(DateTime.Now)
                    },
                    new FileFolder()
                    {
                        id = "2",
                        name = "Root Folder 2",
                        type = "F",
                        modifiedTime = DateTimeHelper.ToUnixEpoch(DateTime.Now)
                    },
                    new FileFolder()
                    {
                        id = "3",
                        name = "Root Folder 3",
                        type = "F",
                        modifiedTime = DateTimeHelper.ToUnixEpoch(DateTime.Now)
                    }
                };
            });
        }

        public List<RecentFile> GetRecentDocuments(string ticket, string filterText)
        {
            throw new NotImplementedException();
        }

        public Task<List<RecentFile>> GetRecentDocumentsAsync(string ticket, string filterText)
        {
            throw new NotImplementedException();
        }

        public List<ScanHistory> GetScanHistory(string ticket, long startDate)
        {
            throw new NotImplementedException();
        }

        public Task<List<ScanHistory>> GetScanHistoryAsync(string ticket, long startDate)
        {
            throw new NotImplementedException();
        }

        public List<UserGroup> GetUserGroups(string ticket)
        {
            return new List<UserGroup>()
            {
                new UserGroup(){id = "1", name = "Community 1"},
                new UserGroup(){id = "2", name = "Community 2"},
                new UserGroup(){id = "3", name = "Community 3"},
            };
        }

        public Task<List<UserGroup>> GetUserGroupsAsync(string ticket)
        {
            return Task.Run(() =>
            {
                return new List<UserGroup>()
                {
                    new UserGroup(){id = "1", name = "Community 1"},
                    new UserGroup(){id = "2", name = "Community 2"},
                    new UserGroup(){id = "3", name = "Community 3"},
                };
            });
        }

        public string LinkDocToTask(string ticket, string taskId, string itemId)
        {
            throw new NotImplementedException();
        }

        public Task<string> LinkDocToTaskAsync(string ticket, string taskId, string itemId)
        {
            throw new NotImplementedException();
        }

        public List<Category> ListCategories(string ticket, string communityId, string categoryType)
        {
            throw new NotImplementedException();
        }

        public Task<List<Category>> ListCategoriesAsync(string ticket, string communityId, string categoryType)
        {
            throw new NotImplementedException();
        }

        public List<Certificate> ListCertificates(string ticket)
        {
            throw new NotImplementedException();
        }

        public Task<List<Certificate>> ListCertificatesAsync(string ticket)
        {
            return Task.Run(() =>
            {
                return new List<Certificate>()
                {
                    new Certificate(){id = "1", text = "Certificate 1"},
                    new Certificate(){id = "2", text = "Certificate 2"},
                    new Certificate(){id = "3", text = "Certificate 3"},
                };
            });
        }

        public List<Project> ListProjects(string ticket, string communityId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Project>> ListProjectsAsync(string ticket, string communityId)
        {
            throw new NotImplementedException();
        }

        public string Login(Credentials credentials)
        {
            return Guid.NewGuid().ToString("N");
        }

        public Task<string> LoginAsync(Credentials credentials)
        {
            return Task.Run(() =>
            {
                return Guid.NewGuid().ToString("N");
            });
        }

        public bool RenameFileFolder(string ticket, string groupId, string fileFolderId, string title)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RenameFileFolderAsync(string ticket, string groupId, string fileFolderId, string title)
        {
            return Task.Run(() =>
            {
                return true;
            });
        }

        public List<SearchResult> Search(string ticket, string text, string filterFileName, string searchType)
        {
            throw new NotImplementedException();
        }

        public Task<List<SearchResult>> SearchAsync(string ticket, string text, string filterFileName, string searchType)
        {
            throw new NotImplementedException();
        }

        public bool UpdateDocumentMetadata(string ticket, string documentType, string uuid, params DokuField[] data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateDocumentMetadataAsync(string ticket, string documentType, string uuid, params DokuField[] data)
        {
            return Task.Run(() =>
            {
                return true;
            });
        }

        public bool UpdateFavorite(string ticket, string itemId, string action, int type)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateFavoriteAsync(string ticket, string itemId, string action, int type)
        {
            throw new NotImplementedException();
        }

        public string UpdateTask(string ticket, string communityId, string title, long startDate, long endDate, string description, string category, string subCategory, string categoryStatus, string projectId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateTaskAsync(string ticket, string communityId, string title, long startDate, long endDate, string description, string category, string subCategory, string categoryStatus)
        {
            throw new NotImplementedException();
        }

        public UploadResult Upload(string ticket, string groupId, string versionId, string folderId, string comments, string linkedId, bool saveAsNewVersion, string certificateId, string source, bool convertToPDF, System.IO.FileInfo fileInfo)
        {
            return new UploadResult()
            {
                nodeId = Guid.NewGuid().ToString(),
                modifiedTime = DateTimeHelper.ToUnixEpoch(DateTime.Now)
            };
        }

        public Task<UploadResult> UploadAsync(string ticket, string groupId, string versionId, string folderId, string comments, string linkedId, bool saveAsNewVersion, string certificateId, string source, bool convertToPDF, System.IO.FileInfo fileInfo)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(10000);

                return new UploadResult()
                {
                    nodeId = Guid.NewGuid().ToString(),
                    modifiedTime = DateTimeHelper.ToUnixEpoch(DateTime.Now)
                };
            });
        }

        public GetAppInfoResponse GetAppInfo(string appId)
        {
            return new GetAppInfoResponse()
            {
                id = "addin_office",
                version = "1.0"
            };
        }

        public Task<GetAppInfoResponse> GetAppInfoAsync(string appId)
        {
            return Task.Run(() =>
            {
                return new GetAppInfoResponse()
                {
                    id = "addin_office",
                    version = "1.0"
                };
            });
        }


        public bool AddImageSignature(FileInfo sigFile, FileInfo sigImageFile, string ticketId, string nodeId, params SignaturePosition[] signaturePositions)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddImageSignatureAsync(FileInfo sigFile, FileInfo sigImageFile, string ticketId, string nodeId, params SignaturePosition[] signaturePositions)
        {
            throw new NotImplementedException();
        }


        public Task<UploadResult> UploadAsync(string ticket, string groupId, string versionId, string folderId, string comments, string linkedId, string externalId, bool saveAsNewVersion, string certificateId, string source, bool convertToPDF, FileInfo fileInfo)
        {
            throw new NotImplementedException();
        }


        public Task<List<SearchUserResult>> SearchUserAsync(string ticket, string text)
        {
            throw new NotImplementedException();
        }


        public Task<UploadResult> UploadAsync(string ticket, string groupId, string versionId, string folderId, string comments, string linkedId, string externalId, bool saveAsNewVersion, string certificateId, string certificatePass, string source, bool convertToPDF, FileInfo fileInfo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RequestDocumentSignature(string ticket, string itemId, List<string> users)
        {
            throw new NotImplementedException();
        }



        public Task<bool> SendAppLog(string organizationId, string communityId, string app, string userId, string error, string errorDesc, string errorStack)
        {
            throw new NotImplementedException();
        }

        public List<Process> ListProcesses(string ticket)
        {
            throw new NotImplementedException();
        }

        public Task<List<Process>> ListProcessesAsync(string ticket)
        {
            throw new NotImplementedException();
        }

        public bool ProcessUpdateData(string ticket, string processId, string communityId, string dataId = "", params ProcessData[] data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ProcessUpdateDataAsync(string ticket, string processId, string communityId, string dataId = "", params ProcessData[] data)
        {
            throw new NotImplementedException();
        }

        string IDataService.ProcessUpdateData(string ticket, string processId, string communityId, string dataId, params ProcessData[] data)
        {
            throw new NotImplementedException();
        }

        Task<string> IDataService.ProcessUpdateDataAsync(string ticket, string processId, string communityId, string dataId, params ProcessData[] data)
        {
            throw new NotImplementedException();
        }

        public Task<UploadResult> ProcessUploadAsync(string ticket, string processId, string dataId, string source, FileInfo fileInfo)
        {
            throw new NotImplementedException();
        }
    }
}
