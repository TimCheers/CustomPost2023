using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomPost2023.Controllers.Admin
{
    public class CustomPostController : Controller
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
            locAsc,
            locDesc,
            thrAsc,
            thrDesc,
            vehIdAsc,
            vehIdDesc,
        }
        public CustomPostController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index(string nameStr, string locStr, string thrStr, string vehStr, SortState sortOrder = SortState.IdAsc)
        {
            IQueryable<custom_post>? myLogg = db.custom_post;
            var n = from st in db.Set<custom_post>()
                    select new { st };
            ViewData["IdSort"] = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            ViewData["nameSort"] = sortOrder == SortState.nameAsc ? SortState.nameDesc : SortState.nameAsc;
            ViewData["massSort"] = sortOrder == SortState.locAsc ? SortState.locDesc : SortState.locAsc;
            ViewData["countSort"] = sortOrder == SortState.thrAsc ? SortState.thrDesc : SortState.thrAsc;
            ViewData["priceSort"] = sortOrder == SortState.vehIdAsc ? SortState.vehIdDesc : SortState.vehIdAsc;
            if (!string.IsNullOrEmpty(nameStr))
            {
                n = n.Where(p => p.st.customs_post_title.Contains(nameStr));
            }
            if (!string.IsNullOrEmpty(locStr))
            {
                n = n.Where(p => p.st.location.Contains(locStr));
            }
            if (!string.IsNullOrEmpty(thrStr))
            {
                n = n.Where(p => p.st.throughput.Equals(int.Parse(thrStr)));
            }
            if (!string.IsNullOrEmpty(vehStr))
            {
                n = n.Where(p => p.st.fk_vehicle_id.Equals(int.Parse(vehStr)));
            }

            //if (datetimeStr != DateTime.MinValue.Date)
            //{
            //    n = n.Where(p => p.st.datetime.Date.Equals(datetimeStr.Date));
            //}
            //Console.WriteLine($"Передача: {datetimeStr}\n Текущая: {n}");
            try
            {
                switch (sortOrder)
                {
                    case SortState.IdAsc: return View(await n.OrderBy(s => s.st.customs_post_id).ToListAsync());
                    case SortState.IdDesc: return View(await n.OrderByDescending(s => s.st.customs_post_id).ToListAsync());
                    case SortState.nameAsc: return View(await n.OrderBy(s => s.st.customs_post_title).ToListAsync());
                    case SortState.nameDesc: return View(await n.OrderByDescending(s => s.st.customs_post_title).ToListAsync());
                    case SortState.locAsc: return View(await n.OrderBy(s => s.st.location).ToListAsync());
                    case SortState.locDesc: return View(await n.OrderByDescending(s => s.st.location).ToListAsync());
                    case SortState.thrAsc: return View(await n.OrderBy(s => s.st.throughput).ToListAsync());
                    case SortState.thrDesc: return View(await n.OrderByDescending(s => s.st.throughput).ToListAsync());
                    case SortState.vehIdAsc: return View(await n.OrderBy(s => s.st.fk_vehicle_id).ToListAsync());
                    case SortState.vehIdDesc: return View(await n.OrderByDescending(s => s.st.fk_vehicle_id).ToListAsync());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View(await n.ToListAsync());
        }
        [HttpPost]
        
        public async Task<IActionResult> Create(custom_post cp)
        {
            db.custom_post.Add(cp);
            logg.SendLogg(db, 1, "custom_post", "whole record", "NULL", $"{cp.customs_post_id}|{cp.customs_post_title}|{cp.location}|{cp.throughput}|{cp.fk_vehicle_id}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                custom_post? cp = await db.custom_post.FirstOrDefaultAsync(p => p.customs_post_id == id);
                if (cp != null)
                {
                    logg.SendLogg(db, 2, "custom_post", "whole record", $"{cp.customs_post_id}|{cp.customs_post_title}|{cp.location}|{cp.throughput}|{cp.fk_vehicle_id}", "NULL");
                    db.custom_post.Remove(cp);
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
                custom_post? cp = await db.custom_post.FirstOrDefaultAsync(p => p.customs_post_id == id);
                if (cp != null)
                {
                    meanBefForLogg = $"{cp.customs_post_id}|{cp.customs_post_title}|{cp.location}|{cp.throughput}|{cp.fk_vehicle_id}";
                    return View(cp);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(custom_post cp)
        {
            db.custom_post.Update(cp);
            logg.SendLogg(db, 3, "custom_post", "whole record", meanBefForLogg, $"{cp.customs_post_id}|{cp.customs_post_title}|{cp.location}|{cp.throughput}|{cp.fk_vehicle_id}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
