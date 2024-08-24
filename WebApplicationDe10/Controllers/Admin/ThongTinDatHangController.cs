using System.Web.Mvc;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Services.Admin;

namespace WebApplicationDe10.Controllers.Admin
{
    [Authorize(Roles = Constants.RoleAdmin)]
    public class ThongTinDatHangController: Controller
    {
        private readonly ThongTinDatHangService thongTinDatHangService;
        public ThongTinDatHangController()
        {
            thongTinDatHangService = new ThongTinDatHangService();
        }

        public ActionResult Index()
        {
            ViewBag.ActivePage = "InfoCart";
            // ViewBag.listDanhMuc = danhMucService.GetAllThongTinDatHang(danhMucSanPham);
            // ViewBag.paramsGet = danhMucSanPham;
            return View("~/Views/Admin/ThongTinDatHang/Index.cshtml");
        }
    }
}