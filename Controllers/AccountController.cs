using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ProductShop.Models;
using ProductShop.ViewModels;

namespace ProductShop.Controllers
{
    public class AccountController : Controller
    {
        private ShoppingContext db;

        public AccountController(ShoppingContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == model.Email
                    && u.Password == model.Password);
                    
                if (user != null)
                {
                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect login and(or) password");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // add user to db
                    user = new User { Email = model.Email, Password = model.Password };
                    
                    Role defaultRole = await db.Roles.FirstOrDefaultAsync(r => r.Name == "buyer");
                    user.Role = defaultRole;
                    user.BuyerType = BuyerType.Regular;

                    db.Users.Add(user);
                    await db.SaveChangesAsync();

                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "This email already in use");
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect login and(or) password");
            }
            return View(model);
        }
        
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name),
                new Claim("buyerType", user.BuyerType.ToString().ToLower())
            };
            // create ClaimsIdentity object
            ClaimsIdentity id = new ClaimsIdentity(claims,
                "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // set auth cookies
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
