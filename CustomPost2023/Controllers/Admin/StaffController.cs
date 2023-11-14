using Microsoft.AspNetCore.Mvc;
using CustomPost2023.Data.Models;
using Microsoft.EntityFrameworkCore;
using CustomPost2023.Controllers.Home;
using Serilog;
using Serilog.Events;

namespace CustomPost2023.Controllers.Admin
{
    public class StaffController : Controller
    {
        ApplicationContext db;
        private loggs logg = new loggs();
        public static string meanBefForLogg;
        public StaffController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.staff.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(staff sf)
        {
            db.staff.Add(sf);
            logg.SendLogg(db, 1, "staff", "whole record", "NULL", $"{sf.id}|{sf.name}|{sf.age}|{sf.work_experience}|{sf.custom_post_id}|{sf.custom_post_id}|{sf.phone_number}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                staff? sf = await db.staff.FirstOrDefaultAsync(p => p.id == id);
                if (sf != null)
                {
                    logg.SendLogg(db, 2, "user", "whole record", $"{sf.name}|{sf.age}|{sf.work_experience}|{sf.custom_post_id}|{sf.custom_post_id}|{sf.phone_number}", "NULL");
                    db.staff.Remove(sf);
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
                staff? sf = await db.staff.FirstOrDefaultAsync(p => p.id == id);
                if (sf != null)
                {
                    meanBefForLogg = $"{sf.id}|{sf.name}|{sf.age}|{sf.work_experience}|{sf.custom_post_id}|{sf.phone_number}";
                    return View(sf);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(staff sf)
        {
            db.staff.Update(sf);
            logg.SendLogg(db, 3, "user", "whole record", meanBefForLogg, $"{sf.id}|{sf.name}|{sf.age}|{sf.work_experience}|{sf.custom_post_id}|{sf.phone_number}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
