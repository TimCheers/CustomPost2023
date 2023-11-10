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

        public UserController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            List<user> model = db.user.ToList();
            Log.Information("@@@ Выполнен запрос на вывод всех пользователей. Колличество пользователей: " + model.Count);
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
            await db.SaveChangesAsync();
            Log.Information("@@@ Выполненено добавление пользователя: " + us.user_id + ' ' + us.user_name + ' ' + us.login);
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
                    db.user.Remove(us);
                    Log.Information("@@@ Выполненено удаление пользователя: " + us.user_id + ' ' + us.user_name + ' ' + us.login);
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
                if (us != null) return View(us);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(user us)
        {
            db.user.Update(us);
            await db.SaveChangesAsync();
            Log.Information("@@@ Выполненено изменение пользователя: " + us.user_id + ' ' + us.user_name + ' ' + us.login + ' ' + us.password );
            return RedirectToAction("Index");
        }
    }
}
