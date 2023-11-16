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
        public static string meanBefForLogg;
        public ProductController(ApplicationContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            var model = db.product;
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(product pr)
        {
            db.product.Add(pr);
            logg.SendLogg(db, 1, "product", "whole record", "NULL", $"{pr.product_id}|{pr.product_title}|{pr.mass}|{pr.fk_type_product_id}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                product? pr = await db.product.FirstOrDefaultAsync(p => p.product_id == id);
                if (pr != null)
                {
                    db.product.Remove(pr);
                    logg.SendLogg(db, 2, "product", "whole record", $"{pr.product_id}|{pr.product_title}|{pr.mass}|{pr.fk_type_product_id}", "NULL");
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
                product? pr = await db.product.FirstOrDefaultAsync(p => p.product_id == id);
                if (pr != null)
                {
                    meanBefForLogg = $"{pr.product_id}|{pr.product_title}|{pr.mass}|{pr.fk_type_product_id}";
                    return View(pr);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(product pr)
        {
            db.product.Update(pr);
            logg.SendLogg(db, 3, "product", "whole record", meanBefForLogg, $"{pr.product_id}|{pr.product_title}|{pr.mass}|{pr.fk_type_product_id}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
