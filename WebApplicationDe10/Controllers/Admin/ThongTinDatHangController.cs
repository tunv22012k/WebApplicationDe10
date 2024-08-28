using System.Web.Mvc;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;
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

        public ActionResult Index(DonHang donHang)
        {
            ViewBag.ActivePage = "InfoCart";
            ViewBag.listThongTinDatHang = thongTinDatHangService.GetAllDonHang(donHang);
            // ViewBag.paramsGet = danhMucSanPham;
            return View("~/Views/Admin/ThongTinDatHang/Index.cshtml");
        }
    }
}