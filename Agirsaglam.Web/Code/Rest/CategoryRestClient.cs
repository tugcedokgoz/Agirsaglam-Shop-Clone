using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;

namespace Agirsaglam.Web.Code.Rest
{
    public class CategoryRestClient
    {

        private string BASE_API_URI = "https://localhost:7119/api";

        public dynamic Login(string userName, string password)
        {
            RestClient client = new RestClient(BASE_API_URI, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions { PropertyNamingPolicy = null }));

            RestRequest req = new RestRequest("/Auth/Login", Method.Post);
            req.AddJsonBody(new
            {
                UserName = userName,
                Password = password
            });

            RestResponse resp = client.Post(req);
            string msg = resp.Content.ToString();
            dynamic result = JObject.Parse(msg);
            return result;
        }
    }
}
