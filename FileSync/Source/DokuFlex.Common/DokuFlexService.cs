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

namespace DokuFlex.Common
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using DokuFlex.Common.ServiceAgents;
    using System.Threading.Tasks;

    public static class DokuFlexService
    {
        private static bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            { //the file is unavailable because it is:
              //still being written to
              //or being processed by another thread
              //or does not exist (has already been processed) return true;
            } finally
            {
                if (stream != null) stream.Close();
            }
            //file is not locked
            return false;
        }


        public static string GetTicket()
        {
            var credentials = new Credentials();

            credentials.UserName = ConfigurationManager.GetValue(Resources.LoginEmailAddressKey);
            credentials.SetEncryptedPassword(ConfigurationManager.GetValue(Resources.LoginPasswordKey));

            return DokuFlexService.Login(credentials);
        }

        public static async Task<string> GetTicketAsync()
        {
            var credentials = new Credentials();

            credentials.UserName = ConfigurationManager.GetValue(Resources.LoginEmailAddressKey);
            credentials.SetEncryptedPassword(ConfigurationManager.GetValue(Resources.LoginPasswordKey));

            return await DokuFlexService.LoginAsync(credentials);
        }

        public static string Login(Credentials credentials)
        {
            if (!credentials.ContainCredententials())
            {
                throw new ArgumentException("Invalid credentials");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("emailAddress={0}&password={1}", credentials.UserName, credentials.Password);
            var response = new RestServiceAgent().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "login"), request);

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (LoginResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(LoginResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.ticket;
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public static async Task<string> LoginAsync(Credentials credentials)
        {
            if (!credentials.ContainCredententials())
            {
                throw new ArgumentException("Invalid credentials");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("emailAddress={0}&password={1}", credentials.UserName, credentials.Password);
            var response = await new RestServiceAgent().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "login"), request);

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (LoginResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(LoginResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.ticket;
            }
            else
            {
                //throw new RestResponseException(responseObject.errorDesc);
                return String.Empty;
            }
        }

        public static bool DeleteFile(string ticket, string groupId, string fileId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentNullException("groupId");
            }

            if (string.IsNullOrWhiteSpace(fileId))
            {
                throw new ArgumentNullException("fileId");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("ticket={0}&groupId={1}&itemId={2}", ticket, groupId, fileId);
            var response = new RestServiceAgent().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "deleteFile"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (CreateFolderResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(CreateFolderResponse));

            if (responseObject.res == "ok")
            {
                return true;
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public static async Task<bool> DeleteFileAsync(string ticket, string groupId, string fileId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentNullException("groupId");
            }

            if (string.IsNullOrWhiteSpace(fileId))
            {
                throw new ArgumentNullException("fileId");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("ticket={0}&groupId={1}&itemId={2}", ticket, groupId, fileId);
            var response = await new RestServiceAgent().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "deleteFile"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (CreateFolderResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(CreateFolderResponse));

            if (responseObject.res == "ok")
            {
                return true;
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public static bool DeleteFolder(string ticket, string groupId, string folderId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentNullException("groupId");
            }

            if (string.IsNullOrWhiteSpace(folderId))
            {
                throw new ArgumentNullException("folderId");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("ticket={0}&groupId={1}&folderId={2}", ticket, groupId, folderId);
            var response = new RestServiceAgent().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "deleteFolder"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (CreateFolderResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(CreateFolderResponse));

            if (responseObject.res == "ok")
            {
                return true;
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public static async Task<bool> DeleteFolderAsync(string ticket, string groupId, string folderId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentNullException("groupId");
            }

            if (string.IsNullOrWhiteSpace(folderId))
            {
                throw new ArgumentNullException("folderId");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("ticket={0}&groupId={1}&folderId={2}", ticket, groupId, folderId);
            var response = await new RestServiceAgent().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "deleteFolder"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (CreateFolderResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(CreateFolderResponse));

            if (responseObject.res == "ok")
            {
                return true;
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public static bool RenameFileFolder(string ticket, string groupId, string fileFolderId, string title)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentNullException("groupId");
            }

            if (string.IsNullOrWhiteSpace(fileFolderId))
            {
                throw new ArgumentNullException("fileFolderId");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException("title");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("ticket={0}&groupId={1}&itemId={2}&title={3}", ticket, groupId, fileFolderId, title);
            var response = new RestServiceAgent().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "renameFileFolder"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (CreateFolderResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(CreateFolderResponse));

            if (responseObject.res == "ok")
            {
                return true;
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public static async Task<bool> RenameFileFolderAsync(string ticket, string groupId, string fileFolderId, string title)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentNullException("groupId");
            }

            if (string.IsNullOrWhiteSpace(fileFolderId))
            {
                throw new ArgumentNullException("fileFolderId");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException("title");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("ticket={0}&groupId={1}&itemId={2}&title={3}", ticket, groupId, fileFolderId, title);
            var response = await new RestServiceAgent().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "renameFileFolder"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (CreateFolderResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(CreateFolderResponse));

            if (responseObject.res == "ok")
            {
                return true;
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public static string CreateFolder(string ticket, string groupId, string parentNodeId, string folderName)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentNullException("groupId");
            }

            if (string.IsNullOrWhiteSpace(folderName))
            {
                throw new ArgumentNullException("folderName");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&folderName={3}", ticket, groupId, parentNodeId, folderName);
            var response = new RestServiceAgent().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "createFolder"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (CreateFolderResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(CreateFolderResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.id;
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public static async Task<string> CreateFolderAsync(string ticket, string groupId, string parentNodeId, string folderName)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentNullException("groupId");
            }

            if (string.IsNullOrWhiteSpace(folderName))
            {
                throw new ArgumentNullException("folderName");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&folderName={3}", ticket, groupId, parentNodeId, folderName);
            var response = await new RestServiceAgent().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "createFolder"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (CreateFolderResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(CreateFolderResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.id;
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public static FileUpload UploadFile(string ticket, string groupId, string folderId,
            string fileId, string fileName, string filePath, bool saveAsNewVersion)
        {
            if (string.IsNullOrWhiteSpace(ticket))
                throw new ArgumentNullException("ticket");

            if (string.IsNullOrWhiteSpace(groupId))
                throw new ArgumentNullException("groupId");

            if (string.IsNullOrWhiteSpace(folderId))
                throw new ArgumentNullException("folderId");

            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");

            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException("filePath");

            var requestParams = new Dictionary<string, string>();
            requestParams.Add("ticket", ticket);
            requestParams.Add("groupId", groupId);
            requestParams.Add("folderId", folderId);
            requestParams.Add("versionId", fileId);
            requestParams.Add("filename", fileName);
            requestParams.Add("newVersion", saveAsNewVersion.ToString());

            var fileInfo = new FileInfo(filePath);

            if (IsFileLocked(fileInfo))
                throw new ArgumentException("fileInfo");

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var response = new RestServiceAgent().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "fileUpload"),
                requestParams, fileInfo, "file");

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var fileUploadResponse = (FileUploadResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(FileUploadResponse));

            if (fileUploadResponse.res == "ok")
            {
                var fileUpload = new FileUpload()
                {
                    nodeId = fileUploadResponse.nodeId,
                    modifiedTime = fileUploadResponse.modifiedTime
                };

                return fileUpload;
            }
            else
            {
                throw new RestResponseException(fileUploadResponse.errorDesc);
            }
        }

        public static FileUpload UploadFile(string ticket, string groupId, string folderId,
            string fileId, FileInfo fileInfo, bool saveAsNewVersion)
        {
            if (string.IsNullOrWhiteSpace(ticket))
                throw new ArgumentNullException("ticket");

            if (string.IsNullOrWhiteSpace(groupId))
                throw new ArgumentNullException("groupId");

            if (string.IsNullOrWhiteSpace(folderId))
                throw new ArgumentNullException("folderId");

            if (IsFileLocked(fileInfo))
                throw new ArgumentException("fileInfo");

            var requestParams = new Dictionary<string, string>();
            requestParams.Add("ticket", ticket);
            requestParams.Add("groupId", groupId);
            requestParams.Add("folderId", folderId);
            requestParams.Add("versionId", fileId);
            requestParams.Add("filename", fileInfo.Name);
            requestParams.Add("newVersion", saveAsNewVersion.ToString());

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var response = new RestServiceAgent().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "fileUpload"),
                requestParams, fileInfo, "file");

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var fileUploadResponse = (FileUploadResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(FileUploadResponse));

            if (fileUploadResponse.res == "ok")
            {
                var fileUpload = new FileUpload()
                {
                    nodeId = fileUploadResponse.nodeId,
                    modifiedTime = fileUploadResponse.modifiedTime
                };

                return fileUpload;
            }
            else
            {
                throw new RestResponseException(fileUploadResponse.errorDesc);
            }
        }

        public static async Task<FileUpload> UploadFileAsync(string ticket, string groupId, string folderId,
            string fileId, FileInfo fileInfo, bool saveAsNewVersion)
        {
            if (string.IsNullOrWhiteSpace(ticket))
                throw new ArgumentNullException("ticket");

            if (string.IsNullOrWhiteSpace(groupId))
                throw new ArgumentNullException("groupId");

            if (IsFileLocked(fileInfo))
                throw new ArgumentException("fileInfo");

            var requestParams = new Dictionary<string, string>();
            requestParams.Add("ticket", ticket);
            requestParams.Add("groupId", groupId);
            requestParams.Add("folderId", folderId);
            requestParams.Add("versionId", fileId);
            requestParams.Add("filename", fileInfo.Name);
            requestParams.Add("newVersion", saveAsNewVersion.ToString());

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var response = await new RestServiceAgent().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "fileUpload"),
                requestParams, fileInfo, "file");

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var fileUploadResponse = (FileUploadResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(FileUploadResponse));

            if (fileUploadResponse.res == "ok")
            {
                var fileUpload = new FileUpload()
                {
                    nodeId = fileUploadResponse.nodeId,
                    modifiedTime = fileUploadResponse.modifiedTime
                };

                return fileUpload;
            }
            else
            {
                throw new RestResponseException(fileUploadResponse.errorDesc);
            }
        }

        public static bool DownloadFile(string ticket, string fileId, string filePath)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(fileId))
            {
                throw new ArgumentNullException("fileId");
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException("filePath");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("{0}/{1}", ticket, fileId);
            var response = new RestServiceAgent().SendGetRequest(string.Format("{0}/{1}/{2}", restServiceUrl, "viewDocument", request));

            if (response == null || response.Length == 0)
            {
                return false;
            }

            try
            {
                var responseFile = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                response.Position = 0;
                response.CopyTo(responseFile);
                response.Close();
                responseFile.Flush();
                responseFile.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static async Task<bool> DownloadFileAsync(string ticket, string fileId, string filePath)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(fileId))
            {
                throw new ArgumentNullException("fileId");
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException("filePath");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("{0}/{1}", ticket, fileId);
            var response = await new RestServiceAgent().SendGetRequestAsync(string.Format("{0}/{1}/{2}", restServiceUrl, "viewDocument", request));

            if (response == null || response.Length == 0)
            {
                return false;
            }

            try
            {
                var responseFile = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                response.Position = 0;
                response.CopyTo(responseFile);
                response.Close();
                responseFile.Flush();
                responseFile.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static List<UserGroup> GetUserGroups(string ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("ticket={0}", ticket);
            var response = new RestServiceAgent().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "getUserGroups"), request);

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (GetUserGroupsResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetUserGroupsResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.groups;
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public static async Task<List<UserGroup>> GetUserGroupsAsync(string ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("ticket={0}", ticket);
            var response = await new RestServiceAgent().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "getUserGroups"), request);

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (GetUserGroupsResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetUserGroupsResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.groups;
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public static List<FileFolder> GetFolders(string ticket, string groupId, string parentFolderId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentNullException("groupId");
            }

            var folders = new List<FileFolder>();
            var firstResult = 0;
            var maxResults = 99;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (GetFilesFoldersResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&includeFolders=true&includeFiles=false", ticket, groupId, parentFolderId);

            do
            {
                response = new RestServiceAgent().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "getFilesFolders"), string.Format("{0}&firstResult={1}&maxResults={2}", request, firstResult, maxResults));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (GetFilesFoldersResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetFilesFoldersResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.filesfolders.Count > 0)
                    {
                        folders.AddRange(responseObject.filesfolders);
                    }
                    else
                    {
                        responseComplete = true;
                    }
                }
                else
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                firstResult += maxResults++;
                maxResults += 99;

            }
            while (!responseComplete);

            return folders;
        }

        public static async Task<List<FileFolder>> GetFoldersAsync(string ticket, string groupId, string parentFolderId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentNullException("groupId");
            }

            var folders = new List<FileFolder>();
            var firstResult = 0;
            var maxResults = 99;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (GetFilesFoldersResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&includeFolders=true&includeFiles=false", ticket, groupId, parentFolderId);

            do
            {
                response = await new RestServiceAgent().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "getFilesFolders"), string.Format("{0}&firstResult={1}&maxResults={2}", request, firstResult, maxResults));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (GetFilesFoldersResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetFilesFoldersResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.filesfolders.Count > 0)
                    {
                        folders.AddRange(responseObject.filesfolders);
                    }
                    else
                    {
                        responseComplete = true;
                    }
                }
                else
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                firstResult += maxResults++;
                maxResults += 99;

            }
            while (!responseComplete);

            return folders;
        }

        public static List<FileFolder> GetFiles(string ticket, string groupId, string parentFolderId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentNullException("groupId");
            }

            var folders = new List<FileFolder>();
            var firstResult = 0;
            var maxResults = 99;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (GetFilesFoldersResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&includeFolders=false&includeFiles=true", ticket, groupId, parentFolderId);

            do
            {
                response = new RestServiceAgent().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "getFilesFolders"), string.Format("{0}&firstResult={1}&maxResults={2}", request, firstResult, maxResults));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (GetFilesFoldersResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetFilesFoldersResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.filesfolders.Count > 0)
                    {
                        folders.AddRange(responseObject.filesfolders);
                    }
                    else
                    {
                        responseComplete = true;
                    }
                }
                else
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                firstResult += maxResults++;
                maxResults += 99;

            }
            while (!responseComplete);

            return folders;
        }

        public static async Task<List<FileFolder>> GetFilesAsync(string ticket, string groupId, string parentFolderId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentNullException("groupId");
            }

            var folders = new List<FileFolder>();
            var firstResult = 0;
            var maxResults = 99;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (GetFilesFoldersResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&includeFolders=false&includeFiles=true", ticket, groupId, parentFolderId);

            do
            {
                response = await new RestServiceAgent().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "getFilesFolders"), string.Format("{0}&firstResult={1}&maxResults={2}", request, firstResult, maxResults));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (GetFilesFoldersResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetFilesFoldersResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.filesfolders.Count > 0)
                    {
                        folders.AddRange(responseObject.filesfolders);
                    }
                    else
                    {
                        responseComplete = true;
                    }
                }
                else
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                firstResult += maxResults++;
                maxResults += 99;

            }
            while (!responseComplete);

            return folders;
        }

        public static List<FileFolder> GetFilesFolders(string ticket, string groupId, string parentFolderId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentNullException("groupId");
            }

            var folders = new List<FileFolder>();
            var firstResult = 0;
            var maxResults = 99;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (GetFilesFoldersResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&includeFolders=true&includeFiles=true", ticket, groupId, parentFolderId);

            do
            {
                response = new RestServiceAgent().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "getFilesFolders"), string.Format("{0}&firstResult={1}&maxResults={2}", request, firstResult, maxResults));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (GetFilesFoldersResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetFilesFoldersResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.filesfolders.Count > 0)
                    {
                        folders.AddRange(responseObject.filesfolders);
                    }
                    else
                    {
                        responseComplete = true;
                    }
                }
                else
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                firstResult += maxResults++;
                maxResults += 99;

            }
            while (!responseComplete);

            return folders;
        }

        public static async Task<List<FileFolder>> GetFilesFoldersAsync(string ticket, string groupId, string parentFolderId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentNullException("groupId");
            }

            var folders = new List<FileFolder>();
            var firstResult = 0;
            var maxResults = 99;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (GetFilesFoldersResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Resources.RestServiceUrlKey);
            var request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&includeFolders=true&includeFiles=true", ticket, groupId, parentFolderId);

            do
            {
                response = await new RestServiceAgent().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "getFilesFolders"), string.Format("{0}&firstResult={1}&maxResults={2}", request, firstResult, maxResults));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (GetFilesFoldersResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetFilesFoldersResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.filesfolders.Count > 0)
                    {
                        folders.AddRange(responseObject.filesfolders);
                    }
                    else
                    {
                        responseComplete = true;
                    }
                }
                else
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                firstResult += maxResults++;
                maxResults += 99;

            }
            while (!responseComplete);

            return folders;
        }
    }
}
