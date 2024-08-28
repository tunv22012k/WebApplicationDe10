using System.Web.Mvc;
using WebApplicationDe10.Services;
using WebApplicationDe10.Services.Admin;

namespace WebApplicationDe10.Controllers
{
    public class DanhMucSanPhamController : Controller
    {
        private readonly DanhMucSanPhamService danhMucSanPhamService;
        public DanhMucSanPhamController()
        {
            danhMucSanPhamService = new DanhMucSanPhamService();
        }

        [HttpGet]
        public ActionResult Index(int MaDanhMuc)
        {
            ViewBag.ActivePage = "DanhMuc";
            var list = danhMucSanPhamService.GetListSanPham(MaDanhMuc);
            ViewBag.ListSanPham = list.ListSanPham;
            ViewBag.DanhMucSanPham = list.DanhMucSanPham;
            return View();
        }
    }
}