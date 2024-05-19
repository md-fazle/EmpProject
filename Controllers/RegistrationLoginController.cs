using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models.UserAuth;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeManagementSystem.Controllers
{
    public class RegistrationLoginController : Controller
    {
        private readonly AppDbContext _context;

        public RegistrationLoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Rl rl)
        {
            if (ModelState.IsValid)
            {
                if (_context.Rls.Any(x => x.Email == rl.Email))
                {
                    ModelState.AddModelError("Email", "Email is already registered.");
                    return View(rl);
                }

                if (rl.Password != rl.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                    return View(rl);
                }

                try
                {
                    rl.Password = HashPassword(rl.Password);
                    rl.RegisterDate = DateTime.Now;
                    _context.Rls.Add(rl);
                    _context.SaveChanges();

                    return RedirectToAction("Login"); // Redirect to login page after successful registration
                }
                catch (Exception ex)
                {
                    // Handle database operation exception
                    ModelState.AddModelError(string.Empty, "Registration failed. Please try again later.");
                    // Log the exception for debugging
                    return View(rl);
                }
            }

            return View(rl);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
 
    }
}









//[HttpGet]
//public IActionResult Login()
//{
//    return View();
//}

//[HttpPost]
//[ValidateAntiForgeryToken]
//public IActionResult Login(Rl rl)
//{
//    if (ModelState.IsValid)
//    {
//        var user = _context.Rls.SingleOrDefault(x => x.Email == rl.Email);
//        if (user != null && VerifyPassword(rl.Password, user.Password))
//        {
//            // Authentication successful, implement your login logic here
//            return RedirectToAction("Index", "Home"); // Redirect to homepage after successful login
//        }
//        else
//        {
//            ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
//        }
//    }

//    return View(rl);
//}

//private bool VerifyPassword(string inputPassword, string storedHashedPassword)
//{
//    using (var sha256 = SHA256.Create())
//    {
//        var hashedInputBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
//        var hashedInputString = BitConverter.ToString(hashedInputBytes).Replace("-", "").ToLower();
//        return hashedInputString == storedHashedPassword;
//    }
//}