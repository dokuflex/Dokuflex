﻿// =================================================================================================================
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
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using System.Web;

    /// <summary>
    /// Extension methods for generic dictionaries.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Template for a multipart/form-data item.
        /// </summary>
        public const string FormDataTemplate = "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}\r\n";

        /// <summary>
        /// Writes a dictionary to a stream as a multipart/form-data set.
        /// </summary>
        /// <param name="dictionary">The dictionary of form values to write to the stream.</param>
        /// <param name="stream">The stream to which the form data should be written.</param>
        /// <param name="mimeBoundary">The MIME multipart form boundary string.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown if <paramref name="stream" /> or <paramref name="mimeBoundary" /> is <see langword="null" />.
        /// </exception>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="mimeBoundary" /> is empty.
        /// </exception>
        /// <remarks>
        /// If <paramref name="dictionary" /> is <see langword="null" /> or empty,
        /// nothing wil be written to the stream.
        /// </remarks>
        public static void WriteMultipartFormData(this IDictionary<string, string> dictionary, Stream stream, string mimeBoundary)
        {
            if (dictionary == null || dictionary.Count == 0)
            {
                return;
            }

            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (mimeBoundary == null)
            {
                throw new ArgumentNullException("mimeBoundary");
            }

            if (mimeBoundary.Length == 0)
            {
                throw new ArgumentException("MIME boundary may not be empty.", "mimeBoundary");
            }

            foreach (string key in dictionary.Keys)
            {
                var item = String.Format(FormDataTemplate, mimeBoundary, key, dictionary[key]);
                var itemBytes = System.Text.Encoding.UTF8.GetBytes(item);
                stream.Write(itemBytes, 0, itemBytes.Length);
            }
        }

        public static async Task WriteMultipartFormDataAsync(this IDictionary<string, string> dictionary, Stream stream, string mimeBoundary)
        {
            if (dictionary.Count == 0)
            {
                throw new ArgumentNullException("the dictionary doesn't contain keys");
            }

            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            if (mimeBoundary == null)
            {
                throw new ArgumentNullException("mimeBoundary");
            }

            if (mimeBoundary.Length == 0)
            {
                throw new ArgumentException("MIME boundary may not be empty.", "mimeBoundary");
            }

            foreach (string key in dictionary.Keys)
            {
                var item = String.Format(FormDataTemplate, mimeBoundary, key, dictionary[key]);
                var itemBytes = System.Text.Encoding.UTF8.GetBytes(item);
                await stream.WriteAsync(itemBytes, 0, itemBytes.Length);
            }
        }
    }
}
