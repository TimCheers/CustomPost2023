using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CustomPost2023.Controllers.Admin
{
    public class ProductTypeController : Controller
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
            coefAsc,
            coefDesc,
        }
        public ProductTypeController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index(string countryStr, SortState sortOrder = SortState.IdAsc)
        {
            IQueryable<product_type>? myLogg = db.product_type;
            var n = from st in db.Set<product_type>()
                select new { st };
            ViewData["IdSort"] = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            ViewData["nameSort"] = sortOrder == SortState.nameAsc ? SortState.nameDesc : SortState.nameAsc;
            ViewData["coefSort"] = sortOrder == SortState.coefAsc ? SortState.coefDesc : SortState.coefAsc;
            
            try
            {
                switch (sortOrder)
                {
                    case SortState.IdAsc: return View(await n.OrderBy(s => s.st.type_product_id).ToListAsync());
                    case SortState.IdDesc: return View(await n.OrderByDescending(s => s.st.type_product_id).ToListAsync());
                    case SortState.nameAsc: return View(await n.OrderBy(s => s.st.type_product_title).ToListAsync());
                    case SortState.nameDesc: return View(await n.OrderByDescending(s => s.st.type_product_title).ToListAsync());
                    case SortState.coefAsc: return View(await n.OrderBy(s => s.st.customs_clearance_coefficient).ToListAsync());
                    case SortState.coefDesc: return View(await n.OrderByDescending(s => s.st.customs_clearance_coefficient).ToListAsync());
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
        public async Task<IActionResult> Create(product_type pt)
        {
            db.product_type.Add(pt);
            logg.SendLogg(db, 1, "product_type", "whole record", "NULL", $"{pt.type_product_id}|{pt.type_product_title}|{pt.customs_clearance_coefficient}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                product_type? pt = await db.product_type.FirstOrDefaultAsync(p => p.type_product_id == id);
                if (pt != null)
                {
                    db.product_type.Remove(pt);
                    logg.SendLogg(db, 2, "product_type", "whole record", $"{pt.type_product_id}|{pt.type_product_title}|{pt.customs_clearance_coefficient}", "NULL");
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
                product_type? pt = await db.product_type.FirstOrDefaultAsync(p => p.type_product_id == id);
                if (pt != null) 
                {
                    meanBefForLogg = $"{pt.type_product_id}|{pt.type_product_title}|{pt.customs_clearance_coefficient}";
                    return View(pt); 
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(product_type pt)
        {
            db.product_type.Update(pt);
            logg.SendLogg(db, 3, "product_type", "whole record", meanBefForLogg, $"{pt.type_product_id}|{pt.type_product_title}|{pt.customs_clearance_coefficient}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
