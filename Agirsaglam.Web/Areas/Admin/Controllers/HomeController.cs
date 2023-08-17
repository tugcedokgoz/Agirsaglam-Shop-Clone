using Microsoft.AspNetCore.Mvc;

namespace Agirsaglam.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()=>View();
        public IActionResult Role()=>View();
        public IActionResult Product()=>View();
        public IActionResult User()=>View();
        public IActionResult Category()=>View();
        public IActionResult Comment()=>View();
        public IActionResult Order()=>View();
        public IActionResult Bill()=>View();
    }
}
