using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomPost2023.Controllers.Admin
{
    public class HistoryController : Controller
    {
        ApplicationContext db;
        public HistoryController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            var n = from hi in db.Set<history>()
                    join ap in db.Set<application>() on hi.application_id equals ap.id
                    select new { hi, ap };
            return View(await n.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(history hi)
        {
            db.history.Add(hi);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                history? hi = await db.history.FirstOrDefaultAsync(p => p.id == id);
                if (hi != null)
                {
                    db.history.Remove(hi);
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
                history? hi = await db.history.FirstOrDefaultAsync(p => p.id == id);
                if (hi != null) return View(hi);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(history hi)
        {
            db.history.Update(hi);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
