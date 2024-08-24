using System;
using System.Web.Mvc;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;
using WebApplicationDe10.Services.Admin;

namespace WebApplicationDe10.Controllers.Admin
{
    [Authorize(Roles = Constants.RoleAdmin)]
    public class ThuongHieuController : Controller
    {
        private readonly ThuongHieuService thuongHieuService;

        public ThuongHieuController()
        {
            thuongHieuService = new ThuongHieuService();
        }

        public ActionResult Index(ThuongHieu thuongHieu)
        {
            ViewBag.ActivePage = "ListThuongHieu";
            ViewBag.listThuongHieu = thuongHieuService.GetAllDanhMuc(thuongHieu);
            ViewBag.paramsGet = thuongHieu;
            return View("~/Views/Admin/ThuongHieu/Index.cshtml");
        }

        public ActionResult Create()
        {
            ViewBag.ActivePage = "CreateThuongHieu";
            return View("~/Views/Admin/ThuongHieu/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ThuongHieu thuongHieu)
        {
            if (ModelState.IsValid)
            {
                bool success = thuongHieuService.CreateThuongHieu(thuongHieu);
                if (success)
                {
                    return RedirectToAction("Index", "ThuongHieu", new { area = "Admin" });
                }
            }
            return View(thuongHieu);
        }

        [HttpPost]
        public JsonResult Delete(string MaThuongHieu)
        {
            try
            {
                bool isSuccess = thuongHieuService.DeleteThuongHieu(MaThuongHieu);
                return Json(new { success = isSuccess });
            }
            catch (Exception ex)
            {
                throw new Exception("Delete err: " + ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Edit(ThuongHieu thuongHieu)
        {
            try
            {
                bool isSuccess = thuongHieuService.EditThuongHieu(thuongHieu);
                return Json(new { success = isSuccess });
            }
            catch (Exception ex)
            {
                throw new Exception("Update err: " + ex.Message);
            }
        }
    }
}