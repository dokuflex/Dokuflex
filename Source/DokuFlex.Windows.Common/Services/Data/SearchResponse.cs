using System;
using System.Collections.Generic;

namespace DokuFlex.Windows.Common.Services.Data
{
    public class SearchResponse 
        : RestResponse
    {
        public List<SearchResult> documents { get; set; }

        public SearchResponse()
        {
            documents = new List<SearchResult>();
        }
    }
}
