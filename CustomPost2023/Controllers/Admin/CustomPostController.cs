using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomPost2023.Controllers.Admin
{
    public class CustomPostController : Controller
    {
        ApplicationContext db;
        private loggs logg = new loggs();
        public static string meanBefForLogg;
        public CustomPostController(ApplicationContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            var model = db.custom_post;
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(custom_post cp)
        {
            db.custom_post.Add(cp);
            logg.SendLogg(db, 1, "custom_post", "whole record", "NULL", $"{cp.customs_post_id}|{cp.customs_post_title}|{cp.location}|{cp.throughput}|{cp.fk_vehicle_id}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                custom_post? cp = await db.custom_post.FirstOrDefaultAsync(p => p.customs_post_id == id);
                if (cp != null)
                {
                    logg.SendLogg(db, 2, "custom_post", "whole record", $"{cp.customs_post_id}|{cp.customs_post_title}|{cp.location}|{cp.throughput}|{cp.fk_vehicle_id}", "NULL");
                    db.custom_post.Remove(cp);
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
                custom_post? cp = await db.custom_post.FirstOrDefaultAsync(p => p.customs_post_id == id);
                if (cp != null)
                {
                    meanBefForLogg = $"{cp.customs_post_id}|{cp.customs_post_title}|{cp.location}|{cp.throughput}|{cp.fk_vehicle_id}";
                    return View(cp);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(custom_post cp)
        {
            db.custom_post.Update(cp);
            logg.SendLogg(db, 3, "custom_post", "whole record", meanBefForLogg, $"{cp.customs_post_id}|{cp.customs_post_title}|{cp.location}|{cp.throughput}|{cp.fk_vehicle_id}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
