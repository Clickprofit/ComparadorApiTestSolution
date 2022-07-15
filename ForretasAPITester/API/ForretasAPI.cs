using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ForretasAPITester.API
{
    public class ForretasAPI
    {
        private readonly IConfiguration Configuration;
        private readonly string APIBaseUrl;

        public ForretasAPI(IConfiguration configuration)
        {
            Configuration = configuration;
            APIBaseUrl = Configuration.GetSection("API")?.GetSection("BaseURL").Value;
        }

        public bool Authenticate(string userName, string password, out string jwt)
        {
            jwt = string.Empty;

            var user = new { user = userName, password = password };

            string stringData = System.Text.Json.JsonSerializer.Serialize(user, new JsonSerializerOptions { WriteIndented = true });
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            // try catch in case api server is down
            try
            {
                string endpointUrl = $"{APIBaseUrl}/api/authentication/authenticate";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.PostAsync(endpointUrl, contentData).Result;
                string stringJWT = response.Content.ReadAsStringAsync().Result;
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    return false;
                }

                var jsonRoot = JObject.Parse(stringJWT);
                jwt = jsonRoot["token"].ToString();

                return true;

            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public string GenericSend(string endpoint, object model, HttpMethod method, string jwt)
        {
            try
            {
                HttpClient client = new HttpClient();
                ConfigureHttpClient(client, jwt);

                HttpResponseMessage response;

                string stringData = System.Text.Json.JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true });

                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

                //if (method == HttpMethod.Post)
                //{
                //    response = client.PostAsync(endpoint, contentData).Result;
                //}
                //else
                //{
                //    response = client.PutAsync(endpoint, contentData).Result;
                //}


                response = client.PostAsync(endpoint, contentData).Result;

                string jsonResponse = response.Content.ReadAsStringAsync().Result;

                if (string.IsNullOrEmpty(jsonResponse))
                    jsonResponse = response.RequestMessage.ToString();


                /**/
                var _uri = $"{APIBaseUrl}{endpoint}";



                var httpRequest = (HttpWebRequest)WebRequest.Create(_uri);
                httpRequest.Method = "POST";

                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = $"Bearer {jwt}";
                httpRequest.ContentType = "application/json";


                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(stringData);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                





                //var request = WebRequest.Create(_uri);

                //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                //request.ContentType = "application/json";
                //request.Method = "POST";
                //request.Headers.Add("Authorization", "Bearer " + jwt);

                ////var type = request.GetType();
                ////var currentMethod = type.GetProperty("CurrentMethod", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(request);

                ////var methodType = currentMethod.GetType();
                ////methodType.GetField("ContentBodyNotAllowed", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(currentMethod, false);

                //using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                //{
                //    streamWriter.Write(JsonConvert.SerializeObject(stringData));
                //}

                //var response2 = request.GetResponse();
                //var responseStream = response2.GetResponseStream();
                //if (responseStream != null)
                //{
                //    var myStreamReader = new StreamReader(responseStream, Encoding.Default);
                //    var resultEntity = myStreamReader.ReadToEnd();
                //    myStreamReader.ReadToEnd();
                //}
                //responseStream.Close();
                //response2.Close();















                client = new HttpClient();


                var httpWebRequest = (HttpWebRequest)WebRequest.Create(_uri);
                httpWebRequest.PreAuthenticate = true;
                httpWebRequest.ContentType = "application/json";
                //httpWebRequest.Method = "POST";
                httpWebRequest.Headers["Authorization"] = "Bearer " + jwt;


                httpWebRequest.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.5060.66 Safari/537.36 Edg/103.0.1264.44");

                httpWebRequest.Headers["Accept"] = "/";
                httpWebRequest.Headers["Accept-Encoding"] = "gzip, deflate, br";
                httpWebRequest.Headers["Accept-Language"] = "en,pt-PT;q=0.9,pt;q=0.8";
                httpWebRequest.Headers["Connection"] = "keep-alive";

                //httpWebRequest.Headers["sec-ch-ua-platfor"] = "Windows";
                //httpWebRequest.Headers["Sec-Fetch-Mode"] = "cors";

                //httpWebRequest.UseDefaultCredentials = true;
                //httpWebRequest.Credentials = CredentialCache.DefaultCredentials;

                httpWebRequest.Headers["upgrade-insecure-requests"] = "1";






                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(stringData);
                }

                var httpResponse3 = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse3.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();

                    jsonResponse = result;
                }






                return jsonResponse;
            }
            catch (Exception ex) { return ex.Message; }
        }

        private void ConfigureHttpClient(HttpClient client, string jwt)
        {
            client.BaseAddress = new Uri(APIBaseUrl);

            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        }
    }
}
