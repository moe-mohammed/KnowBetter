using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KnowBetter_WebApp.Data;
using KnowBetter_WebApp.Models;
using Microsoft.AspNetCore.Http;

namespace KnowBetter_WebApp.Controllers
{
    public class UsersController : Controller
    {
        public const string SessionKeyId = "_Id";
        public const string SessionKeyFirstName = "_Name";

        private readonly KnowBetter_WebAppContext _context;

        public UsersController(KnowBetter_WebAppContext context)
        {
            _context = context;
        }

        public IActionResult EditProfile()
        {
            /*int? userId = HttpContext.Session.GetInt32(SessionKeyId);
            if (userId == null)
            {
                return RedirectToAction("Login", "Users");
            }*/
            //commented out for testing purposes, must be logged into session for code to work
            //ViewBag.MyName = HttpContext.Session.GetString(key: "_name");

            //comment out once ready for final testing
            ViewBag.MyName = "Blake";

            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        // POST: Users/Signup
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup([Bind("UserId,Email,Password,Age,FirstName,LastName")] User user)
        {
            if (ModelState.IsValid)
            {
                var check = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (check == null)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetInt32(SessionKeyId, user.UserId);
                    HttpContext.Session.SetString(SessionKeyFirstName, user.FirstName);
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.error = "User already exists";
                return View();
            }

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.User.FirstOrDefaultAsync((u => u.Email.Equals(email) && u.Password.Equals(password)));
                if (user != null)
                {
                    HttpContext.Session.SetInt32(SessionKeyId, user.UserId);
                    HttpContext.Session.SetString(SessionKeyFirstName, user.FirstName);
                    return RedirectToAction("Dashboard", "Home");
                }

                ViewBag.error = "Login failed!";
                return View();
            }

            return View();
        }
        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
    }
}
