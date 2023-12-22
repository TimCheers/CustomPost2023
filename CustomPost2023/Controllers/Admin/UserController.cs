using Microsoft.AspNetCore.Mvc;
using CustomPost2023.Data.Models;
using Microsoft.EntityFrameworkCore;
using CustomPost2023.Controllers.Home;
using Serilog;
using Serilog.Events;

namespace CustomPost2023.Controllers.Admin
{
    public class UserController : Controller
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
            loginAsc,
            loginDesc,
            passAsc,
            passDesc,
            roleAsc,
            roleDesc,
        }
        public UserController(ApplicationContext context)
        {
            db = context;
        }
        
        public async Task<IActionResult> Index(string nameStr, string loginStr, string passStr, string roleStr, SortState sortOrder = SortState.IdAsc)
        {
            IQueryable<user>? myLogg = db.user;
            var n = from st in db.Set<user>()
                    select new { st };
            ViewData["IdSort"] = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            ViewData["nameSort"] = sortOrder == SortState.nameAsc ? SortState.nameDesc : SortState.nameAsc;
            ViewData["loginSort"] = sortOrder == SortState.loginAsc ? SortState.loginDesc : SortState.loginAsc;
            ViewData["passSort"] = sortOrder == SortState.passAsc ? SortState.passDesc : SortState.passAsc;
            ViewData["roleSort"] = sortOrder == SortState.roleAsc ? SortState.roleDesc : SortState.roleAsc;
            
            if (!string.IsNullOrEmpty(nameStr))
            {
                n = n.Where(p => p.st.user_name.Contains(nameStr));
            }
            if (!string.IsNullOrEmpty(loginStr))
            {
                n = n.Where(p => p.st.login.Contains(loginStr));
            }
            if (!string.IsNullOrEmpty(passStr))
            {
                n = n.Where(p => p.st.password.Contains(passStr));
            }
            if (!string.IsNullOrEmpty(roleStr))
            {
                n = n.Where(p => p.st.role.Contains(roleStr));
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
                    case SortState.IdAsc: return View(await n.OrderBy(s => s.st.user_id).ToListAsync());
                    case SortState.IdDesc: return View(await n.OrderByDescending(s => s.st.user_id).ToListAsync());
                    case SortState.nameAsc: return View(await n.OrderBy(s => s.st.user_name).ToListAsync());
                    case SortState.nameDesc: return View(await n.OrderByDescending(s => s.st.user_name).ToListAsync());
                    case SortState.loginAsc: return View(await n.OrderBy(s => s.st.login).ToListAsync());
                    case SortState.loginDesc: return View(await n.OrderByDescending(s => s.st.login).ToListAsync());
                    case SortState.passAsc: return View(await n.OrderBy(s => s.st.password).ToListAsync());
                    case SortState.passDesc: return View(await n.OrderByDescending(s => s.st.password).ToListAsync());
                    case SortState.roleAsc: return View(await n.OrderBy(s => s.st.role).ToListAsync());
                    case SortState.roleDesc: return View(await n.OrderByDescending(s => s.st.role).ToListAsync());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View(await n.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Create(user us)
        {
            db.user.Add(us);
            logg.SendLogg(db, 1, "user", "whole record", "NULL", $"{us.user_name}|{us.login}|{us.password}|{us.role}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                user? us = await db.user.FirstOrDefaultAsync(p => p.user_id == id);
                if (us != null)
                {
                    logg.SendLogg(db, 2, "user", "whole record", $"{us.user_id}|{us.user_name}|{us.login}|{us.password}|{us.role}", "NULL");
                    db.user.Remove(us);
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
                user? us = await db.user.FirstOrDefaultAsync(p => p.user_id == id);
                if (us != null)
                {
                    meanBefForLogg = $"{us.user_id}|{us.user_name}|{us.login}|{us.password}|{us.role}";
                    return View(us);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(user us)
        {
            db.user.Update(us);
            logg.SendLogg(db, 3, "user", "whole record", meanBefForLogg, $"{us.user_id}|{us.user_name}|{us.login}|{us.password}|{us.role}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
