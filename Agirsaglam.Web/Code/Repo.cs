//kullanamdım

namespace Agirsaglam.Web.Code
{
    public class Repo
    {
        public static class Session
        {

            public static string? UserName
            {
                get
                {
                    string userName = (new HttpContextAccessor()).HttpContext.Session.GetString("UserName");
                    return userName;
                }
                set
                {
                    (new HttpContextAccessor()).HttpContext.Session.SetString("UserName", value ?? "");
                }
            }
            public static string? Token
            {
                get
                {
                    string token = (new HttpContextAccessor()).HttpContext.Session.GetString("Token");
                    return token;
                }
                set
                {
                    (new HttpContextAccessor()).HttpContext.Session.SetString("Token", value ?? "");
                }
            }
            public static string? Role
            {
                get
                {
                    string role = (new HttpContextAccessor()).HttpContext.Session.GetString("Role");
                    return role;
                }
                set
                {
                    (new HttpContextAccessor()).HttpContext.Session.SetString("Role", value ?? "");
                }
            }
        }
    }
}
