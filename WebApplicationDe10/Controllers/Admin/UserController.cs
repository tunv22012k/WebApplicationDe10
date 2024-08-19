using System.Web.Mvc;
using WebApplicationDe10.Models;
using WebApplicationDe10.Services.Admin;

namespace WebApplicationDe10.Controllers.Admin
{
    public class UserController : Controller
    {
        private readonly UserService userService;

        public UserController()
        {
            userService = new UserService();
        }

        public ActionResult Create()
        {
            ViewBag.ActivePage = "CreateUser";
            return View("~/Views/Admin/User/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuanTriVien user)
        {
            if (ModelState.IsValid)
            {
                bool success = userService.CreateUSer(user);
                if (success)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }
            return View(user);
        }
    }
}