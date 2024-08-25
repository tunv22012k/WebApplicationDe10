using System.Web.Mvc;
using WebApplicationDe10.Services;
using WebApplicationDe10.Services.Admin;

namespace WebApplicationDe10.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly ChiTietSanPhamService chiTietSanPhamService;

        public SanPhamController()
        {
            chiTietSanPhamService = new ChiTietSanPhamService();
        }

        [HttpGet]
        public ActionResult ChiTiet(int MaSanPham)
        {
            var list = chiTietSanPhamService.GetProductById(MaSanPham);
            ViewBag.Product = list.sanPham;
            ViewBag.ListThongSoKiThuat = list.thongSoKyThuatSP;
            return View();
        }
    }
}