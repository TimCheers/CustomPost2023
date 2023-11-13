using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomPost2023.Controllers.Admin
{
    public class StatusController : Controller
    {
        ApplicationContext db;
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
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                status? st = await db.status.FirstOrDefaultAsync(p => p.status_id == id);
                if (st != null)
                {
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
                status? st = await db.status.FirstOrDefaultAsync(p => p.status_id == id);
                if (st != null) return View(st);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(status st)
        {
            db.status.Update(st);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
