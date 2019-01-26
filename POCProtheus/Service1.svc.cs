using POCProtheus.Caller;
using POCProtheus.Dtos;
using POCProtheus.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace POCProtheus
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public object JsonConvert { get; private set; }

        public string RegisterProject(string xml)
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
            
            if(String.IsNullOrEmpty(access_token))
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
