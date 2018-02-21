//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
//
// Copyright (c) Paina Solutions. All right reserved.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
//
//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=

namespace DokuFlex.Windows.Common.Services.Data
{
    public class RecentFile : IDocument
    {
        public string id { get; set; }

        public string title { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public double version { get; set; }

        public int size { get; set; }

        public long modifiedTime { get; set; }

        public string type { get; set; }

        public string mimeType { get; set; }
    }
}
