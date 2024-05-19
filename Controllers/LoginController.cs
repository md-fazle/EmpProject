using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models.UserAuth;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Rls.SingleOrDefault(x => x.Email == model.Email);
                if (user != null && VerifyPassword(model.Password, user.Password))
                {
                    // Authentication successful, implement your login logic here
                    return RedirectToAction("Index", "Home"); // Redirect to homepage after successful login
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
                }
            }

            return View(model);
        }

        private bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedInputBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputPassword));
                var hashedInputString = BitConverter.ToString(hashedInputBytes).Replace("-", "").ToLower();
                return hashedInputString == storedHashedPassword;
            }
        }
    }
}
