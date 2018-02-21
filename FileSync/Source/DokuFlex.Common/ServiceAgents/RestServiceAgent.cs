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

namespace DokuFlex.Common.ServiceAgents
{
    using System;
    using System.IO;
    using System.Configuration;
    using System.Collections.Generic;
    using System.Text;
    using System.Net;
    using System.Net.Security;
    using System.Threading.Tasks;

    public class RestServiceAgent
    {
        private NetworkCredential _credentials;

        private bool _useProxyServer;

        public RestServiceAgent()
        {
            _credentials = new NetworkCredential();

            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            _credentials.UserName = ConfigurationManager.GetValue(Resources.ProxyUserNameKey);
            _credentials.Password = ConfigurationManager.GetValue(Resources.ProxyPasswordKey);
            _credentials.Domain = ConfigurationManager.GetValue(Resources.ProxyDomainKey);

            if (!bool.TryParse(ConfigurationManager.GetValue(Resources.ProxyUseProxyKey), out _useProxyServer))
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
            }

            return webRequest;
        }

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

            //Write the request
            var stream = webRequest.GetRequestStream();
            stream.Write(requestBytes, 0, requestBytes.Length);
            stream.Close();

            //Get the response
            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            var responseReader = new StreamReader(webResponse.GetResponseStream());
            response = responseReader.ReadToEnd();
            responseReader.Close();

            return response;
        }

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

            //Write the request
            var stream = await webRequest.GetRequestStreamAsync();
            stream.Write(requestBytes, 0, requestBytes.Length);
            stream.Close();

            //Get the response
            var webResponse = await webRequest.GetResponseAsync();
            var responseReader = new StreamReader(webResponse.GetResponseStream());
            response = responseReader.ReadToEnd();
            responseReader.Close();

            return response;
        }

        public string SendPostRequest(string url, IDictionary<string, string> request,
            FileInfo fileToUpload, string fileFormKey)
        {
            var response = string.Empty;
            var boundary = CreateFormDataBoundary();

            var webRequest = (HttpWebRequest)InternalCreateWebRequest(url);
            webRequest.Method = "POST";
            webRequest.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            webRequest.KeepAlive = true;

            //Write the request
            var stream = webRequest.GetRequestStream();
            request.WriteMultipartFormData(stream, boundary);
            fileToUpload.WriteMultipartFormData(stream, boundary, fileFormKey);
            var endBytes = Encoding.UTF8.GetBytes("--" + boundary + "--");
            stream.Write(endBytes, 0, endBytes.Length);
            stream.Close();

            //Get the response
            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            var responseReader = new StreamReader(webResponse.GetResponseStream());
            response = responseReader.ReadToEnd();
            responseReader.Close();

            return response;
        }

        public async Task<string> SendPostRequestAsync(string url, IDictionary<string, string> request,
            FileInfo fileToUpload, string fileFormKey)
        {
            var response = string.Empty;
            var boundary = CreateFormDataBoundary();

            var webRequest = (HttpWebRequest)InternalCreateWebRequest(url);
            webRequest.Method = "POST";
            webRequest.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
            webRequest.KeepAlive = true;

            //Write the request
            var stream = webRequest.GetRequestStream();
            request.WriteMultipartFormData(stream, boundary);
            fileToUpload.WriteMultipartFormData(stream, boundary, fileFormKey);
            var endBytes = Encoding.UTF8.GetBytes("--" + boundary + "--");
            stream.Write(endBytes, 0, endBytes.Length);
            stream.Close();

            //Get the response
            var webResponse = await webRequest.GetResponseAsync();
            var responseReader = new StreamReader(webResponse.GetResponseStream());
            response = responseReader.ReadToEnd();
            responseReader.Close();

            return response;
        }

        public MemoryStream SendGetRequest(string url)
        {
            var response = new MemoryStream();

            var webRequest = (HttpWebRequest)InternalCreateWebRequest(url);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.KeepAlive = true;
            webRequest.ContentLength = 0;
            webRequest.Proxy = HttpWebRequest.DefaultWebProxy;

            //Get the response
            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            var responseStream = webResponse.GetResponseStream();
            responseStream.CopyTo(response);
            responseStream.Close();

            return response;
        }

        public async Task<MemoryStream> SendGetRequestAsync(string url)
        {
            var response = new MemoryStream();

            var webRequest = (HttpWebRequest)InternalCreateWebRequest(url);
            webRequest.Method = "GET";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.KeepAlive = true;
            webRequest.ContentLength = 0;
            webRequest.Proxy = HttpWebRequest.DefaultWebProxy;

            //Get the response
            var webResponse = await webRequest.GetResponseAsync();
            var responseStream = webResponse.GetResponseStream();
            responseStream.CopyTo(response);
            responseStream.Close();

            return response;
        }
    }
}