using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CustomPost2023.Controllers.Admin
{
    public class Export_countryController : Controller
    {
        ApplicationContext db;
        private loggs logg = new loggs();
        public static string meanBefForLogg;
        public Export_countryController(ApplicationContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            var model = db.export_countries;
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(export_countries ec)
        {
            db.export_countries.Add(ec);
            logg.SendLogg(db, 1, "export_countries", "whole record", "NULL", $"{ec.country_title}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                export_countries? ec = await db.export_countries.FirstOrDefaultAsync(p => p.id == id);
                if (ec != null)
                {
                    logg.SendLogg(db, 2, "export_countries", "whole record", $"{ec.id}|{ec.country_title}", "NULL");
                    db.export_countries.Remove(ec);
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
                export_countries? ec = await db.export_countries.FirstOrDefaultAsync(p => p.id == id);
                if (ec != null) 
                {
                    meanBefForLogg = $"{ec.id}|{ec.country_title}";
                    return View(ec); 
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(export_countries ec)
        {
            db.export_countries.Update(ec);
            logg.SendLogg(db, 3, "export_countries", "whole record", meanBefForLogg, $"{ec.id}|{ec.country_title}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
