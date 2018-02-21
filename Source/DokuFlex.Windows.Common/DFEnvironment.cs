//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
//
// Copyright (c) Paina Solutions. All right reserved.
//
//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=

namespace DokuFlex.Windows.Common
{
    using System;

    public static class DFEnvironment
    {
        public static string GetSpecialFolder(SpecialFolder specialFolder)
        {
            var path = string.Format("{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DokuFlex");

            switch (specialFolder)
            {
                case SpecialFolder.Documents:
                    path = String.Format("{0}\\{1}", path, "Documentos");
                    break;

                case SpecialFolder.SpreadSheet:
                    path = String.Format("{0}\\{1}", path, "Hojas de cálculo");
                    break;

                case SpecialFolder.Presentations:
                    path = String.Format("{0}\\{1}", path, "Presentaciones");
                    break;

                case SpecialFolder.Images:
                    path = String.Format("{0}\\{1}", path, "Imágenes");
                    break;

                case SpecialFolder.Others:
                    path = String.Format("{0}\\{1}", path, "Otros");
                    break;

                case SpecialFolder.UploadDirectory:
                    path = String.Format("{0}\\{1}", Environment.GetEnvironmentVariable("TMP"), Constants.UploadDirectory);
                    break;

                case SpecialFolder.DownloadDirectory:
                    path = String.Format("{0}\\{1}", Environment.GetEnvironmentVariable("TMP"), Constants.DownloadDirectory);
                    break;

                default:
                    break;
            }

            return path;
        }

        public enum SpecialFolder
        { 
            Documents = 1,
            SpreadSheet = 2,
            Presentations = 3,
            Images = 4,
            Others = 5,
            DownloadDirectory,
            UploadDirectory
        }
    }
}
