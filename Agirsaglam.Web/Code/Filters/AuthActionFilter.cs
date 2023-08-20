using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Agirsaglam.Web.Code.Filters
{
    public class AuthActionFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        public string Role;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var sessionRole = context.HttpContext.Session.GetString("role");
            var sessionUserName = context.HttpContext.Session.GetString("userName");

            if (!string.IsNullOrEmpty(Role))
            {
                bool isAuthorized = Role.Split(',').Contains(sessionRole);
                if (!isAuthorized)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            else if (string.IsNullOrEmpty(sessionUserName))
            {
                context.Result = new UnauthorizedResult();
            }
        }


    }
}
