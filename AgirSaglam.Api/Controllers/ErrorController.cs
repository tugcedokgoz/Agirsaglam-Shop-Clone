using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgirSaglam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        /*
              *projemizi yayınladık ve kullanıcılar projemizi kullanmaya başladı
              *bu sırada webapi nin herhangi bir yerinde bir exception ortaya çıktı
              *burası çalışacak

              */
        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError()
        {
            return Problem();
        }

        /*
        * Yazılım geliştirme aşamasında ise, debug modunda yani biz yazılımcılar hatayla ilgili daha fazla bilgi görmek istediğimizde
        * burası çalışacak
        */
        [Route("/error-development")] //sonra program.cs
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            return Problem(detail: exceptionHandlerFeature.Error.StackTrace, title: exceptionHandlerFeature.Error.Message);
        }

    }
}
