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
        
        public enum SortState
        {
            IdAsc,
            IdDesc,
            nameAsc,
            nameDesc,
            massAsc,
            massDesc,
            countAsc,
            countDesc,
            priceAsc,
            priceDesc,
            typeAsc,
            typeDesc,
        }
        public ProductController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index(string prodStr, string massStr, string countStr, string priceStr, string typeStr, SortState sortOrder = SortState.IdAsc)
        {
            IQueryable<product>? myLogg = db.product;
            var n = from st in db.Set<product>()
                    select new { st };
            ViewData["IdSort"] = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            ViewData["nameSort"] = sortOrder == SortState.nameAsc ? SortState.nameDesc : SortState.nameAsc;
            ViewData["massSort"] = sortOrder == SortState.massAsc ? SortState.massDesc : SortState.massAsc;
            ViewData["countSort"] = sortOrder == SortState.countAsc ? SortState.countDesc : SortState.countAsc;
            ViewData["priceSort"] = sortOrder == SortState.priceAsc ? SortState.priceDesc : SortState.priceAsc;
            ViewData["typeSort"] = sortOrder == SortState.typeAsc ? SortState.typeDesc : SortState.typeAsc;
            if (!string.IsNullOrEmpty(prodStr))
            {
                n = n.Where(p => p.st.product_title.Contains(prodStr));
            }
            if (!string.IsNullOrEmpty(massStr))
            {
                n = n.Where(p => p.st.mass.Equals(int.Parse(massStr)));
            }
            if (!string.IsNullOrEmpty(countStr))
            {
                n = n.Where(p => p.st.quantity.Equals(int.Parse(countStr)));
            }
            if (!string.IsNullOrEmpty(priceStr))
            {
                n = n.Where(p => p.st.price.Equals(int.Parse(priceStr)));
            }
            
            if (!string.IsNullOrEmpty(typeStr))
            {
                n = n.Where(p => p.st.fk_type_product_id.Equals(int.Parse(typeStr)));
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
                    case SortState.IdAsc: return View(await n.OrderBy(s => s.st.product_id).ToListAsync());
                    case SortState.IdDesc: return View(await n.OrderByDescending(s => s.st.product_id).ToListAsync());
                    case SortState.nameAsc: return View(await n.OrderBy(s => s.st.product_title).ToListAsync());
                    case SortState.nameDesc: return View(await n.OrderByDescending(s => s.st.product_title).ToListAsync());
                    case SortState.massAsc: return View(await n.OrderBy(s => s.st.mass).ToListAsync());
                    case SortState.massDesc: return View(await n.OrderByDescending(s => s.st.mass).ToListAsync());
                    case SortState.countAsc: return View(await n.OrderBy(s => s.st.quantity).ToListAsync());
                    case SortState.countDesc: return View(await n.OrderByDescending(s => s.st.quantity).ToListAsync());
                    case SortState.priceAsc: return View(await n.OrderBy(s => s.st.price).ToListAsync());
                    case SortState.priceDesc: return View(await n.OrderByDescending(s => s.st.price).ToListAsync());
                    case SortState.typeAsc: return View(await n.OrderBy(s => s.st.fk_type_product_id).ToListAsync());
                    case SortState.typeDesc: return View(await n.OrderByDescending(s => s.st.fk_type_product_id).ToListAsync());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return View(await n.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Create(product pr)
        {
            db.product.Add(pr);
            logg.SendLogg(db, 1, "product", "whole record", "NULL", $"{pr.product_id}|{pr.product_title}|{pr.mass}|{pr.fk_type_product_id}" +
                $"|{pr.price}|{pr.quantity}|{pr.description}|{pr.characteristics}");
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
                    logg.SendLogg(db, 2, "product", "whole record", $"{pr.product_id}|{pr.product_title}|{pr.mass}|{pr.fk_type_product_id}" +
                        $"|{pr.price}|{pr.quantity}|{pr.description}|{pr.characteristics}", "NULL");
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
                    meanBefForLogg = $"{pr.product_id}|{pr.product_title}|{pr.mass}|{pr.fk_type_product_id}|{pr.price}|{pr.quantity}|{pr.description}|{pr.characteristics}";
                    return View(pr);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(product pr)
        {
            db.product.Update(pr);
            logg.SendLogg(db, 3, "product", "whole record", meanBefForLogg, $"{pr.product_id}|{pr.product_title}|{pr.mass}|{pr.fk_type_product_id}" +
                $"|{pr.price}|{pr.quantity}|{pr.description}|{pr.characteristics}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
