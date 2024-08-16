using System.Web.Security;
using System.Web;
using WebApplicationDe10.Models;
using System;

namespace WebApplicationDe10.Helpers
{
    public static class InfoUserLogin
    {
        public static User GetLoggedInUserInfo()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string[] userData = authTicket.UserData.Split('|');

                var userInfo = new User
                {
                    userId = int.Parse(userData[1]),
                    username = userData[2],
                    email = userData[3],
                    role = userData[0]
                };

                return userInfo;
            }
            return null;
        }
    }
}