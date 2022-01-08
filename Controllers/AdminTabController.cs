using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductShop.Models;

namespace ProductShop.Controllers
{
    public class AdminTabController : Controller
    {
        private ShoppingContext db;

        public AdminTabController(ShoppingContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Users.Include(u => u.Customer).Select(u => MapUserToViewModel(u).Result).ToListAsync());
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await db.Users.Include(u => u.Customer)
                                           .FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(await MapUserToViewModel(user));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Role,RoleType")] AdminTab adminTab)
        {
            if (id != adminTab.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var user = await db.Users.FindAsync(id);

                if (user == null)
                    return NotFound();

                user.BuyerType = adminTab.RoleType;

                db.Update(user);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(adminTab);
        }

        private static async Task<AdminTab> MapUserToViewModel(User user)
        {
            return await Task.Run(() => new AdminTab
            {
                Id = user.Id,
                FullName = (user.Customer?.FirstName ?? "-") + " " + user.Customer?.LastName,
                Email = user.Email,
                Role = user.RoleId.ToString() == "1" ? "admin" : "buyer",
                RoleType = user.BuyerType
            });
        }
    }
}
