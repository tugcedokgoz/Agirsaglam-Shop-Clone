using RestSharp;
using RestSharp.Serializers.Json;
using System.Text.Json;

namespace Agirsaglam.Web.Code.Rest
{
    public class BaseRestClient
    {

        public static string BASE_URL = "https://localhost:7119/api";
        protected RestClient client;

        public BaseRestClient()
        {
            client = new RestClient(BASE_URL, configureSerialization: s => s.UseSystemTextJson(new JsonSerializerOptions { PropertyNamingPolicy = null }));
        }
    }
}
