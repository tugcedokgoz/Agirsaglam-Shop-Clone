using Agirsaglam.Web.Code;
using Agirsaglam.Web.Code.Rest;
using Agirsaglam.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace Agirsaglam.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login() => View();

        //oturum verisini burda döndürüyorum.
        [HttpGet]
        public IActionResult GetToken()
        {
            var token = HttpContext.Session.GetString("token");
            return Json(new { Token = token });
        }

        public IActionResult GirisYap(LoginModel model)
        {

            UserRestClient client = new UserRestClient();
            dynamic result = client.Login(model.UserName, model.Password);

            bool success = result.success;
            Console.WriteLine(result);

            if (success)
            {
                ViewBag.kullaniciAdi = model.UserName;// Kullanıcı adını ViewBag'e eklendi
              
                HttpContext.Session.SetString("UserName", model.UserName);
                HttpContext.Session.SetString("token", (string)result.data);
                HttpContext.Session.SetString("role", (string)result.role);
                if (result.role == "Admin")
                {
                    return RedirectToAction("Home", "Admin");
                }
                else
                {
                    return RedirectToAction("Home", "Kullanici");
                }
                  
            }
            else
            {

                ViewBag.LoginError = (string)result.message;
                return View("Login");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Login", "Account"); 
        }

    }
}
