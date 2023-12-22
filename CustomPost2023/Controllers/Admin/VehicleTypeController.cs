using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomPost2023.Controllers.Admin
{
    public class VehicleTypeController : Controller
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
        public VehicleTypeController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index(string countryStr, SortState sortOrder = SortState.IdAsc)
        {
            IQueryable<vehicle_type>? myLogg = db.vehicle_type;
            var n = from st in db.Set<vehicle_type>()
                select new { st };
            ViewData["IdSort"] = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            ViewData["nameSort"] = sortOrder == SortState.nameAsc ? SortState.nameDesc : SortState.nameAsc;
            
            try
            {
                switch (sortOrder)
                {
                    case SortState.IdAsc: return View(await n.OrderBy(s => s.st.vehicle_type_id).ToListAsync());
                    case SortState.IdDesc: return View(await n.OrderByDescending(s => s.st.vehicle_type_id).ToListAsync());
                    case SortState.nameAsc: return View(await n.OrderBy(s => s.st.vehicle_type_title).ToListAsync());
                    case SortState.nameDesc: return View(await n.OrderByDescending(s => s.st.vehicle_type_title).ToListAsync());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
            return View(await n.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Create(vehicle_type vt)
        {
            db.vehicle_type.Add(vt);
            logg.SendLogg(db, 1, "vehicle_type", "whole record", "NULL", $"{vt.vehicle_type_id}|{vt.vehicle_type_title}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                vehicle_type? vt = await db.vehicle_type.FirstOrDefaultAsync(p => p.vehicle_type_id == id);
                if (vt != null)
                {
                    db.vehicle_type.Remove(vt);
                    logg.SendLogg(db, 2, "vehicle_type", "whole record", $"{vt.vehicle_type_id}|{vt.vehicle_type_title}", "NULL");
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
                vehicle_type? vt = await db.vehicle_type.FirstOrDefaultAsync(p => p.vehicle_type_id == id);
                if (vt != null)
                {
                    meanBefForLogg = $"{vt.vehicle_type_id}|{vt.vehicle_type_title}";
                    return View(vt);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(vehicle_type vt)
        {
            db.vehicle_type.Update(vt);
            logg.SendLogg(db, 3, "vehicle_type", "whole record", meanBefForLogg, $"{vt.vehicle_type_id}|{vt.vehicle_type_title}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
