using System.Web;
using System;
using System.Web.Mvc;
using System.Web.Security;
using WebApplicationDe10.Models;
using WebApplicationDe10.Services;

namespace WebApplicationDe10.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService accountService;

        public AccountController()
        {
            accountService = new AccountService();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            var userLogin = accountService.Authenticate(user.email, user.password);
            if (userLogin != null)
            {
                // Tạo ticket FormsAuthentication với vai trò người dùng
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,                                      // Version
                    userLogin.username,                     // User name
                    DateTime.Now,                           // Issue date
                    DateTime.Now.AddMinutes(30),            // Expiration
                    false,                                  // Persistent
                    userLogin.role                          // User's role
                );

                // Mã hóa ticket
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                // Tạo cookie
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(authCookie);

                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác.");
            return View(user);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear(); // Xóa tất cả dữ liệu session
            return RedirectToAction("Login");
        }
    }
}