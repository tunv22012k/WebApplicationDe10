using System.Web.Mvc;
using WebApplicationDe10.Services;

namespace WebApplicationDe10.Controllers
{
    public class GioHangController : Controller
    {
        private readonly GioHangService gioHangService;
        public GioHangController()
        {
            gioHangService = new GioHangService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}