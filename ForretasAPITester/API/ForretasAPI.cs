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

        public string GenericPost(string endpoint, object model, string jwt)
        {
            try
            {
                HttpClient client = new HttpClient();
                ConfigureHttpClient(client, jwt);

                HttpResponseMessage response;

                string stringData = System.Text.Json.JsonSerializer.Serialize(model, new JsonSerializerOptions { WriteIndented = true });

                var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");


                response = client.PostAsync(endpoint, contentData).Result;

                return response.Content.ReadAsStringAsync().Result;
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
