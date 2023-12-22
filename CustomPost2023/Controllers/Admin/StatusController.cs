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
        
        public enum SortState
        {
            IdAsc,
            IdDesc,
            nameAsc,
            nameDesc,
        }
        public StatusController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index(string countryStr, SortState sortOrder = SortState.IdAsc)
        {
            IQueryable<status>? myLogg = db.status;
            var n = from st in db.Set<status>()
                select new { st };
            ViewData["IdSort"] = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            ViewData["nameSort"] = sortOrder == SortState.nameAsc ? SortState.nameDesc : SortState.nameAsc;
            
            try
            {
                switch (sortOrder)
                {
                    case SortState.IdAsc: return View(await n.OrderBy(s => s.st.id).ToListAsync());
                    case SortState.IdDesc: return View(await n.OrderByDescending(s => s.st.id).ToListAsync());
                    case SortState.nameAsc: return View(await n.OrderBy(s => s.st.status_title).ToListAsync());
                    case SortState.nameDesc: return View(await n.OrderByDescending(s => s.st.status_title).ToListAsync());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            return View(await n.ToListAsync());
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
