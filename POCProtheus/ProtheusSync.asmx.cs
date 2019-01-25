using POCProtheus.Caller;
using POCProtheus.Dtos;
using POCProtheus.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;

namespace POCProtheus
{
    /// <summary>
    /// Summary description for ProtheusSync
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ProtheusSync : System.Web.Services.WebService
    {

        [WebMethod]
        //[ValidateInput(false)]
        public Task<string> RegisterProject(string xml)
        {
            IncomingWebRequestContext request = WebOperationContext.Current.IncomingRequest;
            LogControl logControl = new LogControl();
            CallApi api = new CallApi();
            GenericAddDto dto = new GenericAddDto
            {
                Xml = xml
            };

            string user = request.Headers["Usuario"];
            string senha = request.Headers["Senha"];

            string access_token = api.Login(user, senha).Result;

            if (String.IsNullOrEmpty(access_token))
            {
                logControl.Write("Usuário e senha não conferem");
                return api.CAllApiMethod(dto, "/project", access_token);
            }
            else
            {
                logControl.Write("Usuário logado");
                return api.CAllApiMethod(dto, "/project", access_token);
            }

        }
    }
}
