using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CustomPost2023.Controllers.Admin
{
    public class ProductTypeController : Controller
    {
        ApplicationContext db;
        private loggs logg = new loggs();
        public static string meanBefForLogg;
        public ProductTypeController(ApplicationContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            var model = db.product_type;
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(product_type pt)
        {
            db.product_type.Add(pt);
            logg.SendLogg(db, 1, "product_type", "whole record", "NULL", $"{pt.type_product_id}|{pt.type_product_title}|{pt.customs_clearance_coefficient}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                product_type? pt = await db.product_type.FirstOrDefaultAsync(p => p.type_product_id == id);
                if (pt != null)
                {
                    db.product_type.Remove(pt);
                    logg.SendLogg(db, 2, "product_type", "whole record", $"{pt.type_product_id}|{pt.type_product_title}|{pt.customs_clearance_coefficient}", "NULL");
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
                product_type? pt = await db.product_type.FirstOrDefaultAsync(p => p.type_product_id == id);
                if (pt != null) 
                {
                    meanBefForLogg = $"{pt.type_product_id}|{pt.type_product_title}|{pt.customs_clearance_coefficient}";
                    return View(pt); 
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(product_type pt)
        {
            db.product_type.Update(pt);
            logg.SendLogg(db, 3, "product_type", "whole record", meanBefForLogg, $"{pt.type_product_id}|{pt.type_product_title}|{pt.customs_clearance_coefficient}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
