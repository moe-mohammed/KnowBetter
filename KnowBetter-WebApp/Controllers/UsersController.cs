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
            int? userId = HttpContext.Session.GetInt32(SessionKeyId);
            if (userId == null)
            {
                return RedirectToAction("Login", "Users");
            }
   
            ViewBag.MyName = HttpContext.Session.GetString(key: "_name");
            
            return View();
        }

        /// <summary>
        /// Sign up method return signup view
        /// </summary>
        public IActionResult Signup()
        {
            return View();
        }

        /// <summary>
        /// Sign up action takes user object, determines if it's already in database before adding it,
        /// then adds new user to daatabase
        /// </summary>
        /// <param name="user">User object to add to database.</param>
        /// <returns>Returns user to dashboard view after successful sign-up,
        /// or current view after failure</returns>
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
                    return RedirectToAction("Dashboard", "Home");
                }

                ViewBag.error = "User already exists";
                return View();
            }

            return View();
        }

        /// <summary>
        /// Login function returns login view
        /// </summary>
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Logout function clears the session and returns logout view
        /// </summary>
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("LogOut");
        }

        /// <summary>
        /// This method takes an email and password to query the database for a user match
        /// if matched a session/cookie is created for logged in user and they're redirected to dashboard
        /// </summary>
        /// <param name="email">Email of user logging in</param>
        /// <param name="password">Password of user logging in</param>
        /// <returns></returns>
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
