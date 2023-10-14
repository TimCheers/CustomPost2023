using CustomPost2023.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomPost2023.Data.Repository;
using CustomPost2023.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomPost2023.Controllers.Admin
{
    public class UserController : Controller
    {
        ApplicationContext db;
        public UserController(ApplicationContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            var model = db.users;
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(user us)
        {
            db.users.Add(us);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                user? user = await db.users.FirstOrDefaultAsync(p => p.user_id == id);
                if (user != null)
                {
                    db.users.Remove(user);
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
                user? us = await db.users.FirstOrDefaultAsync(p => p.user_id == id);
                if (us != null) return View(us);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(user us)
        {
            db.users.Update(us);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
