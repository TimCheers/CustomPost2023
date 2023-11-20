using Microsoft.AspNetCore.Mvc;
using CustomPost2023.Data.Models;
using Microsoft.EntityFrameworkCore;
using CustomPost2023.Controllers.Home;
using Serilog;
using Serilog.Events;
using CustomPost2023.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Xml.Linq;

namespace CustomPost2023.Controllers.Admin
{
    public class StaffController : Controller
    {
        ApplicationContext db;
        private loggs logg = new loggs();
        public static string meanBefForLogg;
        public enum SortState
        {
            IdAsc,
            IdDesc,
            NameAsc,
            NameDesc,
            AgeAsc,
            AgeDesc,
            work_experienceAsc,
            work_experienceDesc,
            customs_post_titleAsc,
            customs_post_titleDesc,
            job_titleAsc,
            job_titleDesc,
            phone_numberAsc,
            phone_numberDesc
        }
        public StaffController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index(string name, string custom_post, SortState sortOrder = SortState.IdAsc)
        {
            var n = from st in db.Set<staff>()
                    join cp in db.Set<custom_post>() on st.custom_post_id equals cp.customs_post_id
                    select new { st, cp };
            ViewData["IdSort"] = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["AgeSort"] = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
            ViewData["work_experienceSort"] = sortOrder == SortState.work_experienceAsc ? SortState.work_experienceDesc : SortState.work_experienceAsc;
            ViewData["customs_post_titleSort"] = sortOrder == SortState.customs_post_titleAsc ? SortState.customs_post_titleDesc : SortState.customs_post_titleAsc;
            ViewData["job_titleSort"] = sortOrder == SortState.job_titleAsc ? SortState.job_titleDesc : SortState.job_titleAsc;
            ViewData["phone_numberSort"] = sortOrder == SortState.phone_numberAsc ? SortState.phone_numberDesc : SortState.phone_numberAsc;
            if (!string.IsNullOrEmpty(name))
            {
                n = n.Where(p => p.st.name.Contains(name));
            }
            if (!string.IsNullOrEmpty(custom_post))
            {
                n = n.Where(p => p.cp.customs_post_title.Contains(custom_post));
            }
            switch (sortOrder)
            {
                case SortState.IdAsc: return View(await n.OrderBy(p => p.st.id).ToListAsync());
                case SortState.IdDesc: return View(await n.OrderByDescending(p => p.st.id).ToListAsync());
                case SortState.NameAsc: return View(await n.OrderBy(p => p.st.name).ToListAsync());
                case SortState.NameDesc: return View(await n.OrderByDescending(p => p.st.name).ToListAsync());
                case SortState.AgeAsc: return View(await n.OrderBy(p => p.st.age).ToListAsync());
                case SortState.AgeDesc: return View(await n.OrderByDescending(p => p.st.age).ToListAsync());
                case SortState.work_experienceAsc: return View(await n.OrderBy(p => p.st.work_experience).ToListAsync());
                case SortState.work_experienceDesc: return View(await n.OrderByDescending(p => p.st.work_experience).ToListAsync());
                case SortState.customs_post_titleAsc: return View(await n.OrderBy(p => p.cp.customs_post_title).ToListAsync());
                case SortState.customs_post_titleDesc: return View(await n.OrderByDescending(p => p.cp.customs_post_title).ToListAsync());
                case SortState.job_titleAsc: return View(await n.OrderBy(p => p.st.id).ToListAsync());
                case SortState.job_titleDesc: return View(await n.OrderByDescending(p => p.st.id).ToListAsync());
                case SortState.phone_numberAsc: return View(await n.OrderBy(p => p.st.phone_number).ToListAsync());
                case SortState.phone_numberDesc: return View(await n.OrderByDescending(p => p.st.phone_number).ToListAsync());
            }
            return View(await n.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(staff sf)
        {
            db.staff.Add(sf);
            logg.SendLogg(db, 1, "staff", "whole record", "NULL", $"{sf.id}|{sf.name}|{sf.age}|{sf.work_experience}|{sf.job_title}|{sf.custom_post_id}|{sf.custom_post_id}|{sf.phone_number}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                staff? sf = await db.staff.FirstOrDefaultAsync(p => p.id == id);
                if (sf != null)
                {
                    logg.SendLogg(db, 2, "staff", "whole record", $"{sf.name}|{sf.age}|{sf.work_experience}|{sf.job_title}|{sf.custom_post_id}|{sf.custom_post_id}|{sf.phone_number}", "NULL");
                    db.staff.Remove(sf);
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
                staff? sf = await db.staff.FirstOrDefaultAsync(p => p.id == id);
                if (sf != null)
                {
                    meanBefForLogg = $"{sf.id}|{sf.name}|{sf.age}|{sf.work_experience}|{sf.job_title}|{sf.custom_post_id}|{sf.phone_number}";
                    return View(sf);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(staff sf)
        {
            db.staff.Update(sf);
            logg.SendLogg(db, 3, "staff", "whole record", meanBefForLogg, $"{sf.id}|{sf.name}|{sf.age}|{sf.work_experience}|{sf.job_title}|{sf.custom_post_id}|{sf.phone_number}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
