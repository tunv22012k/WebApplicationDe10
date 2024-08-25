using System.Web.Mvc;
using WebApplicationDe10.Services;

namespace WebApplicationDe10.Controllers
{
    public class GioiThieuController : Controller
    {
        public GioiThieuController()
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.ActivePage = "GioiThieu";
            return View();
        }
    }
}