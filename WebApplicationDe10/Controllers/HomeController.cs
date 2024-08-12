using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationDe10.Services;

namespace WebApplicationDe10.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService userService;

        public HomeController()
        {
            userService = new UserService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var products = userService.GetAllUsers();
            return View(products);
        }

        // public ActionResult Details(int id)
        // {
        //     var product = _productService.GetProductById(id);
        //     if (product == null)
        //     {
        //         return HttpNotFound();
        //     }
        //     return View(product);
        // }
        // 
        // [HttpPost]
        // public ActionResult Create(Product product)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         bool success = _productService.AddProduct(product);
        //         if (success)
        //         {
        //             return RedirectToAction("Index");
        //         }
        //     }
        //     return View(product);
        // }
        // 
        // [HttpPost]
        // public ActionResult Edit(Product product)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         bool success = _productService.UpdateProduct(product);
        //         if (success)
        //         {
        //             return RedirectToAction("Index");
        //         }
        //     }
        //     return View(product);
        // }
        // 
        // [HttpPost]
        // public ActionResult Delete(int id)
        // {
        //     bool success = _productService.DeleteProduct(id);
        //     if (success)
        //     {
        //         return RedirectToAction("Index");
        //     }
        //     return HttpNotFound();
        // }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}