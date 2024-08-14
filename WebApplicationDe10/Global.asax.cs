using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace WebApplicationDe10
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var formsIdentity = (FormsIdentity)HttpContext.Current.User.Identity;
                    var ticket = formsIdentity.Ticket;

                    // Đảm bảo rằng vai trò được khởi tạo đúng cách từ cookie
                    string[] roles = ticket.UserData.Split(new char[] { ',' });
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(formsIdentity, roles);
                }
            }
        }
    }
}
