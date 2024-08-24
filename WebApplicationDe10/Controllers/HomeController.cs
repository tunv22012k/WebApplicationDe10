using System.Web.Mvc;
using WebApplicationDe10.Services;

namespace WebApplicationDe10.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService userService;

        public HomeController()
        {
            userService = new UserService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.ActivePage = "IndexUser";
            var list = userService.GetListDanhMucAndSanPham();
            ViewBag.ProductNew = list.ListSanPhamNew;
            ViewBag.ListDanhMuc = list.ListDanhMucSanPham;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}