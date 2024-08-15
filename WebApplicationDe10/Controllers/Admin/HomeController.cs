using System.Web.Mvc;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;
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

        public ActionResult Index(User user)
        {
            ViewBag.ActivePage = "Index";
            ViewBag.listUser = homeService.GetAllUsers(user);
            ViewBag.paramsGet = user;
            return View("~/Views/Admin/Home/Index.cshtml");
        }
    }
}