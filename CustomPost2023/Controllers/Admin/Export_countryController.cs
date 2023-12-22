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
        
        public enum SortState
        {
            IdAsc,
            IdDesc,
            countryAsc,
            countryDesc,
        }
        
        public Export_countryController(ApplicationContext context)
        {
            db = context;
        }
        
        public async Task<IActionResult> index(string countryStr, SortState sortOrder = SortState.IdAsc)
        {
            IQueryable<export_countries>? myLogg = db.export_countries;
            var n = from st in db.Set<export_countries>()
                    select new { st };
            ViewData["IdSort"] = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            ViewData["countrySort"] = sortOrder == SortState.countryAsc ? SortState.countryDesc : SortState.countryAsc;

            if (!string.IsNullOrEmpty(countryStr))
            {
                n = n.Where(p => p.st.country_title.Contains(countryStr));
            }
            //if (datetimeStr != DateTime.MinValue.Date)
            //{
            //    n = n.Where(p => p.st.datetime.Date.Equals(datetimeStr.Date));
            //}
            Console.WriteLine($"Передача: {countryStr}\n Текущая: {n}");
            try
            {
                switch (sortOrder)
                {
                    case SortState.IdAsc: return View(await n.OrderBy(s => s.st.id).ToListAsync());
                    case SortState.IdDesc: return View(await n.OrderByDescending(s => s.st.id).ToListAsync());
                    case SortState.countryAsc: return View(await n.OrderBy(s => s.st.country_title).ToListAsync());
                    case SortState.countryDesc: return View(await n.OrderByDescending(s => s.st.country_title).ToListAsync());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View(await n.ToListAsync());
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
