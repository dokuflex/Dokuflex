//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=
//
// Copyright (c) Paina Solutions. All right reserved.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
//
//=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=

namespace DokuFlex.Windows.Common.Services.Data
{
    using System;
    using System.Collections.Generic;

    public class GetDocumentMetadataResponse
         : RestResponse
    {
        public long dateCreated { get; set; }

        public string docType { get; set; }

        public long dateModified { get; set; }

        public int version { get; set; }

        public List<DokuField> elements { get; set; }

        public GetDocumentMetadataResponse()
        {
            elements = new List<DokuField>();
        }
    }
}
