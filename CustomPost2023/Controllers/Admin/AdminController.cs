using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Xml.Linq;

namespace CustomPost2023.Controllers.Admin
{
    public class AdminController : Controller
    {
        ApplicationContext db;
        public enum SortState
        {
            IdAsc,
            IdDesc,
            datetimeAsc,
            datetimeDesc,
            user_idAsc,
            user_idDesc,
            actionAsc,
            actionDesc,
            tableAsc,
            tableDesc,
            attributeAsc,
            attributeDesc,
            meaning_beforeAsc,
            meaning_beforeDesc,
        }
        public AdminController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Loggs(string actionStr, string tableStr, DateTime datetimeStr, SortState sortOrder = SortState.IdAsc)
        {
            IQueryable<loggs>? myLogg = db.loggs;
            var n = from st in db.Set<loggs>()
                    select new { st };
            ViewData["IdSort"] = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            ViewData["datetimeSort"] = sortOrder == SortState.datetimeAsc ? SortState.datetimeDesc : SortState.datetimeAsc;
            ViewData["user_idSort"] = sortOrder == SortState.user_idAsc ? SortState.user_idDesc : SortState.user_idAsc;
            ViewData["attributeSort"] = sortOrder == SortState.attributeAsc ? SortState.attributeDesc : SortState.attributeAsc;
            ViewData["actionSort"] = sortOrder == SortState.actionAsc ? SortState.actionDesc : SortState.actionAsc;
            ViewData["tableSort"] = sortOrder == SortState.tableAsc ? SortState.tableDesc : SortState.tableAsc;
            if (!string.IsNullOrEmpty(actionStr))
            {
                n = n.Where(p => p.st.action.Contains(actionStr));
            }
            if (!string.IsNullOrEmpty(tableStr))
            {
                n = n.Where(p => p.st.table.Contains(tableStr));
            }
            //if (datetimeStr != DateTime.MinValue.Date)
            //{
            //    n = n.Where(p => p.st.datetime.Date.Equals(datetimeStr.Date));
            //}
            Console.WriteLine($"Передача: {datetimeStr}\n Текущая: {n}");
            try
            {
                switch (sortOrder)
                {
                    case SortState.IdAsc: return View(await n.OrderBy(s => s.st.id).ToListAsync());
                    case SortState.IdDesc: return View(await n.OrderByDescending(s => s.st.id).ToListAsync());
                    case SortState.datetimeAsc: return View(await n.OrderBy(s => s.st.datetime).ToListAsync());
                    case SortState.datetimeDesc: return View(await n.OrderByDescending(s => s.st.datetime).ToListAsync());
                    case SortState.user_idAsc: return View(await n.OrderBy(s => s.st.user_id).ToListAsync());
                    case SortState.user_idDesc: return View(await n.OrderByDescending(s => s.st.user_id).ToListAsync());
                    case SortState.attributeAsc: return View(await n.OrderBy(s => s.st.attribute).ToListAsync());
                    case SortState.attributeDesc: return View(await n.OrderByDescending(s => s.st.attribute).ToListAsync());
                    case SortState.actionAsc: return View(await n.OrderBy(s => s.st.action).ToListAsync());
                    case SortState.actionDesc: return View(await n.OrderByDescending(s => s.st.action).ToListAsync());
                    case SortState.tableAsc: return View(await n.OrderBy(s => s.st.table).ToListAsync());
                    case SortState.tableDesc: return View(await n.OrderByDescending(s => s.st.table).ToListAsync());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View(await n.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> DeleteLogg(int? id)
        {
            if (id != null)
            {
                loggs? sf = await db.loggs.FirstOrDefaultAsync(p => p.id == id);
                if (sf != null)
                {               
                    db.loggs.Remove(sf);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Loggs");
                }
            }
            return NotFound();
        }
    }
}
