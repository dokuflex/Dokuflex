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

namespace DokuFlex.Windows.Common.Extensions
{
    using System;
    using System.IO;
    using System.Text;
    using DokuFlex.Windows.Common.Log;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for <see cref="System.IO.FileInfo"/>.
    /// </summary>
    public static class FileInfoExtensions
    {
        /// <summary>
        /// Template for a file item in multipart/form-data format.
        /// </summary>
        public const string HeaderTemplate = "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n";

        /// <summary>
        /// Writes a file to a stream in multipart/form-data format.
        /// </summary>
        /// <param name="file">The file that should be written.</param>
        /// <param name="stream">The stream to which the file should be written.</param>
        /// <param name="mimeBoundary">The MIME multipart form boundary string.</param>
        /// <param name="mimeType">The MIME type of the file.</param>
        /// <param name="formKey">The name of the form parameter corresponding to the file upload.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if any parameter is <see langword="null" />.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="mimeBoundary" />, <paramref name="mimeType" />,
        /// or <paramref name="formKey" /> is empty.
        /// </exception>
        /// <exception cref="System.IO.FileNotFoundException">
        /// Thrown if <paramref name="file" /> does not exist.
        /// </exception>
        public static void WriteMultipartFormData(this FileInfo file, Stream stream, string mimeBoundary, string formKey)
        {
            if (file == null)
            {
                Exception ex = new ArgumentNullException(string.Format(ErrorMessages.ParamNotNullOrEmptyError, "file"));
                //LogFactory.CreateLog().LogError(ex);
                throw ex;                
            }

            if (!file.Exists)
            {
                Exception ex = new FileNotFoundException(ErrorMessages.FileNotFoundError, file.FullName);
                //LogFactory.CreateLog().LogError(ex);
                throw ex;
            }

            if (stream == null)
            {
                Exception ex = new ArgumentNullException(string.Format(ErrorMessages.ParamNotNullOrEmptyError, "stream"));
                //LogFactory.CreateLog().LogError(ex);
                throw ex;
            }

            if (string.IsNullOrWhiteSpace(mimeBoundary))
            {
                Exception ex = new ArgumentNullException(string.Format(ErrorMessages.ParamNotNullOrEmptyError, "mimeBoundary"));
                //LogFactory.CreateLog().LogError(ex);
                throw ex;
            }

            if (string.IsNullOrWhiteSpace(formKey))
            {
                Exception ex = new ArgumentNullException(string.Format(ErrorMessages.ParamNotNullOrEmptyError, "formKey"));
                //LogFactory.CreateLog().LogError(ex);
                throw ex;
            }

            var header = String.Format(HeaderTemplate, mimeBoundary, formKey, file.Name, file.GetMimeType());
            var headerbytes = Encoding.UTF8.GetBytes(header);

            stream.Write(headerbytes, 0, headerbytes.Length);

            using (FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
            {
                var buffer = new byte[1024];
                var bytesRead = 0;

                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    stream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }

            var newlineBytes = Encoding.UTF8.GetBytes("\r\n");
            stream.Write(newlineBytes, 0, newlineBytes.Length);
        }

        public static async Task WriteMultipartFormDataAsync(this FileInfo file, Stream stream, string mimeBoundary, string formKey)
        {
            if (file == null)
            {
                Exception ex = new ArgumentNullException(string.Format(ErrorMessages.ParamNotNullOrEmptyError, "file"));
                //LogFactory.CreateLog().LogError(ex);
                throw ex;
            }

            if (!file.Exists)
            {
                Exception ex = new FileNotFoundException(ErrorMessages.FileNotFoundError, file.FullName);
                //LogFactory.CreateLog().LogError(ex);
                throw ex;
            }

            if (stream == null)
            {
                Exception ex = new ArgumentNullException(string.Format(ErrorMessages.ParamNotNullOrEmptyError, "stream"));
                //LogFactory.CreateLog().LogError(ex);
                throw ex;
            }

            if (string.IsNullOrWhiteSpace(mimeBoundary))
            {
                Exception ex = new ArgumentNullException(string.Format(ErrorMessages.ParamNotNullOrEmptyError, "mimeBoundary"));
                //LogFactory.CreateLog().LogError(ex);
                throw ex;
            }

            if (string.IsNullOrWhiteSpace(formKey))
            {
                Exception ex = new ArgumentNullException(string.Format(ErrorMessages.ParamNotNullOrEmptyError, "formKey"));
                //LogFactory.CreateLog().LogError(ex);
                throw ex;
            }

            var header = String.Format(HeaderTemplate, mimeBoundary, formKey, file.Name, file.GetMimeType());
            var headerbytes = Encoding.UTF8.GetBytes(header);

            await stream.WriteAsync(headerbytes, 0, headerbytes.Length);

            using (FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
            {
                var buffer = new byte[1024];
                var bytesRead = 0;

                while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    await stream.WriteAsync(buffer, 0, bytesRead);
                }
                fileStream.Close();
            }

            var newlineBytes = Encoding.UTF8.GetBytes("\r\n");
            await stream.WriteAsync(newlineBytes, 0, newlineBytes.Length);
        }
    }
}
