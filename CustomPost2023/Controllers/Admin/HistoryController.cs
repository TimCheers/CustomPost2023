﻿using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomPost2023.Controllers.Admin
{
    public class HistoryController : Controller
    {
        ApplicationContext db;
        public HistoryController(ApplicationContext context)
        {
            db = context;
        }
        public ActionResult Index()
        {
            var model = db.historys;
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(history hi)
        {
            db.historys.Add(hi);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                history? hi = await db.historys.FirstOrDefaultAsync(p => p.history_id == id);
                if (hi != null)
                {
                    db.historys.Remove(hi);
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
                history? hi = await db.historys.FirstOrDefaultAsync(p => p.history_id == id);
                if (hi != null) return View(hi);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(history hi)
        {
            db.historys.Update(hi);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
