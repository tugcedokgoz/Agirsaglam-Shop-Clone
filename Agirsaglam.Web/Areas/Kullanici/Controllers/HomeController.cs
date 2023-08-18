using Microsoft.AspNetCore.Mvc;

namespace Agirsaglam.Web.Areas.Kullanici.Controllers
{
    [Area("Kullanici")]
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
