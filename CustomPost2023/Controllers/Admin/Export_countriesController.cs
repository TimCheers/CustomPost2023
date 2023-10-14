using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomPost2023.Controllers.Admin
{
    public class Export_countriesController : Controller
    {
        ApplicationContext db;
        public Export_countriesController(ApplicationContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            var model = db.export_countriess;
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(export_countries ec)
        {
            db.export_countriess.Add(ec);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                export_countries? ec = await db.export_countriess.FirstOrDefaultAsync(p => p.country_id == id);
                if (ec != null)
                {
                    db.export_countriess.Remove(ec);
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
                export_countries? ec = await db.export_countriess.FirstOrDefaultAsync(p => p.country_id == id);
                if (ec != null) return View(ec);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(export_countries ec)
        {
            db.export_countriess.Update(ec);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
