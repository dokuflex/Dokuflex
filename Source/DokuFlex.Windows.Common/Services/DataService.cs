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
    using System.Text;

    public class DataService : IDataService
    {
        public string Login(Credentials credentials)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<string> LoginAsync(Credentials credentials)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public bool DeleteFile(string ticket, string groupId, string fileId)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<bool> DeleteFileAsync(string ticket, string groupId, string fileId)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public bool UpdateDocumentMetadata(string ticket, string documentType, string uuid, params DokuField[] data)
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
                switch (dokuField.type)
                {
                    case "F":

                    case "H":
                        var epochTime = ((DateTime)dokuField.value).ToUnixEpoch();
                        obj[dokuField.key] = epochTime;
                        break;

                    case "M":
                        obj[dokuField.key] = (double)dokuField.value;
                        break;

                    case "N":
                        obj[dokuField.key] = (double)dokuField.value;
                        break;

                    default:
                        obj[dokuField.key] = dokuField.value.ToString();
                        break;
                }
            }

            var serializedObj = JsonConvert.SerializeObject(obj);
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<bool> UpdateDocumentMetadataAsync(string ticket, string documentType, string uuid, params DokuField[] data)
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
                switch (dokuField.type)
                {
                    case "F":

                    case "H":
                        var epochTime = ((DateTime)dokuField.value).ToUnixEpoch();
                        obj[dokuField.key] = epochTime;
                        break;

                    case "M":
                        obj[dokuField.key] = (double)dokuField.value;
                        break;

                    case "N":
                        obj[dokuField.key] = (double)dokuField.value;
                        break;

                    default:
                        obj[dokuField.key] = dokuField.value.ToString();
                        break;
                }
            }

            var serializedObj = JsonConvert.SerializeObject(obj);
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public string ProcessUpdateData(string ticket, string processId, string communityId, string dataId = "", params ProcessData[] data)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException(nameof(ticket));
            }

            if (string.IsNullOrWhiteSpace(processId))
            {
                throw new ArgumentNullException(nameof(processId));
            }

            if (string.IsNullOrWhiteSpace(communityId))
            {
                throw new ArgumentNullException(nameof(communityId));
            }

            //convert metadata to json
            var obj = new JObject();

            foreach (var processData in data)
            {
                switch (processData.type)
                {
                    case "F":

                    case "H":
                        var epochTime = ((DateTime)processData.value).ToUnixEpoch();
                        obj[processData.fieldName] = epochTime;
                        break;

                    case "M":
                        obj[processData.fieldName] = (double)processData.value;
                        break;

                    case "N":
                        obj[processData.fieldName] = (double)processData.value;
                        break;

                    default:
                        obj[processData.fieldName] = processData.value.ToString();
                        break;
                }
            }

            var serializedObj = JsonConvert.SerializeObject(obj);
            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&processId={1}&communityId={2}&dataId={3}&data={4}", ticket, processId, communityId, dataId, serializedObj);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "process/updateData"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (ProcessUpdateDataResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(ProcessUpdateDataResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.id;
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<string> ProcessUpdateDataAsync(string ticket, string processId, string communityId, string dataId = "", params ProcessData[] data)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException(nameof(ticket));
            }

            if (string.IsNullOrWhiteSpace(processId))
            {
                throw new ArgumentNullException(nameof(processId));
            }

            if (string.IsNullOrWhiteSpace(communityId))
            {
                throw new ArgumentNullException(nameof(communityId));
            }

            //convert metadata to json
            var obj = new JObject();

            foreach (var processData in data)
            {
                switch (processData.type)
                {
                    case "F":

                    case "H":
                        var epochTime = ((DateTime)processData.value).ToUnixEpoch();
                        obj[processData.fieldName] = epochTime;
                        break;

                    case "M":
                        obj[processData.fieldName] = (double)processData.value;
                        break;

                    case "N":
                        obj[processData.fieldName] = (double)processData.value;
                        break;

                    default:
                        obj[processData.fieldName] = processData.value?.ToString();
                        break;
                }
            }

            var serializedObj = JsonConvert.SerializeObject(obj);
            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&processId={1}&communityId={2}&dataId={3}&data={4}", ticket, processId, communityId, dataId, serializedObj);
            LogFactory.CreateLog().LogInfo($"Sending request \"{request}\"");
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "process/updateData"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (ProcessUpdateDataResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(ProcessUpdateDataResponse));

            if (responseObject.res == "ok")
            {
                return responseObject.id;
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc, responseObject.error, request);
            }
        }

        public async Task<UploadResult> ProcessUploadAsync(string ticket, string processId, string dataId, string source, FileInfo fileInfo)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var requestParams = new Dictionary<string, string>
            {
                { "ticket", ticket },
                { "processId", processId },
                { "dataId", dataId },
                { "source", source },
                { "filename", fileInfo.Name }
            };

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "process/fileUpload"),
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
            {
                throw new RestResponseException(fileUploadResponse.errorDesc);
            }
        }

        public bool DeleteFolder(string ticket, string groupId, string folderId)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<bool> DeleteFolderAsync(string ticket, string groupId, string folderId)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public bool RenameFileFolder(string ticket, string groupId, string fileFolderId, string title)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<bool> RenameFileFolderAsync(string ticket, string groupId, string fileFolderId, string title)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public string CreateFolder(string ticket, string groupId, string parentNodeId, string folderName)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<string> CreateFolderAsync(string ticket, string groupId, string parentNodeId, string folderName)
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
            string request = String.Empty;
            if (String.IsNullOrEmpty(parentNodeId))
            {
                request = string.Format("ticket={0}&groupId={1}&folderName={2}", ticket, groupId, folderName);
            }
            else
            {
                request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&folderName={3}", ticket, groupId, parentNodeId, folderName);
            }
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public UploadResult Upload(string ticket, string groupId, string versionId, string folderId,
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
            {
                throw new RestResponseException(fileUploadResponse.errorDesc);
            }
        }

        public async Task<UploadResult> UploadAsync(string ticket, string groupId, string versionId, string folderId,
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
            if (!String.IsNullOrEmpty(folderId))
            {
                requestParams.Add("folderId", folderId);
            }
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
            {
                throw new RestResponseException(fileUploadResponse.errorDesc);
            }
        }

        public async Task<UploadResult> UploadAsync(string ticket, string groupId, string versionId, string folderId,
            string comments, string linkedId, string externalId, bool saveAsNewVersion, string certificateId, string source,
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
            requestParams.Add("externalId", externalId);
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
            {
                throw new RestResponseException(fileUploadResponse.errorDesc);
            }
        }

        public async Task<UploadResult> UploadAsync(string ticket, string groupId, string versionId, string folderId, string comments, string linkedId, string externalId, bool saveAsNewVersion, string certificateId, string certificatePass, string source, bool convertToPDF, FileInfo fileInfo)
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
            requestParams.Add("externalId", externalId);
            requestParams.Add("comments", comments);
            requestParams.Add("convertToPdf", convertToPDF.ToString());
            requestParams.Add("source", source);
            requestParams.Add("certificateId", certificateId);
            requestParams.Add("certificatePass", certificatePass);
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
            {
                throw new RestResponseException(fileUploadResponse.errorDesc);
            }
        }

        public bool Download(string ticket, string fileId, string path)
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

        public Stream Download(string ticket, string fileId)
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

        public async Task<bool> DownloadAsync(string ticket, string fileId, string path)
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

        public async Task<Stream> DownloadAsync(string ticket, string fileId)
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

        public List<UserGroup> GetUserGroups(string ticket)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<List<UserGroup>> GetUserGroupsAsync(string ticket)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public List<Documentary> GetDocumentaryTypesFake(string path)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public List<Documentary> GetDocumentaryTypes(string ticket)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<List<Documentary>> GetDocumentaryTypesAsync(string ticket)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public List<ScanHistory> GetScanHistory(string ticket, long startDate)
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
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                start += max++;
                max += 99;

            }
            while (!responseComplete);

            return historyList;
        }

        public async Task<List<ScanHistory>> GetScanHistoryAsync(string ticket, long startDate)
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
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                start += max++;
                max += 99;

            }
            while (!responseComplete);

            return historyList;
        }

        public List<SearchResult> Search(string ticket, string text, string filterFileName, string searchType)
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
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                start += 100;
            }
            while (!responseComplete);

            return results;
        }

        public async Task<List<SearchResult>> SearchAsync(string ticket, string text, string filterFileName, string searchType)
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
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                start += 100;
            }
            while (!responseComplete);

            return results;
        }

        public async Task<List<SearchUserResult>> SearchUserAsync(string ticket, string text)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException("text");
            }

            var results = new List<SearchUserResult>();
            var start = 0;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (SearchUserResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&text={1}&searchType=u", ticket, text);

            do
            {
                response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "search"), string.Format("{0}&start={1}&max=99", request, start));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (SearchUserResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(SearchUserResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.users.Count > 0)
                    {
                        results.AddRange(responseObject.users);
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

                start += 100;
            }
            while (!responseComplete);

            return results;
        }

        public List<Project> ListProjects(string ticket, string communityId)
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
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                start += 100;
            }
            while (!responseComplete);

            return projects;
        }

        public async Task<List<Project>> ListProjectsAsync(string ticket, string communityId)
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
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                start += 100;
            }
            while (!responseComplete);

            return projects;
        }

        public List<Process> ListProcesses(string ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var processes = new List<Process>();
            var start = 0;
            var responseComplete = false;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (ListProcessesResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}", ticket);

            do
            {
                response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "process/listProcesses"), string.Format("{0}&start={1}&max=99", request, start));
                jsonReader = new JsonTextReader(new StringReader(response));
                jObject = JObject.Load(jsonReader);
                responseObject = (ListProcessesResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(ListProcessesResponse));

                if (responseObject.res == "ok")
                {
                    if (responseObject.elements.Count > 0)
                    {
                        processes.AddRange(responseObject.elements);
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

                start += 100;
            }
            while (!responseComplete);

            return processes;
        }

        public async Task<List<Process>> ListProcessesAsync(string ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var processes = new List<Process>();
            var start = 0;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (ListProcessesResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}", ticket);

            response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "process/listProcesses"), string.Format("{0}&start={1}&max=99", request, start));
            jsonReader = new JsonTextReader(new StringReader(response));
            jObject = JObject.Load(jsonReader);
            responseObject = (ListProcessesResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(ListProcessesResponse));

            if (responseObject.res == "ok")
            {
                if (responseObject.elements.Count > 0)
                {
                    processes.AddRange(responseObject.elements);
                }
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }

            return processes;
        }

        public object searchAppData(string ticket, string communityId, string processId, string filterStatus)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(communityId))
            {
                throw new ArgumentNullException("communityId");
            }

            if (string.IsNullOrWhiteSpace(processId))
            {
                throw new ArgumentNullException("processId");
            }

            if (string.IsNullOrWhiteSpace(filterStatus))
            {
                throw new ArgumentNullException("filterStatus");
            }

            var processes = new List<Process>();
            var start = 0;
            var response = string.Empty;
            var jsonSerializer = new JsonSerializer();
            var jsonReader = (JsonTextReader)null;
            var jObject = (JObject)null;
            var responseObject = (ListProcessesResponse)null;

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&processId={1}&communityId={2}&filterStatus={3}", ticket, processId, communityId, filterStatus);

            response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "process/searchAppData"), string.Format("{0}&start={1}&max=99", request, start));
            jsonReader = new JsonTextReader(new StringReader(response));
            jObject = JObject.Load(jsonReader);
            responseObject = (ListProcessesResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(ListProcessesResponse));

            if (responseObject.res == "ok")
            {
                if (responseObject.elements.Count > 0)
                {
                    processes.AddRange(responseObject.elements);
                }
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }

            return processes;
        }

        public List<Certificate> ListCertificates(string ticket)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<List<Certificate>> ListCertificatesAsync(string ticket)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public List<FileFolder> GetFolders(string ticket, string groupId, string parentFolderId)
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
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                firstResult += maxResults++;
                maxResults += 99;

            }
            while (!responseComplete);

            return folders;
        }

        public async Task<List<FileFolder>> GetFoldersAsync(string ticket, string groupId, string parentFolderId)
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
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                firstResult += maxResults++;
                maxResults += 99;

            }
            while (!responseComplete);

            return folders;
        }

        public List<FileFolder> GetFiles(string ticket, string groupId, string parentFolderId)
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
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                firstResult += maxResults++;
                maxResults += 99;

            }
            while (!responseComplete);

            return files;
        }

        public async Task<List<FileFolder>> GetFilesAsync(string ticket, string groupId, string parentFolderId)
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
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                firstResult += maxResults++;
                maxResults += 99;

            }
            while (!responseComplete);

            return files;
        }

        public List<FileFolder> GetFilesFolders(string ticket, string groupId, string parentFolderId)
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
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                firstResult += maxResults++;
                maxResults += 99;

            }
            while (!responseComplete);

            return filesFolders;
        }

        public async Task<List<FileFolder>> GetFilesFoldersAsync(string ticket, string groupId, string parentFolderId)
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
            string request = String.Empty;
            if (String.IsNullOrEmpty(parentFolderId))
            {
                request = string.Format("ticket={0}&groupId={1}&includeFolders=true&includeFiles=true", ticket, groupId);
            }
            else
            {
                request = string.Format("ticket={0}&groupId={1}&parentNodeId={2}&includeFolders=true&includeFiles=true", ticket, groupId, parentFolderId);
            }

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
                {
                    throw new RestResponseException(responseObject.errorDesc);
                }

                firstResult += maxResults++;
                maxResults += 99;

            }
            while (!responseComplete);

            return filesFolders;
        }

        public bool UpdateFavorite(string ticket, string itemId, string action, int type)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<bool> UpdateFavoriteAsync(string ticket, string itemId, string action, int type)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public List<RecentFile> GetFavouriteItems(string ticket, int type, string filterText)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<List<RecentFile>> GetFavouriteItemsAsync(string ticket, int type, string filterText)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public List<RecentFile> GetFavoriteDocuments(string ticket, string filterText)
        {
            return GetFavouriteItems(ticket, 1, filterText);
        }

        public async Task<List<RecentFile>> GetFavoriteDocumentsAsync(string ticket, string filterText)
        {
            return await GetFavouriteItemsAsync(ticket, 1, filterText);
        }

        public List<RecentFile> GetRecentDocuments(string ticket, string filterText)
        {
            return GetFavouriteItems(ticket, 2, filterText);
        }

        public async Task<List<RecentFile>> GetRecentDocumentsAsync(string ticket, string filterText)
        {
            return await GetFavouriteItemsAsync(ticket, 2, filterText);
        }

        public GetDocumentMetadataResponse GetDocumentMetadada(string ticket, string uuid)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<GetDocumentMetadataResponse> GetDocumentMetadadaAsync(string ticket, string uuid)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public string UpdateTask(string ticket, string communityId, string title,
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

                request = request + string.Format("category={0}&subCategory={1}&taskType={2}&status={3}&projectId={4}&updateUsers=false&percentageFinished=0",
                category, subCategory, "1", categoryStatus, projectId);

            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "tasks/updateTask"), request);

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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<string> UpdateTaskAsync(string ticket, string communityId, string title,
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public string LinkDocToTask(string ticket, string taskId, string itemId)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<string> LinkDocToTaskAsync(string ticket, string taskId, string itemId)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public List<Category> ListCategories(string ticket, string communityId, string categoryType)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&communityId={1}&categoryType={2}", ticket, communityId, categoryType);
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<List<Category>> ListCategoriesAsync(string ticket, string communityId, string categoryType)
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
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public GetAppInfoResponse GetAppInfo(string appId)
        {
            if (string.IsNullOrWhiteSpace(appId))
            {
                throw new ArgumentNullException("appId");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("appId={0}", appId);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "getAppInfo"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (GetAppInfoResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetAppInfoResponse));

            if (responseObject.res == "ok")
            {
                return responseObject;
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public async Task<GetAppInfoResponse> GetAppInfoAsync(string appId)
        {
            if (string.IsNullOrWhiteSpace(appId))
            {
                throw new ArgumentNullException("appId");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("appId={0}", appId);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "apps/getAppInfo"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (GetAppInfoResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(GetAppInfoResponse));

            if (responseObject.res == "ok")
            {
                return responseObject;
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }

        public bool AddImageSignature(System.IO.FileInfo sigFile, System.IO.FileInfo sigImageFile, string ticketId, string nodeId, params SignaturePosition[] signaturePositions)
        {
            if (string.IsNullOrWhiteSpace(ticketId))
            {
                throw new ArgumentNullException("ticketId");
            }

            if (string.IsNullOrWhiteSpace(nodeId))
            {
                throw new ArgumentNullException("nodeId");
            }

            //convert signature positions to json
            var jArray = new JArray();

            foreach (var position in signaturePositions)
            {
                var jObj = new JObject();
                jObj["page"] = position.page;
                jObj["x"] = position.x;
                jObj["y"] = position.y;

                jArray.Add(jObj);
            }

            var requestParams = new Dictionary<string, string>();
            requestParams.Add("ticket", ticketId);
            requestParams.Add("nodeId", nodeId);
            requestParams.Add("signaturePosition", JsonConvert.SerializeObject(jArray));

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var response = new RESTfulService().SendPostRequest(string.Format("{0}/{1}", restServiceUrl, "signature/addImageSignature"),
                requestParams, sigFile, sigImageFile, "SigFile", "SigImageFile");

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

                return true;
            }
            else
            {
                throw new RestResponseException(fileUploadResponse.errorDesc);
            }
        }

        public async Task<bool> AddImageSignatureAsync(System.IO.FileInfo sigFile, System.IO.FileInfo sigImageFile, string ticketId, string nodeId, params SignaturePosition[] signaturePositions)
        {
            if (string.IsNullOrWhiteSpace(ticketId))
            {
                throw new ArgumentNullException("ticketId");
            }

            if (string.IsNullOrWhiteSpace(nodeId))
            {
                throw new ArgumentNullException("nodeId");
            }

            //convert signature positions to json
            var jArray = new JArray();

            foreach (var position in signaturePositions)
            {
                var jObj = new JObject();
                jObj["page"] = position.page;
                jObj["x"] = position.x;
                jObj["y"] = position.y;

                jArray.Add(jObj);
            }

            var requestParams = new Dictionary<string, string>();
            requestParams.Add("ticket", ticketId);
            requestParams.Add("nodeId", nodeId);
            requestParams.Add("signaturePosition", JsonConvert.SerializeObject(jArray));

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "signature/addImageSignature"),
                requestParams, sigFile, sigImageFile, "SigFile", "SigImageFile");

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

                return true;
            }
            else
            {
                throw new RestResponseException(fileUploadResponse.errorDesc);
            }
        }

        public async Task<bool> RequestDocumentSignature(string ticket, string itemId, List<string> users)
        {
            if (string.IsNullOrWhiteSpace(ticket))
            {
                throw new ArgumentNullException("ticket");
            }

            if (string.IsNullOrWhiteSpace(itemId))
            {
                throw new ArgumentNullException("itemId");
            }

            if (users.Count == 0)
            {
                throw new ArgumentNullException("action");
            }

            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            var request = string.Format("ticket={0}&&itemId={1}&targetUserId={2}", ticket, itemId, String.Join("##", users));
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "signature/requestDocumentSignature"), request);

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (RestResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(RestResponse));

            if (responseObject.res == "ok")
            {
                return true;
            }
            else
            {
                throw new RestResponseException(responseObject.errorDesc);
            }
        }


        public async Task<bool> SendAppLog(string organizationId, string communityId, string app, string userId, string error, string errorDesc, string errorStack)
        {
            var restServiceUrl = ConfigurationManager.GetValue(Constants.RESTfulServiceUrl);
            StringBuilder request = new StringBuilder();
            request.AppendFormat("app={0}", app);
            if (!String.IsNullOrEmpty(organizationId))
            {
                request.AppendFormat("&organizationId={0}", organizationId);
            }
            if (!String.IsNullOrEmpty(organizationId))
            {
                request.AppendFormat("&organizationId={0}", organizationId);
            }
            if (!String.IsNullOrEmpty(userId))
            {
                request.AppendFormat("&userId={0}", userId);
            }
            request.AppendFormat("&error={0}", error);
            request.AppendFormat("&errorDesc={0}", errorDesc);
            request.AppendFormat("&errorStack={0}", errorStack);
            var response = await new RESTfulService().SendPostRequestAsync(string.Format("{0}/{1}", restServiceUrl, "apps/log"), request.ToString());

            //Deserialize response
            var jsonReader = new JsonTextReader(new StringReader(response));
            var jObject = JObject.Load(jsonReader);
            var jsonSerializer = new JsonSerializer();
            var responseObject = (RestResponse)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(RestResponse));

            if (responseObject.res == "ok")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
