// =================================================================================================================
// Paina Solutions
// DokuFlex
// =================================================================================================================
// ©2013 DokuFlex. All rights reserved. Certain content used with permission from contributors.
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance
// with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software distributed under the License is
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.
// =================================================================================================================

namespace DokuFlex.Windows.Common.Services.RESTful
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Text;
    using System.Net;
    using DokuFlex.Windows.Common.Log;
    using DokuFlex.Windows.Common.Extensions;
    using System.Threading.Tasks;
    using System.Security.Cryptography.X509Certificates;
    using System.Net.Security;

    public class RESTfulService
    {
        private NetworkCredential _credentials;

        private bool _useProxyServer;

        public RESTfulService()
        {
            _credentials = new NetworkCredential();

            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            _credentials.UserName = ConfigurationManager.GetValue(Constants.ProxyUserName);
            _credentials.Password = ConfigurationManager.GetValue(Constants.ProxyPassword);
            _credentials.Domain = ConfigurationManager.GetValue(Constants.ProxyDomain);

            if (!bool.TryParse(ConfigurationManager.GetValue(Constants.UseProxy), out _useProxyServer))
            {
                _useProxyServer = false;
            }
        }

        /// <summary>
        /// Creates a multipart/form-data boundary.
        /// </summary>
        /// <returns>
        /// A dynamically generated form boundary for use in posting multipart/form-data requests.
        /// </returns>
        private string CreateFormDataBoundary()
        {
            return Guid.NewGuid().ToString("N");
        }

        private HttpWebRequest InternalCreateWebRequest(string url)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(url);

            if (_useProxyServer)
            {
                webRequest.Proxy = HttpWebRequest.DefaultWebProxy;
                webRequest.UseDefaultCredentials = false;
                webRequest.Credentials = _credentials;
                webRequest.Proxy.Credentials = _credentials;
                webRequest.ServerCertificateValidationCallback = ValidateServerCertificate;
            }

            return webRequest;
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;

        /// <summary>
        /// Send a post verb request to a service
        /// </summary>
        /// <param name="url">The service URL</param>
        /// <param name="request">Request params</param>
        /// <returns>String with the response</returns>
        public string SendPostRequest(string url, string request)
        {
            var requestBytes = UTF8Encoding.UTF8.GetBytes(request);
            var response = string.Empty;

            var webRequest = (HttpWebRequest)InternalCreateWebRequest(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.KeepAlive = true;
            webRequest.ContentLength = requestBytes.Length;
            webRequest.Proxy = HttpWebRequest.DefaultWebProxy;

            try
            {
                var stream = webRequest.GetRequestStream();
                stream.Write(requestBytes, 0, requestBytes.Length);
                stream.Close();
            }
            catch (Exception e)
            {
                LogFactory.CreateLog().LogError(ErrorMessages.RestServiceFaultRequestError, e, request);

                throw e;
            }

            try
            {
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                var responseReader = new StreamReader(webResponse.GetResponseStream());
                response = responseReader.ReadToEnd();
                responseReader.Close();

            }
            catch (Exception e)
            {
                LogFactory.CreateLog().LogError(ErrorMessages.RestServiceFaultResponseError, e, request);

                throw e;
            }

            return response;
        }

        /// <summary>
        /// Send a post verb request to a service asynchronosly
        /// </summary>
        /// <param name="url">The service URL</param>
        /// <param name="request">Request params</param>
        /// <returns>String with the response</returns>
        public async Task<string> SendPostRequestAsync(string url, string request)
        {
            var requestBytes = UTF8Encoding.UTF8.GetBytes(request);
            var response = string.Empty;

            var webRequest = (HttpWebRequest)InternalCreateWebRequest(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.KeepAlive = true;
            webRequest.ContentLength = requestBytes.Length;
            webRequest.Proxy = HttpWebRequest.DefaultWebProxy;

            var stream = await webRequest.GetRequestStreamAsync();
            await stream.WriteAsync(requestBytes, 0, requestBytes.Length);
            stream.Close();

            var webResponse = await webRequest.GetResponseAsync();
            var responseReader = new StreamReader(webResponse.GetResponseStream());
            response = await responseReader.ReadToEndAsync();
            responseReader.Close();

            return response;
        }

        /// <summary>
        /// Send a post verb request to a service with the multipart/form-data content type
        /// </summary>
        /// <param name="url">The service URL</param>
        /// <param name="request">Request params</param>
        /// <param name="fileToUpload">The file that is going to upload</param>
        /// <param name="fileFormKey">The key of the form</param>
        /// <returns>String with the response</returns>
        public string SendPostRequest(string url, IDictionary<string, string> request,
            FileInfo fileToUpload, string fileFormKey)
        {
            var response = string.Empty;
            var boundary = CreateFormDataBoundary();

            var webRequest = (HttpWebRequest)InternalCreateWebRequest(url);
            webRequest.Method = "POST";
            webRequest.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            webRequest.KeepAlive = true;

            try
            {
                var stream = webRequest.GetRequestStream();
                request.WriteMultipartFormData(stream, boundary);
                fileToUpload.WriteMultipartFormData(stream, boundary, fileFormKey);
                var endBytes = Encoding.UTF8.GetBytes("--" + boundary + "--");
                stream.Write(endBytes, 0, endBytes.Length);
                stream.Close();
            }
            catch (Exception e)
            {

                LogFactory.CreateLog().LogError(ErrorMessages.RestServiceFaultRequestError, e, request);

                throw e;
            }

            try
            {
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                var responseReader = new StreamReader(webResponse.GetResponseStream());
                response = responseReader.ReadToEnd();
                responseReader.Close();

            }
            catch (Exception e)
            {
                LogFactory.CreateLog().LogError(ErrorMessages.RestServiceFaultResponseError, e, request);

                throw e;
            }

            return response;
        }

        /// <summary>
        /// Send a post verb request to a service asynchronosly with the multipart/form-data content type
        /// </summary>
        /// <param name="url">The service URL</param>
        /// <param name="request">Request params</param>
        /// <param name="fileToUpload">The file that is going to upload</param>
        /// <param name="fileFormKey">The key of the form</param>
        /// <returns>String with the response</returns>
        public async Task<string> SendPostRequestAsync(string url, IDictionary<string, string> request,
            FileInfo fileToUpload, string fileFormKey)
        {
            var response = string.Empty;
            var boundary = CreateFormDataBoundary();

            var webRequest = (HttpWebRequest)InternalCreateWebRequest(url);
            webRequest.Method = "POST";
            webRequest.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            webRequest.KeepAlive = true;

            var stream = await webRequest.GetRequestStreamAsync();
            await request.WriteMultipartFormDataAsync(stream, boundary);
            await fileToUpload.WriteMultipartFormDataAsync(stream, boundary, fileFormKey);
            var endBytes = Encoding.UTF8.GetBytes("--" + boundary + "--");
            await stream.WriteAsync(endBytes, 0, endBytes.Length);
            stream.Close();

            var webResponse = await webRequest.GetResponseAsync();
            var responseReader = new StreamReader(webResponse.GetResponseStream());
            response = await responseReader.ReadToEndAsync();
            responseReader.Close();

            return response;
        }

        /// <summary>
        /// Send a post verb request to a service with the multipart/form-data content type
        /// </summary>
        /// <param name="url">The service URL</param>
        /// <param name="request">Request params</param>
        /// <param name="fileToUpload1">The file that is going to upload</param>
        /// <param name="fileToUpload2">The file that is going to upload</param>
        /// <param name="fileFormKey1">The key of the form</param>
        /// <param name="fileFormKey2">The key of the form</param>
        /// <returns>String with the response</returns>
        public string SendPostRequest(string url, IDictionary<string, string> request,
            FileInfo fileToUpload1, FileInfo fileToUpload2, string fileFormKey1, string fileFormKey2)
        {
            var response = string.Empty;
            var boundary = CreateFormDataBoundary();

            var webRequest = (HttpWebRequest)InternalCreateWebRequest(url);
            webRequest.Method = "POST";
            webRequest.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            webRequest.KeepAlive = true;

            try
            {
                var stream = webRequest.GetRequestStream();
                request.WriteMultipartFormData(stream, boundary);
                fileToUpload1.WriteMultipartFormData(stream, boundary, fileFormKey1);
                fileToUpload2.WriteMultipartFormData(stream, boundary, fileFormKey2);
                var endBytes = Encoding.UTF8.GetBytes("--" + boundary + "--");
                stream.Write(endBytes, 0, endBytes.Length);
                stream.Close();
            }
            catch (Exception e)
            {

                LogFactory.CreateLog().LogError(ErrorMessages.RestServiceFaultRequestError, e, request);

                throw e;
            }

            try
            {
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                var responseReader = new StreamReader(webResponse.GetResponseStream());
                response = responseReader.ReadToEnd();
                responseReader.Close();

            }
            catch (Exception e)
            {
                LogFactory.CreateLog().LogError(ErrorMessages.RestServiceFaultResponseError, e, request);

                throw e;
            }

            return response;
        }

        /// <summary>
        /// Send a post verb request to a service asynchronosly with the multipart/form-data content type
        /// </summary>
        /// <param name="url">The service URL</param>
        /// <param name="request">Request params</param>
        /// <param name="fileToUpload1">The file that is going to upload</param>
        /// <param name="fileToUpload2">The file that is going to upload</param>
        /// <param name="fileFormKey1">The key of the form</param>
        /// <param name="fileFormKey2">The key of the form</param>
        /// <returns>String with the response</returns>
        public async Task<string> SendPostRequestAsync(string url, IDictionary<string, string> request,
            FileInfo fileToUpload1, FileInfo fileToUpload2, string fileFormKey1, string fileFormKey2)
        {
            var response = string.Empty;
            var boundary = CreateFormDataBoundary();

            var webRequest = (HttpWebRequest)InternalCreateWebRequest(url);
            webRequest.Method = "POST";
            webRequest.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            webRequest.KeepAlive = true;

            var stream = await webRequest.GetRequestStreamAsync();
            await request.WriteMultipartFormDataAsync(stream, boundary);
            await fileToUpload1.WriteMultipartFormDataAsync(stream, boundary, fileFormKey1);
            await fileToUpload2.WriteMultipartFormDataAsync(stream, boundary, fileFormKey2);
            var endBytes = Encoding.UTF8.GetBytes("--" + boundary + "--");
            await stream.WriteAsync(endBytes, 0, endBytes.Length);
            stream.Close();

            var webResponse = await webRequest.GetResponseAsync();
            var responseReader = new StreamReader(webResponse.GetResponseStream());
            response = await responseReader.ReadToEndAsync();
            responseReader.Close();

            return response;
        }

        /// <summary>
        /// Send a get verb to a service
        /// Is for download content from services... ej: Images, Docs, etc.
        /// </summary>
        /// <param name="url">The service URL</param>
        /// <returns>Stream with the response</returns>
        public MemoryStream SendGetRequest(string url)
        {
            var response = new MemoryStream();

            var webRequest = (HttpWebRequest)InternalCreateWebRequest(url);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.KeepAlive = true;
            webRequest.ContentLength = 0;
            webRequest.Proxy = HttpWebRequest.DefaultWebProxy;

            try
            {
                var webResponse = (HttpWebResponse)webRequest.GetResponse();
                var responseStream = webResponse.GetResponseStream();
                responseStream.CopyTo(response);
                responseStream.Close();
            }
            catch (Exception e)
            {
                LogFactory.CreateLog().LogError(ErrorMessages.RestServiceFaultResponseError, e, url);

                throw e;
            }

            return response;
        }

        /// <summary>
        /// Send a get verb to a service asynchronosly
        /// Is for download content from services... ej: Images, Docs, etc.
        /// </summary>
        /// <param name="url">The service URL</param>
        /// <returns>Stream with the response</returns>
        public async Task<MemoryStream> SendGetRequestAsync(string url)
        {
            var response = new MemoryStream();

            var webRequest = (HttpWebRequest)InternalCreateWebRequest(url);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.KeepAlive = true;
            webRequest.ContentLength = 0;
            webRequest.Proxy = HttpWebRequest.DefaultWebProxy;

            var webResponse = await webRequest.GetResponseAsync();
            var responseStream = webResponse.GetResponseStream();
            await responseStream.CopyToAsync(response);
            responseStream.Close();

            return response;
        }
    }
}
