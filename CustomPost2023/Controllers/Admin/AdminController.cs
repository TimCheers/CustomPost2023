using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomPost2023.Controllers.Admin
{
    public class AdminController : Controller
    {
        ApplicationContext db;

        public AdminController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Loggs()
        {
            return View(await db.loggs.ToListAsync());
        }
    }
}
