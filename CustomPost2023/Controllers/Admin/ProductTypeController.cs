using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomPost2023.Controllers.Admin
{
    public class ProductTypeController : Controller
    {
        ApplicationContext db;
        public ProductTypeController(ApplicationContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            var model = db.product_types;
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(product_type pt)
        {
            db.product_types.Add(pt);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                product_type? pt = await db.product_types.FirstOrDefaultAsync(p => p.type_product_id == id);
                if (pt != null)
                {
                    db.product_types.Remove(pt);
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
                product_type? pt = await db.product_types.FirstOrDefaultAsync(p => p.type_product_id == id);
                if (pt != null) return View(pt);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(product_type pt)
        {
            db.product_types.Update(pt);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
