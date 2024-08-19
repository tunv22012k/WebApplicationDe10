using System.Web.Security;
using System.Web;
using WebApplicationDe10.Models;
using System;

namespace WebApplicationDe10.Helpers
{
    public static class InfoUserLogin
    {
        public static QuanTriVien GetLoggedInUserInfo()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string[] userData = authTicket.UserData.Split('|');

                var userInfo = new QuanTriVien
                {
                    MaQuanTriVien   = int.Parse(userData[1]),
                    TenQuanTriVien  = userData[2],
                    Email           = userData[3]
                };

                return userInfo;
            }
            return null;
        }
    }
}