using System;
using System.Collections.Generic;

namespace DokuFlex.Windows.Common.Services.Data
{
    public class SearchUserResponse 
        : RestResponse
    {
        public List<SearchUserResult> users { get; set; }

        public SearchUserResponse()
        {
            //Usuarios
            users = new List<SearchUserResult>();
        }
    }
}
