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
                FormsAuthentication.SetAuthCookie(userLogin.username, false);

                // Lưu trữ vai trò trong session để sử dụng ở các nơi khác trong ứng dụng nếu cần
                Session["userRole"] = userLogin.role;

                return RedirectToAction("Index", "Home");
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