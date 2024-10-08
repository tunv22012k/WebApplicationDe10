﻿using System;
using System.Web.Mvc;
using WebApplicationDe10.Helpers;
using WebApplicationDe10.Models;
using WebApplicationDe10.Services.Admin;

namespace WebApplicationDe10.Controllers.Admin
{
    [Authorize(Roles = Constants.RoleAdmin)]
    public class DanhMucController: Controller
    {
        private readonly DanhMucService danhMucService;

        public DanhMucController()
        {
            danhMucService = new DanhMucService();
        }

        public ActionResult Index(DanhMucSanPham danhMucSanPham)
        {
            ViewBag.ActivePage = "ListCategory";
            ViewBag.listDanhMuc = danhMucService.GetAllDanhMuc(danhMucSanPham);
            ViewBag.paramsGet = danhMucSanPham;
            return View("~/Views/Admin/DanhMuc/Index.cshtml");
        }

        public ActionResult Create()
        {
            ViewBag.ActivePage = "CreateCategory";
            return View("~/Views/Admin/DanhMuc/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DanhMucSanPham danhMucSanPham)
        {
            if (ModelState.IsValid)
            {
                bool success = danhMucService.CreateDanhMuc(danhMucSanPham);
                if (success)
                {
                    return RedirectToAction("Index", "DanhMuc", new { area = "Admin" });
                }
            }
            return View(danhMucSanPham);
        }

        [HttpPost]
        public JsonResult Delete(string MaDanhMuc)
        {
            try
            {
                bool isSuccess = danhMucService.DeleteDanhMuc(MaDanhMuc);
                return Json(new { success = isSuccess });
            }
            catch (Exception ex)
            {
                throw new Exception("Delete err: " + ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Edit(DanhMucSanPham danhMucSanPham)
        {
            try
            {
                bool isSuccess = danhMucService.EditDanhMuc(danhMucSanPham);
                return Json(new { success = isSuccess });
            }
            catch (Exception ex)
            {
                throw new Exception("Update err: " + ex.Message);
            }
        }
    }
}