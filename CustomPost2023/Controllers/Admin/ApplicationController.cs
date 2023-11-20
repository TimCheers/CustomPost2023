using Microsoft.AspNetCore.Mvc;
using CustomPost2023.Data.Models;
using Microsoft.EntityFrameworkCore;
using CustomPost2023.Controllers.Home;
using Serilog;
using Serilog.Events;

namespace CustomPost2023.Controllers.Admin
{
    public class ApplicationController : Controller
    {
        ApplicationContext db;
        private loggs logg = new loggs();
        public static string meanBefForLogg;
        public ApplicationController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            var n = from ap in db.Set<application>()
                    join pr in db.Set<product>() on ap.product_id equals pr.product_id
                    join cp in db.Set<custom_post>() on ap.custom_post_id equals cp.customs_post_id
                    join st in db.Set<staff>() on ap.staff_id equals st.id
                    join ss in db.Set<status>() on ap.status_id equals ss.id
                    join us in db.Set<user>() on ap.user_id equals us.user_id
                    join co in db.Set<export_countries>() on ap.export_country_id equals co.id
                    select new { ap, pr, cp, st, ss, us, co };
            return View(await n.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(application ap)
        {
            db.application.Add(ap);
            //logg.SendLogg(db, 1, "application", "whole record", "NULL", $"{us.user_name}|{us.login}|{us.password}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                application? ap = await db.application.FirstOrDefaultAsync(p => p.id == id);
                if (ap != null)
                {
                    //logg.SendLogg(db, 2, "user", "whole record", $"{ap.user_id}|{us.user_name}|{us.login}|{us.password}", "NULL");
                    db.application.Remove(ap);
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
                application? ap = await db.application.FirstOrDefaultAsync(p => p.id == id);
                if (ap != null)
                {
                    //meanBefForLogg = $"{us.user_id}|{us.user_name}|{us.login}|{us.password}";
                    return View(ap);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(application ap)
        {
            db.application.Update(ap);
            //logg.SendLogg(db, 3, "user", "whole record", meanBefForLogg, $"{us.user_id}|{us.user_name}|{us.login}|{us.password}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
