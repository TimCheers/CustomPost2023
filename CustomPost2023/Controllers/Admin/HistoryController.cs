using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CustomPost2023.Controllers.Admin
{
    public class HistoryController : Controller
    {
        ApplicationContext db;
        
        public enum SortState
        {
            IdAsc,
            IdDesc,
            dateAsc,
            dateDesc,
            appIdAsc,
            appIdDesc,
        }
        public HistoryController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index(string countryStr, SortState sortOrder = SortState.IdAsc)
        {
            IQueryable<history>? myLogg = db.history;
            var n = from st in db.Set<history>()
                select new { st };
            ViewData["IdSort"] = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            ViewData["dateSort"] = sortOrder == SortState.dateAsc ? SortState.dateDesc : SortState.dateAsc;
            ViewData["appIdSort"] = sortOrder == SortState.appIdAsc ? SortState.appIdDesc : SortState.appIdAsc;
            
            try
            {
                switch (sortOrder)
                {
                    case SortState.IdAsc: return View(await n.OrderBy(s => s.st.id).ToListAsync());
                    case SortState.IdDesc: return View(await n.OrderByDescending(s => s.st.id).ToListAsync());
                    case SortState.dateAsc: return View(await n.OrderBy(s => s.st.custom_date).ToListAsync());
                    case SortState.dateDesc: return View(await n.OrderByDescending(s => s.st.custom_date).ToListAsync());
                    case SortState.appIdAsc: return View(await n.OrderBy(s => s.st.application_id).ToListAsync());
                    case SortState.appIdDesc: return View(await n.OrderByDescending(s => s.st.application_id).ToListAsync());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            return View(await n.ToListAsync());
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
