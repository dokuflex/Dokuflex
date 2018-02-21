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

    public class ListCategoriesResponse
        : RestResponse
    {
        public List<Category> elements { get; set; }

        public ListCategoriesResponse()
        {
            elements = new List<Category>();
        }
    }
}
