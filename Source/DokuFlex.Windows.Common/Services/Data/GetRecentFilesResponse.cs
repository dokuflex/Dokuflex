//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
//
// Copyright (c) Paina Solutions. All right reserved.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
//
//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=

namespace DokuFlex.Windows.Common.Services.Data
{
    using System.Collections.Generic;

    public class GetRecentFilesResponse
        : RestResponse
    {
        public List<RecentFile> elements { get; set; }

        public int total { get; set; }

        public GetRecentFilesResponse()
        {
            elements = new List<RecentFile>();
        }
    }
}
