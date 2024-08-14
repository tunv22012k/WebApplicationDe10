using System.Web.Mvc;
using WebApplicationDe10.Helpers;

namespace WebApplicationDe10.Controllers.Admin
{
    [Authorize(Roles = Constants.RoleAdmin)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ActivePage = "Index";
            return View("~/Views/Admin/Home/Index.cshtml");
        }
    }
}