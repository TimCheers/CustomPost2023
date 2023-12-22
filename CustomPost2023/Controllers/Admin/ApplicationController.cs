using Microsoft.AspNetCore.Mvc;
using CustomPost2023.Data.Models;
using Microsoft.EntityFrameworkCore;
using CustomPost2023.Controllers.Home;
using Serilog;
using Serilog.Events;

namespace CustomPost2023.Controllers.Admin
{
    public class ApplicationController : Controller
    {
        ApplicationContext db;
        private loggs logg = new loggs();
        public static string meanBefForLogg;
        
        public enum SortState
        {
            IdAsc,
            IdDesc,
            prodAsc,
            prodDesc,
            locAsc,
            locDesc,
            staffAsc,
            staffDesc,
            statusAsc,
            statusDesc,
            impAsc,
            impDesc,
            expAsc,
            expDesc,
        }
        public ApplicationController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index(string prodStr, string locStr, string staffStr, string statusStr, string impStr, string expStr, SortState sortOrder = SortState.IdAsc)
        {
            IQueryable<product>? myLogg = db.product;
            var n = from ap in db.Set<application>()
                join pr in db.Set<product>() on ap.product_id equals pr.product_id
                join cp in db.Set<custom_post>() on ap.custom_post_id equals cp.customs_post_id
                join st in db.Set<staff>() on ap.staff_id equals st.id
                join ss in db.Set<status>() on ap.status_id equals ss.id
                join us in db.Set<user>() on ap.user_id equals us.user_id
                join co in db.Set<export_countries>() on ap.export_country_id equals co.id
                select new { ap, pr, cp, st, ss, us, co };
            
            ViewData["IdSort"] = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            ViewData["prodSort"] = sortOrder == SortState.prodAsc ? SortState.prodDesc : SortState.prodAsc;
            ViewData["locSort"] = sortOrder == SortState.locAsc ? SortState.locDesc : SortState.locAsc;
            ViewData["staffSort"] = sortOrder == SortState.staffAsc ? SortState.staffDesc : SortState.staffAsc;
            ViewData["statusSort"] = sortOrder == SortState.statusAsc ? SortState.statusDesc : SortState.statusAsc;
            ViewData["importSort"] = sortOrder == SortState.impAsc ? SortState.impDesc : SortState.impAsc;
            ViewData["exportSort"] = sortOrder == SortState.expAsc ? SortState.expDesc : SortState.expAsc;
            
            if (!string.IsNullOrEmpty(prodStr))
            {
                n = n.Where(p => p.pr.product_title.Contains(prodStr));
            }
            if (!string.IsNullOrEmpty(locStr))
            {
                n = n.Where(p => p.cp.customs_post_title.Contains(locStr));
            }
            if (!string.IsNullOrEmpty(staffStr))
            {
                n = n.Where(p => p.st.name.Contains(staffStr));
            }
            if (!string.IsNullOrEmpty(statusStr))
            {
                n = n.Where(p => p.ss.status_title.Contains(statusStr));
            }
            
            if (!string.IsNullOrEmpty(impStr))
            {
                n = n.Where(p => p.us.user_name.Contains(impStr));
            }
            
            if (!string.IsNullOrEmpty(expStr))
            {
                n = n.Where(p => p.co.country_title.Contains(expStr));
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
                    case SortState.IdAsc: return View(await n.OrderBy(s => s.ap.id).ToListAsync());
                    case SortState.IdDesc: return View(await n.OrderByDescending(s => s.ap.id).ToListAsync());
                    case SortState.prodAsc: return View(await n.OrderBy(s => s.pr.product_title).ToListAsync());
                    case SortState.prodDesc: return View(await n.OrderByDescending(s => s.pr.product_title).ToListAsync());
                    case SortState.locAsc: return View(await n.OrderBy(s => s.cp.customs_post_title).ToListAsync());
                    case SortState.locDesc: return View(await n.OrderByDescending(s => s.cp.customs_post_title).ToListAsync());
                    case SortState.staffAsc: return View(await n.OrderBy(s => s.st.name).ToListAsync());
                    case SortState.staffDesc: return View(await n.OrderByDescending(s => s.st.name).ToListAsync());
                    case SortState.statusAsc: return View(await n.OrderBy(s => s.ss.status_title).ToListAsync());
                    case SortState.statusDesc: return View(await n.OrderByDescending(s => s.ss.status_title).ToListAsync());
                    case SortState.impAsc: return View(await n.OrderBy(s => s.us.user_name).ToListAsync());
                    case SortState.impDesc: return View(await n.OrderByDescending(s => s.us.user_name).ToListAsync());
                    case SortState.expAsc: return View(await n.OrderBy(s => s.co.country_title).ToListAsync());
                    case SortState.expDesc: return View(await n.OrderByDescending(s => s.co.country_title).ToListAsync());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View(await n.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Create(application ap)
        {
            db.application.Add(ap);
            //logg.SendLogg(db, 1, "application", "whole record", "NULL", $"{us.user_name}|{us.login}|{us.password}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                application? ap = await db.application.FirstOrDefaultAsync(p => p.id == id);
                if (ap != null)
                {
                    //logg.SendLogg(db, 2, "user", "whole record", $"{ap.user_id}|{us.user_name}|{us.login}|{us.password}", "NULL");
                    db.application.Remove(ap);
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
                application? ap = await db.application.FirstOrDefaultAsync(p => p.id == id);
                if (ap != null)
                {
                    //meanBefForLogg = $"{us.user_id}|{us.user_name}|{us.login}|{us.password}";
                    return View(ap);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(application ap)
        {
            db.application.Update(ap);
            //logg.SendLogg(db, 3, "user", "whole record", meanBefForLogg, $"{us.user_id}|{us.user_name}|{us.login}|{us.password}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
