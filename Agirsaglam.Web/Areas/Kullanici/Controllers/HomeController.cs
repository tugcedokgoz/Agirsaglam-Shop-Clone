
using Agirsaglam.Web.Code.Filters;
using Agirsaglam.Web.Code.Rest;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Data;

namespace Agirsaglam.Web.Areas.Kullanici.Controllers
{
    [Area("Kullanici")]
    //[AuthActionFilter(Role = "Kullanici")]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Clothes(int id)
        {

            ProductRestClient client = new ProductRestClient();
            dynamic result = client.GetProduct(1);

            bool success = result.success;
            if (success)
                ViewBag.Clothes = result.data;
            return View();

        }
        public IActionResult AboutUs() => View();

        //[AuthActionFilter(Role = "Kullanici")]
        public IActionResult Accessory()
        {

            ProductRestClient client = new ProductRestClient();
            dynamic result = client.GetProduct(2);

            bool success = result.success;
            if (success)
                ViewBag.Accessory = result.data;
            return View();
        }
        public IActionResult Coffee(int id)
        {
            ProductRestClient client = new ProductRestClient();
            dynamic result = client.GetProduct(3);

            bool success = result.success;
            if (success)
                ViewBag.Coffee = result.data;
            return View();
        }
        public IActionResult PrivacyPolicy() => View();
        public IActionResult ReturnPolicy() => View();
        public IActionResult Terms() => View();
        public IActionResult UserGuide() => View();
        public IActionResult Blog() => View();
        public IActionResult ProductDetail(int id)
        {

            ProductRestClient client = new ProductRestClient();
            dynamic result = client.GetProductDetails(id);

            bool success = result.success;
            if (success)
            {
                ViewBag.ProductDetails = result.product;
                ViewBag.ProductProperties = result.properties;
            }
            return View();
        }

        public IActionResult DiscountedProduct()
        {
            ProductRestClient client = new ProductRestClient();
            dynamic result = client.GetDiscountedProduct();

            bool success = result.success;
            if (success)
                ViewBag.DiscountedProduct = result.data;
            return View();
        }


        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Cart() => View();
        public IActionResult Register() => View();
    }
}
