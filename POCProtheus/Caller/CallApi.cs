using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using POCProtheus.Dtos;
using POCProtheus.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace POCProtheus.Caller
{
    public class CallApi
    {

        TicketApiClient _api = new TicketApiClient();

        public async Task<string> Login(string user, string pwd)
        {
            var loginDto = new GetProtheus
            {
                Email = user,
                Senha = pwd
            };

            string json = JsonConvert.SerializeObject(loginDto, Formatting.Indented);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpClient client = _api.Initial();
            HttpResponseMessage userToken = client.PostAsync("/auth/login", byteContent).Result;

            string responseBody = await userToken.Content.ReadAsStringAsync();
            JObject jObject = JObject.Parse(responseBody);
            JObject data = (JObject)jObject["data"];
            string access_token = (string)data.SelectToken("access_token");

            if(!String.IsNullOrEmpty(access_token))
            {
                return access_token;
            }
            else
            {
                return null;
            }

        }

        public string CAllApiMethod(GenericAddDto dto, string url, string access_token)
        {
            LogControl logControl = new LogControl();

            string json2 = JsonConvert.SerializeObject(dto, Formatting.Indented);
            var buffer2 = System.Text.Encoding.UTF8.GetBytes(json2);
            var byteContent2 = new ByteArrayContent(buffer2);
            byteContent2.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpClient client = _api.Initial();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

            HttpResponseMessage res = client.PostAsync(url, byteContent2).Result;

            string resposta = "FALHA";

            if (res.IsSuccessStatusCode)
            {
                logControl.Write("Processamento de " + url + " Sucesso");
                resposta = "SUCESSO";
            }
            else
            {
                logControl.Write("Processamento de " + url + " Falha");
                resposta = "FALHA";
            }

            return resposta;
        }
    }
}