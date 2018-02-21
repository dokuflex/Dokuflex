using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using DokuFlex.Windows.Common.Services.Data;

namespace DokuFlex.Windows.Common.Services
{
    public interface IDataService
    {
        string CreateFolder(string ticket, string groupId, string parentNodeId, string folderName);

        Task<string> CreateFolderAsync(string ticket, string groupId, string parentNodeId, string folderName);

        bool DeleteFile(string ticket, string groupId, string fileId);

        Task<bool> DeleteFileAsync(string ticket, string groupId, string fileId);

        bool DeleteFolder(string ticket, string groupId, string folderId);

        Task<bool> DeleteFolderAsync(string ticket, string groupId, string folderId);

        Stream Download(string ticket, string fileId);

        bool Download(string ticket, string fileId, string path);

        Task<Stream> DownloadAsync(string ticket, string fileId);

        Task<bool> DownloadAsync(string ticket, string fileId, string path);

        GetAppInfoResponse GetAppInfo(string appId);

        Task<GetAppInfoResponse> GetAppInfoAsync(string appId);

        List<Documentary> GetDocumentaryTypes(string ticket);

        Task<List<Documentary>> GetDocumentaryTypesAsync(string ticket);

        GetDocumentMetadataResponse GetDocumentMetadada(string ticket, string uuid);

        Task<GetDocumentMetadataResponse> GetDocumentMetadadaAsync(string ticket, string uuid);

        List<RecentFile> GetFavoriteDocuments(string ticket, string filterText);

        Task<List<RecentFile>> GetFavoriteDocumentsAsync(string ticket, string filterText);

        List<RecentFile> GetFavouriteItems(string ticket, int type, string filterText);

        Task<List<RecentFile>> GetFavouriteItemsAsync(string ticket, int type, string filterText);

        List<FileFolder> GetFiles(string ticket, string groupId, string parentFolderId);

        Task<List<FileFolder>> GetFilesAsync(string ticket, string groupId, string parentFolderId);

        List<FileFolder> GetFilesFolders(string ticket, string groupId, string parentFolderId);

        Task<List<FileFolder>> GetFilesFoldersAsync(string ticket, string groupId, string parentFolderId);

        List<FileFolder> GetFolders(string ticket, string groupId, string parentFolderId);

        Task<List<FileFolder>> GetFoldersAsync(string ticket, string groupId, string parentFolderId);

        List<RecentFile> GetRecentDocuments(string ticket, string filterText);

        Task<List<RecentFile>> GetRecentDocumentsAsync(string ticket, string filterText);

        List<ScanHistory> GetScanHistory(string ticket, long startDate);

        Task<List<ScanHistory>> GetScanHistoryAsync(string ticket, long startDate);

        List<UserGroup> GetUserGroups(string ticket);

        Task<List<UserGroup>> GetUserGroupsAsync(string ticket);

        string LinkDocToTask(string ticket, string taskId, string itemId);

        Task<string> LinkDocToTaskAsync(string ticket, string taskId, string itemId);

        List<Category> ListCategories(string ticket, string communityId, string categoryType);

        Task<List<Category>> ListCategoriesAsync(string ticket, string communityId, string categoryType);

        List<Certificate> ListCertificates(string ticket);

        Task<List<Certificate>> ListCertificatesAsync(string ticket);
        List<Project> ListProjects(string ticket, string communityId);

        Task<List<Project>> ListProjectsAsync(string ticket, string communityId);

        List<Process> ListProcesses(string ticket);

        Task<List<Process>> ListProcessesAsync(string ticket);

        string Login(Credentials credentials);

        Task<string> LoginAsync(Credentials credentials);

        bool RenameFileFolder(string ticket, string groupId, string fileFolderId, string title);

        Task<bool> RenameFileFolderAsync(string ticket, string groupId, string fileFolderId, string title);

        List<SearchResult> Search(string ticket, string text, string filterFileName, string searchType);

        Task<List<SearchResult>> SearchAsync(string ticket, string text, string filterFileName, string searchType);

        Task<List<SearchUserResult>> SearchUserAsync(string ticket, string text);

        bool AddImageSignature(System.IO.FileInfo sigFile, System.IO.FileInfo sigImageFile, string ticketId, string nodeId, params SignaturePosition[] signaturePositions);

        Task<bool> AddImageSignatureAsync(System.IO.FileInfo sigFile, System.IO.FileInfo sigImageFile, string ticketId, string nodeId, params SignaturePosition[] signaturePositions);

        bool UpdateDocumentMetadata(string ticket, string documentType, string uuid, params DokuField[] data);

        Task<bool> UpdateDocumentMetadataAsync(string ticket, string documentType, string uuid, params DokuField[] data);

        string ProcessUpdateData(string ticket, string processId, string communityId, string dataId = "", params ProcessData[] data);

        Task<string> ProcessUpdateDataAsync(string ticket, string processId, string communityId, string dataId = "", params ProcessData[] data);

        Task<UploadResult> ProcessUploadAsync(string ticket, string processId, string dataId, string source, FileInfo fileInfo);

        bool UpdateFavorite(string ticket, string itemId, string action, int type);

        Task<bool> UpdateFavoriteAsync(string ticket, string itemId, string action, int type);

        string UpdateTask(string ticket, string communityId, string title, long startDate, long endDate, string description, string category, string subCategory, string categoryStatus, string projectId);

        Task<string> UpdateTaskAsync(string ticket, string communityId, string title, long startDate, long endDate, string description, string category, string subCategory, string categoryStatus);

        UploadResult Upload(string ticket, string groupId, string versionId, string folderId, string comments, string linkedId, bool saveAsNewVersion, string certificateId, string source, bool convertToPDF, System.IO.FileInfo fileInfo);

        Task<UploadResult> UploadAsync(string ticket, string groupId, string versionId, string folderId, string comments, string linkedId, bool saveAsNewVersion, string certificateId, string source, bool convertToPDF, System.IO.FileInfo fileInfo);

        Task<UploadResult> UploadAsync(string ticket, string groupId, string versionId, string folderId, string comments, string linkedId, string externalId, bool saveAsNewVersion, string certificateId, string source, bool convertToPDF, FileInfo fileInfo);

        Task<UploadResult> UploadAsync(string ticket, string groupId, string versionId, string folderId, string comments, string linkedId, string externalId, bool saveAsNewVersion, string certificateId, string certificatePass, string source, bool convertToPDF, FileInfo fileInfo);

        Task<bool> RequestDocumentSignature(string ticket, string itemId, List<string> users);

        Task<bool> SendAppLog(string organizationId, string communityId, string app, string userId, string error, string errorDesc, string errorStack);
    }
}
