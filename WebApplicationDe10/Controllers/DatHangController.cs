using System;
using System.Web.Mvc;
using WebApplicationDe10.Models;
using WebApplicationDe10.Services;

namespace WebApplicationDe10.Controllers
{
    public class DatHangController : Controller
    {
        private readonly DatHangService datHangService;
        public DatHangController()
        {
            datHangService = new DatHangService();
        }

        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(DatHangViewModel datHangViewModel)
        {
            try
            {
                bool isSuccess = datHangService.Create(datHangViewModel);
                return Json(new { success = isSuccess });
            }
            catch (Exception ex)
            {
                throw new Exception("Create err: " + ex.Message);
            }
        }
    }
}