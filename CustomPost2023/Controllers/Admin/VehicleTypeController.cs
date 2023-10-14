using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomPost2023.Controllers.Admin
{
    public class VehicleTypeController : Controller
    {
        ApplicationContext db;
        public VehicleTypeController(ApplicationContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            var model = db.vehicle_types;
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(vehicle_type vt)
        {
            db.vehicle_types.Add(vt);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                vehicle_type? vt = await db.vehicle_types.FirstOrDefaultAsync(p => p.vehicle_type_id == id);
                if (vt != null)
                {
                    db.vehicle_types.Remove(vt);
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
                vehicle_type? vt = await db.vehicle_types.FirstOrDefaultAsync(p => p.vehicle_type_id == id);
                if (vt != null) return View(vt);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(user us)
        {
            db.users.Update(us);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
