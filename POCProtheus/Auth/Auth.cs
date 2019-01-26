using System.Configuration;

namespace POCProtheus.Auth
{
    public class Authorization : System.Web.Services.Protocols.SoapHeader
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }

        public bool IsValid()
        {
            string user = ConfigurationManager.AppSettings["user"].ToString();
            string password = ConfigurationManager.AppSettings["password"].ToString();

            if((Usuario == user) && (Senha == password))
            {
                return true;
            }

            return false;
        }
    }
}