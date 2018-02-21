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

namespace DokuFlex.Windows.Common.Services
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using DokuFlex.Windows.Common.Log;
    using DokuFlex.Windows.Common.Services.Data;
    using DokuFlex.Windows.Common.Services.RESTful;
    using System.Threading.Tasks;
    using DokuFlex.Windows.Common.Extensions;

    public static class DokuFlexService
    {
        public static string Login(Credentials credentials)
        {
            if (!credentials.ContainCredententials())
            {
                throw new ArgumentException("Invalid credentials");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("emailAddress={0}&password={1}", credentials.UserName, credentials.Password);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "login"), request);

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
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static async Task<string> LoginAsync(Credentials credentials)
        {
            if (!credentials.ContainCredententials())
            {
                throw new ArgumentException("Invalid credentials");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("emailAddress={0}&password={1}", credentials.UserName, credentials.Password);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "login"), request);

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
                throw new RestResponseException(responseObject.errorDesc);
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

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&groupId={1}&itemId={2}", ticket, groupId, fileId);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "deleteFile"), request);

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
                throw new RestResponseException(responseObject.errorDesc);
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

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&groupId={1}&itemId={2}", ticket, groupId, fileId);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "deleteFile"), request);

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
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static bool UpdateDocumentMetadata(string ticket, string documentType, string uuid, params DokuField[] data)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(documentType))
            {
                throw new ArgumentNullException("documentType");
            }

            if (string.IsNullOrWhiteSpace(uuid))
            {
                throw new ArgumentNullException("uuid");
            }

            //convert metadata to json
            var obj = new JObject();

            foreach (var dokuField in data)
            {
                if (dokuField.value == null) continue;

                switch (dokuField.type)
                {
                    case "F":

                    case "H":
                        obj[dokuField.key] = long.Parse(dokuField.value.ToString());
                        break;

                    case "M":

                    case "N":
                        obj[dokuField.key] = double.Parse(dokuField.value.ToString());
                        break;

                    default:
                        obj[dokuField.key] = dokuField.value.ToString();
                        break;
                }
            }

            var serializerSettings = new JsonSerializerSettings() {
                StringEscapeHandling = StringEscapeHandling.EscapeHtml
            };

            var serializedObj = JsonConvert.SerializeObject(obj, Formatting.None, serializerSettings);
            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&documentType={1}&uuid={2}&status={3}&data={4}", ticket, documentType, uuid, true.ToString(), serializedObj);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "metadata/updateDocumentMetadata"), request);

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
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static async Task<bool> UpdateDocumentMetadataAsync(string ticket, string documentType, string uuid, params DokuField[] data)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(documentType))
            {
                throw new ArgumentNullException("documentType");
            }

            if (string.IsNullOrWhiteSpace(uuid))
            {
                throw new ArgumentNullException("uuid");
            }

            //convert metadata to json
            var obj = new JObject();

            foreach (var dokuField in data)
            {
                if (dokuField.value == null) continue;

                switch (dokuField.type)
                {
                    case "F":

                    case "H":
                        obj[dokuField.key] = long.Parse(dokuField.value.ToString());
                        break;

                    case "M":

                    case "N":
                        obj[dokuField.key] = double.Parse(dokuField.value.ToString());
                        break;

                    default:
                        obj[dokuField.key] = dokuField.value.ToString();
                        break;
                }
            }

            var serializerSettings = new JsonSerializerSettings()
            {
                StringEscapeHandling = StringEscapeHandling.EscapeHtml
            };

            var serializedObj = JsonConvert.SerializeObject(obj, Formatting.None, serializerSettings);
            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&documentType={1}&uuid={2}&status={3}&data={4}", ticket, documentType, uuid, true.ToString(), serializedObj);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "metadata/updateDocumentMetadata"), request);

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
                throw new RestResponseException(responseObject.errorDesc);
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

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&groupId={1}&folderId={2}", ticket, groupId, folderId);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "deleteFolder"), request);

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
                throw new RestResponseException(responseObject.errorDesc);
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

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&groupId={1}&folderId={2}", ticket, groupId, folderId);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "deleteFolder"), request);

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
                throw new RestResponseException(responseObject.errorDesc);
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

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&groupId={1}&itemId={2}&title={3}", ticket, groupId, fileFolderId, title);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "renameFileFolder"), request);

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
                throw new RestResponseException(responseObject.errorDesc);
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

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&groupId={1}&itemId={2}&title={3}", ticket, groupId, fileFolderId, title);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "renameFileFolder"), request);

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
                throw new RestResponseException(responseObject.errorDesc);
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

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&folderName={3}", ticket, groupId, parentNodeId, folderName);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "createFolder"), request);

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
                throw new RestResponseException(responseObject.errorDesc);
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

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&folderName={3}", ticket, groupId, parentNodeId, folderName);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "createFolder"), request);

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
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static UploadResult Upload(string ticket, string groupId, string versionId, string folderId,
            string comments, string linkedId, bool saveAsNewVersion, string certificateId, string source,
            bool convertToPDF, FileInfo fileInfo)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var requestParams = new Dictionary<string, string>();
            requestParams.Add("ticket", ticket);
            requestParams.Add("groupId", groupId);
            requestParams.Add("folderId", folderId);
            requestParams.Add("versionId", versionId);
            requestParams.Add("linkedId", linkedId);
            requestParams.Add("comments", comments);
            requestParams.Add("convertToPdf", convertToPDF.ToString());
            requestParams.Add("source", source);
            requestParams.Add("certificateId", certificateId);
            requestParams.Add("filename", fileInfo.Name);
            requestParams.Add("newVersion", saveAsNewVersion.ToString());

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "fileUpload"),
                requestParams, fileInfo, "file");

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var fileUploadResponse = (FileUploadResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(FileUploadResponse));

            if (fileUploadResponse.res == "ok")
            {
                var fileUpload = new UploadResult()
                {
                    nodeId = fileUploadResponse.nodeId,
                    modifiedTime = fileUploadResponse.modifiedTime
                };

                return fileUpload;
            }
            else
                throw new RestResponseException(fileUploadResponse.errorDesc);
        }

        public static async Task<UploadResult> UploadAsync(string ticket, string groupId, string versionId, string folderId,
            string comments, string linkedId, bool saveAsNewVersion, string certificateId, string source,
            bool convertToPDF, FileInfo fileInfo)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var requestParams = new Dictionary<string, string>();
            requestParams.Add("ticket", ticket);
            requestParams.Add("groupId", groupId);
            requestParams.Add("folderId", folderId);
            requestParams.Add("versionId", versionId);
            requestParams.Add("linkedId", linkedId);
            requestParams.Add("comments", comments);
            requestParams.Add("convertToPdf", convertToPDF.ToString());
            requestParams.Add("source", source);
            requestParams.Add("certificateId", certificateId);
            requestParams.Add("filename", fileInfo.Name);
            requestParams.Add("newVersion", saveAsNewVersion.ToString());

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "fileUpload"),
                requestParams, fileInfo, "file");

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var fileUploadResponse = (FileUploadResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(FileUploadResponse));

            if (fileUploadResponse.res == "ok")
            {
                var result = new UploadResult()
                {
                    nodeId = fileUploadResponse.nodeId,
                    modifiedTime = fileUploadResponse.modifiedTime
                };

                return result;
            }
            else
                throw new RestResponseException(fileUploadResponse.errorDesc);
        }

        public static async Task<UploadResult> ProcessUploadAsync(string ticket, string processId, string source, FileInfo fileInfo)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var requestParams = new Dictionary<string, string>();
            requestParams.Add("ticket", ticket);
            requestParams.Add("processId", processId);
            requestParams.Add("source", source);
            requestParams.Add("filename", fileInfo.Name);

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "fileUpload"),
                requestParams, fileInfo, "file");

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var fileUploadResponse = (FileUploadResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(FileUploadResponse));

            if (fileUploadResponse.res == "ok")
            {
                var result = new UploadResult()
                {
                    nodeId = fileUploadResponse.nodeId,
                    modifiedTime = fileUploadResponse.modifiedTime
                };

                return result;
            }
            else
                throw new RestResponseException(fileUploadResponse.errorDesc);
        }

        public static bool Download(string ticket, string fileId, string path)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(fileId))
            {
                throw new ArgumentNullException("fileId");
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("filePath");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("{0}/{1}", ticket, fileId);
            var response = new RESTfulService().SendGetRequest(string.Format("{0}/{1}/{2}", restServiceUrl, "viewDocument", request));

            if (response == null || response.Length == 0)
            {
                return false;
            }

            try
            {
                var responseFile = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
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

        public static Stream Download(string ticket, string fileId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(fileId))
            {
                throw new ArgumentNullException("fileId");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("{0}/{1}", ticket, fileId);
            var response = new RESTfulService().SendGetRequest(string.Format("{0}/{1}/{2}", restServiceUrl, "viewDocument", request));

            if (response == null || response.Length == 0)
            {
                return null;
            }

            return response;
        }

        public static async Task<bool> DownloadAsync(string ticket, string fileId, string path)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(fileId))
            {
                throw new ArgumentNullException("fileId");
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException("filePath");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("{0}/{1}", ticket, fileId);
            var response = await new RESTfulService().SendGetRequestAsync(string.Format("{0}/{1}/{2}", restServiceUrl, "viewDocument", request));

            if (response == null || response.Length == 0)
            {
                return false;
            }

            var file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            response.Position = 0;
            await response.CopyToAsync(file);
            response.Close();

            await file.FlushAsync();
            file.Close();

            return true;
        }

        public static async Task<Stream> DownloadAsync(string ticket, string fileId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(fileId))
            {
                throw new ArgumentNullException("fileId");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("{0}/{1}", ticket, fileId);
            var response = await new RESTfulService().SendGetRequestAsync(string.Format("{0}/{1}/{2}", restServiceUrl, "viewDocument", request));

            if (response == null || response.Length == 0)
            {
                return null;
            }

            return response;
        }

        public static List<UserGroup> GetUserGroups(string ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}", ticket);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "getUserGroups"), request);

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
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static async Task<List<UserGroup>> GetUserGroupsAsync(string ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}", ticket);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "getUserGroups"), request);

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
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static List<Documentary> GetDocumentaryTypesFake(string path)
        {
            FileStream fs = File.OpenRead(path);

            var text = new StreamReader(fs).ReadToEnd();

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(text));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (GetDocumentaryTypesResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetDocumentaryTypesResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.elements;
            }
            else
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static List<Documentary> GetDocumentaryTypes(string ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}", ticket);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "getDocumentTypes"), request);

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (GetDocumentaryTypesResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetDocumentaryTypesResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.elements;
            }
            else
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static async Task<List<Documentary>> GetDocumentaryTypesAsync(string ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}", ticket);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "getDocumentTypes"), request);

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (GetDocumentaryTypesResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetDocumentaryTypesResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.elements;
            }
            else
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static List<ScanHistory> GetScanHistory(string ticket, long startDate)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var historyList = new List<ScanHistory>();
            var start = 0;
            var max = 99;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (GetScanHistoryResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&startDate={1}", ticket, startDate.ToString());

            do
            {
                response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "scan/getScanHistory"), string.Format("{0}&start={1}&max={2}", request, start, max));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (GetScanHistoryResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetScanHistoryResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.elements.Count > 0)
                    {
                        historyList.AddRange(responseObject.elements);
                    }
                    else
                    {
                        responseComplete = true;
                    }
                }
                else
                    throw new RestResponseException(responseObject.errorDesc);

                start += max++;
                max += 99;

            }
            while (!responseComplete);

            return historyList;
        }

        public static async Task<List<ScanHistory>> GetScanHistoryAsync(string ticket, long startDate)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var historyList = new List<ScanHistory>();
            var start = 0;
            var max = 99;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (GetScanHistoryResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&startDate={1}", ticket, startDate.ToString());

            do
            {

                response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "scan/getScanHistory"), string.Format("{0}&start={1}&max={2}", request, start, max));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (GetScanHistoryResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetScanHistoryResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.elements.Count > 0)
                    {
                        historyList.AddRange(responseObject.elements);
                    }
                    else
                    {
                        responseComplete = true;
                    }
                }
                else
                    throw new RestResponseException(responseObject.errorDesc);

                start += max++;
                max += 99;

            }
            while (!responseComplete);

            return historyList;
        }

        public static List<SearchResult> Search(string ticket, string text, string filterFileName, string searchType)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException("text");
            }

            if (string.IsNullOrWhiteSpace(searchType))
            {
                throw new ArgumentNullException("searchType");
            }

            var results = new List<SearchResult>();
            var start = 0;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (SearchResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&text={1}&searchType={2}&filterFileName={3}", ticket, text, searchType, filterFileName);

            do
            {
                response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "search"), string.Format("{0}&start={1}&max=99", request, start));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (SearchResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(SearchResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.documents.Count > 0)
                    {
                        results.AddRange(responseObject.documents);
                    }
                    else
                    {
                        responseComplete = true;
                    }
                }
                else
                    throw new RestResponseException(responseObject.errorDesc);

                start += 100;
            }
            while (!responseComplete);

            return results;
        }

        public static async Task<List<SearchResult>> SearchAsync(string ticket, string text, string filterFileName, string searchType)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException("text");
            }

            if (string.IsNullOrWhiteSpace(searchType))
            {
                throw new ArgumentNullException("searchType");
            }

            var results = new List<SearchResult>();
            var start = 0;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (SearchResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&text={1}&searchType={2}&filterFileName={3}", ticket, text, searchType, filterFileName);

            do
            {
                response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "search"), string.Format("{0}&start={1}&max=99", request, start));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (SearchResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(SearchResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.documents.Count > 0)
                    {
                        results.AddRange(responseObject.documents);
                    }
                    else
                    {
                        responseComplete = true;
                    }
                }
                else
                    throw new RestResponseException(responseObject.errorDesc);

                start += 100;
            }
            while (!responseComplete);

            return results;
        }

        public static List<Project> ListProjects(string ticket, string communityId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(communityId))
            {
                throw new ArgumentNullException("communityId");
            }

            var projects = new List<Project>();
            var start = 0;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (ListProjectsResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&cmmId={1}&includeClosedProjects=false", ticket, communityId);

            do
            {
                response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "projects/listProjects"), string.Format("{0}&start={1}&max=99", request, start));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (ListProjectsResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(ListProjectsResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.elements.Count > 0)
                    {
                        projects.AddRange(responseObject.elements);
                    }
                    else
                    {
                        responseComplete = true;
                    }
                }
                else
                    throw new RestResponseException(responseObject.errorDesc);

                start += 100;
            }
            while (!responseComplete);

            return projects;
        }

        public static async Task<List<Project>> ListProjectsAsync(string ticket, string communityId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(communityId))
            {
                throw new ArgumentNullException("communityId");
            }

            var projects = new List<Project>();
            var start = 0;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (ListProjectsResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&cmmId={1}&includeClosedProjects=false", ticket, communityId);

            do
            {
                response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "projects/listProjects"), string.Format("{0}&start={1}&max=99", request, start));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (ListProjectsResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(ListProjectsResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.elements.Count > 0)
                    {
                        projects.AddRange(responseObject.elements);
                    }
                    else
                    {
                        responseComplete = true;
                    }
                }
                else
                    throw new RestResponseException(responseObject.errorDesc);

                start += 100;
            }
            while (!responseComplete);

            return projects;
        }

        public static List<Certificate> ListCertificates(string ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}", ticket);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "organization/listCertificates"), request);

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (ListCertificatesResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(ListCertificatesResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.elements;
            }
            else
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static async Task<List<Certificate>> ListCertificatesAsync(string ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}", ticket);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "organization/listCertificates"), request);

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (ListCertificatesResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(ListCertificatesResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.elements;
            }
            else
                throw new RestResponseException(responseObject.errorDesc);
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

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&includeFolders=true&includeFiles=false", ticket, groupId, parentFolderId);

            do
            {
                response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "getFilesFolders"), string.Format("{0}&firstResult={1}&maxResults={2}", request, firstResult, maxResults));
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
                    throw new RestResponseException(responseObject.errorDesc);

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

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&includeFolders=true&includeFiles=false", ticket, groupId, parentFolderId);

            do
            {
                response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "getFilesFolders"), string.Format("{0}&firstResult={1}&maxResults={2}", request, firstResult, maxResults));
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
                    throw new RestResponseException(responseObject.errorDesc);

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

            var files = new List<FileFolder>();
            var firstResult = 0;
            var maxResults = 99;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (GetFilesFoldersResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&includeFolders=false&includeFiles=true", ticket, groupId, parentFolderId);

            do
            {
                response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "getFilesFolders"), string.Format("{0}&firstResult={1}&maxResults={2}", request, firstResult, maxResults));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (GetFilesFoldersResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetFilesFoldersResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.filesfolders.Count > 0)
                    {
                        files.AddRange(responseObject.filesfolders);
                    }
                    else
                    {
                        responseComplete = true;
                    }
                }
                else
                    throw new RestResponseException(responseObject.errorDesc);

                firstResult += maxResults++;
                maxResults += 99;

            }
            while (!responseComplete);

            return files;
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

            var files = new List<FileFolder>();
            var firstResult = 0;
            var maxResults = 99;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (GetFilesFoldersResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&includeFolders=false&includeFiles=true", ticket, groupId, parentFolderId);

            do
            {
                response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "getFilesFolders"), string.Format("{0}&firstResult={1}&maxResults={2}", request, firstResult, maxResults));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (GetFilesFoldersResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetFilesFoldersResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.filesfolders.Count > 0)
                    {
                        files.AddRange(responseObject.filesfolders);
                    }
                    else
                    {
                        responseComplete = true;
                    }
                }
                else
                    throw new RestResponseException(responseObject.errorDesc);

                firstResult += maxResults++;
                maxResults += 99;

            }
            while (!responseComplete);

            return files;
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

            var filesFolders = new List<FileFolder>();
            var firstResult = 0;
            var maxResults = 99;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (GetFilesFoldersResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&includeFolders=true&includeFiles=true", ticket, groupId, parentFolderId);

            do
            {
                response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "getFilesFolders"), string.Format("{0}&firstResult={1}&maxResults={2}", request, firstResult, maxResults));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (GetFilesFoldersResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetFilesFoldersResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.filesfolders.Count > 0)
                    {
                        filesFolders.AddRange(responseObject.filesfolders);
                    }
                    else
                    {
                        responseComplete = true;
                    }
                }
                else
                    throw new RestResponseException(responseObject.errorDesc);

                firstResult += maxResults++;
                maxResults += 99;

            }
            while (!responseComplete);

            return filesFolders;
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

            var filesFolders = new List<FileFolder>();
            var firstResult = 0;
            var maxResults = 99;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (GetFilesFoldersResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&includeFolders=true&includeFiles=true", ticket, groupId, parentFolderId);

            do
            {
                response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "getFilesFolders"), string.Format("{0}&firstResult={1}&maxResults={2}", request, firstResult, maxResults));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (GetFilesFoldersResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetFilesFoldersResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.filesfolders.Count > 0)
                    {
                        filesFolders.AddRange(responseObject.filesfolders);
                    }
                    else
                    {
                        responseComplete = true;
                    }
                }
                else
                    throw new RestResponseException(responseObject.errorDesc);

                firstResult += maxResults++;
                maxResults += 99;

            }
            while (!responseComplete);

            return filesFolders;
        }

        public static bool UpdateFavorite(string ticket, string itemId, string action, int type)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new ArgumentNullException("itemId");
            }

            if (string.IsNullOrWhiteSpace(action))
            {
                throw new ArgumentNullException("action");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&&itemId={1}&action={2}&type={3}", ticket, itemId, action, type.ToString());
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "favourite/updateFavouriteItem"), request);

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
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static async Task<bool> UpdateFavoriteAsync(string ticket, string itemId, string action, int type)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new ArgumentNullException("itemId");
            }

            if (string.IsNullOrWhiteSpace(action))
            {
                throw new ArgumentNullException("action");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&&itemId={1}&action={2}&type={3}", ticket, itemId, action, type.ToString());
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "favourite/updateFavouriteItem"), request);

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
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static List<RecentFile> GetFavouriteItems(string ticket, int type, string filterText)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&type={1}&filterText={2}&max=99", ticket, type, filterText);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "getFavouriteItems"), request);
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (GetRecentFilesResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetRecentFilesResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.elements;
            }
            else
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static async Task<List<RecentFile>> GetFavouriteItemsAsync(string ticket, int type, string filterText)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&type={1}&filterText={2}&max=99", ticket, type, filterText);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "favourites/getFavouriteItems"), request);
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (GetRecentFilesResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetRecentFilesResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.elements;
            }
            else
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static List<RecentFile> GetFavoriteDocuments(string ticket, string filterText)
        {
            return DokuFlexService.GetFavouriteItems(ticket, 1, filterText);
        }

        public static async Task<List<RecentFile>> GetFavoriteDocumentsAsync(string ticket, string filterText)
        {
            return await DokuFlexService.GetFavouriteItemsAsync(ticket, 1, filterText);
        }

        public static List<RecentFile> GetRecentDocuments(string ticket, string filterText)
        {
            return DokuFlexService.GetFavouriteItems(ticket, 2, filterText);
        }

        public static async Task<List<RecentFile>> GetRecentDocumentsAsync(string ticket, string filterText)
        {
            return await DokuFlexService.GetFavouriteItemsAsync(ticket, 2, filterText);
        }

        public static GetDocumentMetadataResponse GetDocumentMetadada(string ticket, string uuid)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(uuid))
            {
                throw new ArgumentNullException("uuid");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&uuid={1}", ticket, uuid);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "getDocumentMetadata"), request);

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (GetDocumentMetadataResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetDocumentMetadataResponse));

            if (responseObject.res == "ok")
            {
                return responseObject;
            }
            else
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static async Task<GetDocumentMetadataResponse> GetDocumentMetadadaAsync(string ticket, string uuid)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(uuid))
            {
                throw new ArgumentNullException("uuid");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&uuid={1}", ticket, uuid);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "getDocumentMetadata"), request);

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (GetDocumentMetadataResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetDocumentMetadataResponse));

            if (responseObject.res == "ok")
            {
                return responseObject;
            }
            else
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static UpdateTaskResponse UpdateTask(string ticket, string communityId, string title,
            long startDate, long endDate, string description, string category,
            string subCategory, string categoryStatus, string projectId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(communityId))
            {
                throw new ArgumentNullException("communityId");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException("title");
            }

            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentNullException("category");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&communityId={1}&title={2}&startDate={3}&endDate={4}&description={5}",
                ticket, communityId, title, startDate.ToString(), endDate.ToString(), description);

                request = request + string.Format("&category={0}&subCategory={1}&taskType={2}&status={3}&projectId={4}&updateUsers=false&percentageFinished=0",
                category, subCategory, "1", categoryStatus, projectId);

            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "tasks/updateTask"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (UpdateTaskResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(UpdateTaskResponse));

            if (responseObject.res == "ok")
            {
                return responseObject;
            }
            else
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static async Task<string> UpdateTaskAsync(string ticket, string communityId, string title,
            long startDate, long endDate, string description, string category,
            string subCategory, string categoryStatus)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(communityId))
            {
                throw new ArgumentNullException("communityId");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException("title");
            }

            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentNullException("category");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&communityId={1}&title={2}&startDate={3}&endDate={4}&description={5}&category={6}&subCategory={7}&taskType={8}&status={9}&updateUsers=false&percentageFinished=0",
                ticket, communityId, title, startDate.ToString(), endDate.ToString(), description, category, subCategory, "1", categoryStatus);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "tasks/updateTask"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (UpdateTaskResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(UpdateTaskResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.id;
            }
            else
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static string LinkDocToTask(string ticket, string taskId, string itemId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(taskId))
            {
                throw new ArgumentNullException("taskId");
            }

            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new ArgumentNullException("itemId");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&taskId={1}&itemId={2}", ticket, taskId, itemId);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "tasks/linkDocToTask"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (LinkDocToTaskResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(LinkDocToTaskResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.res;
            }
            else
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static async Task<string> LinkDocToTaskAsync(string ticket, string taskId, string itemId)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(taskId))
            {
                throw new ArgumentNullException("taskId");
            }

            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new ArgumentNullException("itemId");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&taskId={1}&itemId={2}", ticket, taskId, itemId);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "tasks/linkDocToTask"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (LinkDocToTaskResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(LinkDocToTaskResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.res;
            }
            else
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static List<Category> ListCategories(string ticket, string communityId, string projectId, string categoryType)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&communityId={1}&projectId={2}&categoryType={3}", ticket, communityId, projectId, categoryType);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "configuration/listCategories"), request);

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (ListCategoriesResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(ListCategoriesResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.elements;
            }
            else
                throw new RestResponseException(responseObject.errorDesc);
        }

        public static async Task<List<Category>> ListCategoriesAsync(string ticket, string communityId, string categoryType)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&communityId={1}&categoryType={2}", ticket, communityId, categoryType);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "configuration/listCategories"), request);

            //Deserialize json response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (ListCategoriesResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(ListCategoriesResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.elements;
            }
            else
                throw new RestResponseException(responseObject.errorDesc);
        }
    }
}
