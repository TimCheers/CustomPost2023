using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CustomPost2023.Controllers.Admin
{
    public class StatusController : Controller
    {
        ApplicationContext db;
        private loggs logg = new loggs();
        public static string meanBefForLogg;
        public StatusController(ApplicationContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            var model = db.status;
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(status st)
        {
            db.status.Add(st);
            logg.SendLogg(db, 1, "status", "whole record", "NULL", $"{st.status_title}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                status? st = await db.status.FirstOrDefaultAsync(p => p.id == id);
                if (st != null)
                {
                    logg.SendLogg(db, 2, "status", "whole record", $"{st.id}|{st.status_title}", "NULL");
                    db.status.Remove(st);
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
                status? st = await db.status.FirstOrDefaultAsync(p => p.id == id);
                if (st != null) 
                {
                    meanBefForLogg = $"{st.id}|{st.status_title}";
                    return View(st); 
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(status st)
        {
            db.status.Update(st);
            logg.SendLogg(db, 3, "status", "whole record", meanBefForLogg, $"{st.id}|{st.status_title}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
