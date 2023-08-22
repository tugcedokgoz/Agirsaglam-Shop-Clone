﻿using Newtonsoft.Json.Linq;
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
            dynamic productResult = JObject.Parse(msg);

            RestRequest propertyReq = new RestRequest($"/ProductProperty/GetProductPropertiesAndGroups/{id}", Method.Get);
            RestResponse propertyRes = client.Get(propertyReq);
            string propertyMsg = propertyRes.Content.ToString();
            dynamic propertyResult = JObject.Parse(propertyMsg);

            return new
            {
                success = true,
                product = productResult.data,
                properties = propertyResult.data
            };
        }

        public dynamic GetDiscountedProduct()
        {
            RestRequest req = new RestRequest($"/Product/GetDiscountedProduct", Method.Get);
            RestResponse res = client.Get(req);
            string msg = res.Content.ToString();

            dynamic result = JObject.Parse(msg);
            return result;
        }
    }
}
