using Agirsaglam.Web.Code;
using Agirsaglam.Web.Code.Rest;
using Agirsaglam.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Agirsaglam.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login() => View();



        public IActionResult GirisYap(LoginModel model)
        {

            CategoryRestClient client = new CategoryRestClient();
            dynamic result = client.Login(model.UserName, model.Password);

            bool success = result.success;
            Console.WriteLine(result);

            if (success)
            {

                HttpContext.Session.SetString("userName",model.UserName);
                HttpContext.Session.SetString("token", (string)result.data);
                HttpContext.Session.SetString("rol", (string)result.role);
                //Repo.Session.UserName = model.UserName;
                //Repo.Session.Token = (string)result.data;
                //Repo.Session.Role = (string)result.role;

                return RedirectToAction("Index", "Home");
            }
            else
            {

                ViewBag.LoginError = (string)result.message;
                return View("Login");
            }
        }
    }
}
