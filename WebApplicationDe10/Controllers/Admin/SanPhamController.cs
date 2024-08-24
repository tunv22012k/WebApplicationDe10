using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;
using WebApplicationDe10.Services.Admin;

namespace WebApplicationDe10.Controllers.Admin
{
    [Authorize(Roles = Constants.RoleAdmin)]
    public class SanPhamController: Controller
    {
        private readonly SanPhamService sanPhamService;
        public SanPhamController()
        {
            sanPhamService = new SanPhamService();
        }

        public ActionResult Index(SanPham sanPham)
        {
            ViewBag.ActivePage = "listProduct";
            ViewBag.listDanhMuc = sanPhamService.GetAllDanhMuc();
            ViewBag.listThuongHieu = sanPhamService.GetAllThuongHieu();
            ViewBag.listSanPham = sanPhamService.GetAllSanPham(sanPham);
            ViewBag.paramsGet = sanPham;
            return View("~/Views/Admin/SanPham/Index.cshtml");
        }

        public ActionResult Create()
        {

            ViewBag.listDanhMuc = sanPhamService.GetAllDanhMuc();
            ViewBag.listThuongHieu = sanPhamService.GetAllThuongHieu();
            ViewBag.ActivePage = "CreateProduct";
            return View("~/Views/Admin/SanPham/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SanPhamViewModel SanPhamViewModel)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;

                // Kiểm tra nếu có hình ảnh được tải lên
                if (SanPhamViewModel.URLHinhAnh != null && SanPhamViewModel.URLHinhAnh.ContentLength > 0)
                {
                    fileName = Path.GetFileName(SanPhamViewModel.URLHinhAnh.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/ImagesUpload/"), fileName);

                    // Lưu hình ảnh vào thư mục Images
                    SanPhamViewModel.URLHinhAnh.SaveAs(path);
                }

                SanPham sanPham = new SanPham
                {
                    MaSanPham           = SanPhamViewModel.MaSanPham,
                    TenSanPham          = SanPhamViewModel.TenSanPham,
                    MaDanhMuc           = SanPhamViewModel.MaDanhMuc,
                    MaThuongHieu        = SanPhamViewModel.MaThuongHieu,
                    GiaSanPham          = SanPhamViewModel.GiaSanPham,
                    MoTa                = SanPhamViewModel.MoTa,
                    URLHinhAnh          = fileName != null ? "/Content/ImagesUpload/" + fileName : null,
                    SoLuongTonKho       = SanPhamViewModel.SoLuongTonKho,
                    ThoiGianBaoHanh     = SanPhamViewModel.ThoiGianBaoHanh,
                    NgayRaMat           = SanPhamViewModel.NgayRaMat,
                    ThongSoKyThuatSPs   = new List<ThongSoKyThuatSP>()
                };

                if (SanPhamViewModel.ThongSoKyThuatSP != null)
                {
                    // Thêm thông số kỹ thuật
                    foreach (var thongSo in SanPhamViewModel.ThongSoKyThuatSP)
                    {
                        sanPham.ThongSoKyThuatSPs.Add(new ThongSoKyThuatSP
                        {
                            TenThongSo      = thongSo.TenThongSo,
                            GiaTriThongSo   = thongSo.GiaTriThongSo
                        });
                    }
                }

                bool success = sanPhamService.CreateSanPham(sanPham);
                if (success)
                {
                    return RedirectToAction("Index", "SanPham", new { area = "Admin" });
                }
            }
            ViewBag.listDanhMuc = sanPhamService.GetAllDanhMuc();
            ViewBag.listThuongHieu = sanPhamService.GetAllThuongHieu();
            return View(SanPhamViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(SanPhamViewModel SanPhamViewModel)
        {
            try
            {
                string fileName = null;

                // Kiểm tra nếu có hình ảnh được tải lên
                if (SanPhamViewModel.URLHinhAnh != null && SanPhamViewModel.URLHinhAnh.ContentLength > 0)
                {
                    fileName = Path.GetFileName(SanPhamViewModel.URLHinhAnh.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/ImagesUpload/"), fileName);

                    // Lưu hình ảnh vào thư mục Images
                    SanPhamViewModel.URLHinhAnh.SaveAs(path);
                } else
                {
                    fileName = SanPhamViewModel.URLHinhAnhCu;
                }

                SanPham sanPham = new SanPham
                {
                    MaSanPham           = SanPhamViewModel.MaSanPham,
                    TenSanPham          = SanPhamViewModel.TenSanPham,
                    MaDanhMuc           = SanPhamViewModel.MaDanhMuc,
                    MaThuongHieu        = SanPhamViewModel.MaThuongHieu,
                    GiaSanPham          = SanPhamViewModel.GiaSanPham,
                    MoTa                = SanPhamViewModel.MoTa,
                    URLHinhAnh          = fileName != null ? "/Content/ImagesUpload/" + fileName : null,
                    SoLuongTonKho       = SanPhamViewModel.SoLuongTonKho,
                    ThoiGianBaoHanh     = SanPhamViewModel.ThoiGianBaoHanh,
                    NgayRaMat           = SanPhamViewModel.NgayRaMat,
                    ThongSoKyThuatSPs   = new List<ThongSoKyThuatSP>()
                };

                if (SanPhamViewModel.ThongSoKyThuatSP != null)
                {
                    // Thêm thông số kỹ thuật
                    foreach (var thongSo in SanPhamViewModel.ThongSoKyThuatSP)
                    {
                        sanPham.ThongSoKyThuatSPs.Add(new ThongSoKyThuatSP
                        {
                            TenThongSo      = thongSo.TenThongSo,
                            GiaTriThongSo   = thongSo.GiaTriThongSo
                        });
                    }
                }

                bool success = sanPhamService.EditSanPham(sanPham);

                return Json(new { success = success });
            }
            catch (Exception ex)
            {
                throw new Exception("Update err: " + ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Delete(string MaSanPham)
        {
            try
            {
                bool isSuccess = sanPhamService.DeleteSanPham(MaSanPham);
                return Json(new { success = isSuccess });
            }
            catch (Exception ex)
            {
                throw new Exception("Delete err: " + ex.Message);
            }
        }
    }
}