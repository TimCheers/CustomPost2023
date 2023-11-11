using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomPost2023.Controllers.Admin
{
    public class ProductController : Controller
    {
        ApplicationContext db;
        private loggs logg = new loggs();
        public ProductController(ApplicationContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            var model = db.products;
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(product pr)
        {
            db.products.Add(pr);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                product? pr = await db.products.FirstOrDefaultAsync(p => p.product_id == id);
                if (pr != null)
                {
                    db.products.Remove(pr);
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
                product? pr = await db.products.FirstOrDefaultAsync(p => p.product_id == id);
                if (pr != null) return View(pr);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(product pr)
        {
            db.products.Update(pr);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
