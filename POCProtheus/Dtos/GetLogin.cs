using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POCProtheus.Dtos
{
    public class GetLogin
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public DateTime expires_in { get; set; }
    }
}