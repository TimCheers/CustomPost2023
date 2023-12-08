using Microsoft.AspNetCore.Mvc;
using CustomPost2023.Data.Models;
using Microsoft.EntityFrameworkCore;
using CustomPost2023.Controllers.Home;
using Serilog;
using Serilog.Events;

namespace CustomPost2023.Controllers.Admin
{
    public class UserController : Controller
    {
        ApplicationContext db;
        private loggs logg = new loggs();
        public static string meanBefForLogg;
        public UserController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.user.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(user us)
        {
            db.user.Add(us);
            logg.SendLogg(db, 1, "user", "whole record", "NULL", $"{us.user_name}|{us.login}|{us.password}|{us.role}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                user? us = await db.user.FirstOrDefaultAsync(p => p.user_id == id);
                if (us != null)
                {
                    logg.SendLogg(db, 2, "user", "whole record", $"{us.user_id}|{us.user_name}|{us.login}|{us.password}|{us.role}", "NULL");
                    db.user.Remove(us);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                user? us = await db.user.FirstOrDefaultAsync(p => p.user_id == id);
                if (us != null)
                {
                    meanBefForLogg = $"{us.user_id}|{us.user_name}|{us.login}|{us.password}|{us.role}";
                    return View(us);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(user us)
        {
            db.user.Update(us);
            logg.SendLogg(db, 3, "user", "whole record", meanBefForLogg, $"{us.user_id}|{us.user_name}|{us.login}|{us.password}|{us.role}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
