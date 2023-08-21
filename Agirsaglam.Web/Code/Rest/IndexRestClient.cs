using Newtonsoft.Json.Linq;
using RestSharp;

namespace Agirsaglam.Web.Code.Rest
{
    public class IndexRestClient:BaseRestClient
    {
        //public dynamic GetClothes(int id)
        //{
        //    RestRequest req= new RestRequest($"/Home/Index/{id}",Method.Get);
        //    RestResponse res = client.Get(req);
        //    string msg = res.Content.ToString();

        //    dynamic result = JObject.Parse(msg);
        //    return result;
        //}
    }
}
