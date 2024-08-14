using System.Web.Mvc;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Services;
using WebApplicationDe10.Services.Admin;

namespace WebApplicationDe10.Controllers.Admin
{
    [Authorize(Roles = Constants.RoleAdmin)]
    public class HomeController : Controller
    {
        private readonly HomeService homeService;

        public HomeController()
        {
            homeService = new HomeService();
        }

        public ActionResult Index()
        {
            ViewBag.ActivePage = "Index";
            ViewBag.listUser = homeService.GetAllUsers();
            return View("~/Views/Admin/Home/Index.cshtml");
        }
    }
}