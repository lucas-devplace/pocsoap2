using POCProtheus.Auth;
using POCProtheus.Caller;
using POCProtheus.Dtos;
using POCProtheus.Helper;
using System;
using System.Web.Services;
using System.Web.Services.Protocols;

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

        public Authorization Auth;

        [WebMethod]
        [SoapHeader("Auth", Required = true)]
        public string RegisterProject(string xml)
        {
            
            LogControl logControl = new LogControl();
            CallApi api = new CallApi();
            GenericAddDto dto = new GenericAddDto
            {
                Xml = xml
            };

            if (Auth != null)
            {
                if(Auth.IsValid())
                {
                    string access_token = api.Login(Auth.Usuario, Auth.Senha).Result;

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
                else
                {
                    logControl.Write("Usuário e senha informados na requisição não conferem");
                    return "FALHA";
                }
            }
            else
            {
                logControl.Write("Não foram informados usuário e senha no cabeçalho da requisição");
                return "FALHA";
            }
          
       

        }
    }
}
