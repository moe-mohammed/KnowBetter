using KnowBetter_WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KnowBetter_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public const string SessionKeyId = "_Id";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View("Login","Users");
        }
        public IActionResult SignUp()
        {
            return View("SignUp","Users");
        }


        public IActionResult Dashboard()
        {
            ViewBag.MyName = HttpContext.Session.GetString(key: "_Name");

            int? userId = HttpContext.Session.GetInt32(SessionKeyId);
            if (userId == null)
            {
                return RedirectToAction("Login", "Users");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
