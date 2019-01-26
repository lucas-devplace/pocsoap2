using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace POCProtheus.Helper
{
    public class TicketApiClient
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["api_url"].ToString());
            return Client;
        }
    }
}