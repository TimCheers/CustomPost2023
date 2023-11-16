using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomPost2023.Controllers.Admin
{
    public class AdminController : Controller
    {
        ApplicationContext db;
        public enum SortState
        {
            IdAsc,    // по имени по возрастанию
            IdDesc,   // по имени по убыванию
            AgeAsc, // по возрасту по возрастанию
            AgeDesc,    // по возрасту по убыванию
            CompanyAsc, // по компании по возрастанию
            CompanyDesc // по компании по убыванию
        }
        public AdminController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Loggs(SortState sortOrder = SortState.IdAsc)
        {
            IQueryable<loggs>? myLogg = db.loggs;
            ViewData["IdSort"] = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            myLogg = sortOrder switch
            {
                SortState.IdAsc => myLogg.OrderBy(s => s.id),
                SortState.IdDesc => myLogg.OrderByDescending(s => s.id)
            };
            return View(await myLogg.AsNoTracking().ToListAsync());
        }
    }
}
