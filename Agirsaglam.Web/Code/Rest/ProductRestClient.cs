using Newtonsoft.Json.Linq;
using RestSharp;

namespace Agirsaglam.Web.Code.Rest
{
    public class ProductRestClient:BaseRestClient
    {
        //public dynamic GetProduct()
        //{
        //    RestRequest req = new RestRequest($"/Product/GetProducts", Method.Get);
        //    RestResponse res = client.Get(req);
        //    string msg = res.Content.ToString();

        //    dynamic result = JObject.Parse(msg);
        //    return result;
        //}

        public dynamic GetProduct(int id)
        {
            RestRequest req = new RestRequest($"/Product/GetProductsByParentCategoryId/{id}", Method.Get);
            RestResponse res = client.Get(req);
            string msg = res.Content.ToString();

            dynamic result = JObject.Parse(msg);
            return result;
        }
        public dynamic GetProductDetails(int id)
        {
            RestRequest req = new RestRequest($"/Product/ProductId/{id}", Method.Get);
            RestResponse res = client.Get(req);
            string msg = res.Content.ToString();

            dynamic result = JObject.Parse(msg);
            return result;
        }
    }
}
