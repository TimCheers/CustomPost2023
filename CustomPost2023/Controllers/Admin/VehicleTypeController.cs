using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomPost2023.Controllers.Admin
{
    public class VehicleTypeController : Controller
    {
        ApplicationContext db;
        private loggs logg = new loggs();
        public static string meanBefForLogg;
        public VehicleTypeController(ApplicationContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            var model = db.vehicle_type;
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(vehicle_type vt)
        {
            db.vehicle_type.Add(vt);
            logg.SendLogg(db, 1, "vehicle_type", "whole record", "NULL", $"{vt.vehicle_type_id}|{vt.vehicle_type_title}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                vehicle_type? vt = await db.vehicle_type.FirstOrDefaultAsync(p => p.vehicle_type_id == id);
                if (vt != null)
                {
                    db.vehicle_type.Remove(vt);
                    logg.SendLogg(db, 2, "vehicle_type", "whole record", $"{vt.vehicle_type_id}|{vt.vehicle_type_title}", "NULL");
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
                vehicle_type? vt = await db.vehicle_type.FirstOrDefaultAsync(p => p.vehicle_type_id == id);
                if (vt != null)
                {
                    meanBefForLogg = $"{vt.vehicle_type_id}|{vt.vehicle_type_title}";
                    return View(vt);
                }
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(vehicle_type vt)
        {
            db.vehicle_type.Update(vt);
            logg.SendLogg(db, 3, "vehicle_type", "whole record", meanBefForLogg, $"{vt.vehicle_type_id}|{vt.vehicle_type_title}");
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
