﻿using CustomPost2023.Data.Calculations;
using CustomPost2023.Data.Models;
using CustomPost2023.Data.Calculations;
using CustomPost2023.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;

namespace CustomPost2023.Controllers.Staff
{
    public class StaffNativeController : Controller
    {
        ApplicationContext db;
        private loggs logg = new loggs();
        public static string meanBefForLogg;
        public StaffNativeController(ApplicationContext context)
        {
            db = context;
        }
        private StaffViewModel DetectStaff(int id)
        {
            var n = db.staff.Where(p => p.id.Equals(id)).FirstOrDefault();
            StaffViewModel model = new StaffViewModel();
            model.staff = n;
            model.custom_post = db.custom_post.Where(p => p.customs_post_id.Equals(n.custom_post_id)).FirstOrDefault();
            model.applications = new List<ApplicationViewModel>();
            List<application> curApp = db.application.Where(p => p.staff_id.Equals(n.id)).ToList();

            foreach (application item in curApp)
            {
                ApplicationViewModel tmp = new ApplicationViewModel();
                tmp.app_app = db.application.FirstOrDefault(p => p.id.Equals(item.id));
                tmp.app_staff = db.staff.FirstOrDefault(p => p.id.Equals(item.staff_id));
                tmp.app_status = db.status.FirstOrDefault(p => p.id.Equals(item.status_id));
                tmp.app_product = db.product.FirstOrDefault(p => p.product_id.Equals(item.product_id));
                tmp.app_custom_post = db.custom_post.FirstOrDefault(p => p.customs_post_id.Equals(item.custom_post_id));
                tmp.app_user = db.user.FirstOrDefault(p => p.user_id.Equals(item.user_id));
                tmp.app_export_country = db.export_countries.FirstOrDefault(p => p.id.Equals(item.staff_id));
                tmp.app_history = db.history.FirstOrDefault(p => p.application_id.Equals(item.id));
                tmp.app_vehicle_type = db.vehicle_type.FirstOrDefault(p => p.vehicle_type_id.Equals(tmp.app_custom_post.fk_vehicle_id));
                tmp.app_prod_type = db.product_type.FirstOrDefault(p => p.type_product_id.Equals(tmp.app_product.fk_type_product_id));
                model.applications.Add(tmp);
            }
            return model;
        }
        private ApplicationViewModel DetectApplication(int id)
        {
            ApplicationViewModel model = new ApplicationViewModel();
            application curApp = db.application.FirstOrDefault(p => p.id.Equals(id));
            model.app_app = curApp;
            model.app_status = db.status.FirstOrDefault(p => p.id.Equals(curApp.status_id));
            model.app_product = db.product.FirstOrDefault(p => p.product_id.Equals(curApp.product_id));
            model.app_custom_post = db.custom_post.FirstOrDefault(p => p.customs_post_id.Equals(curApp.custom_post_id));
            model.app_staff = db.staff.FirstOrDefault(p => p.id.Equals(curApp.staff_id));
            model.app_user = db.user.FirstOrDefault(p => p.user_id.Equals(curApp.user_id));
            model.app_export_country = db.export_countries.FirstOrDefault(p => p.id.Equals(curApp.export_country_id));
            model.app_prod_type = db.product_type.FirstOrDefault(p => p.type_product_id.Equals(model.app_product.fk_type_product_id));
            model.app_history = db.history.FirstOrDefault(p => p.application_id.Equals(curApp.id));
            model.app_vehicle_type = db.vehicle_type.FirstOrDefault(p => p.vehicle_type_id.Equals(model.app_custom_post.fk_vehicle_id));
            return model;
        }
        public IActionResult Index(int id)
        {
            return View(DetectStaff(id));
        }
        [HttpPost]
        public async Task<IActionResult> EditPassword(staff us)
        {
            if (us != null)
            {
                //logg.SendLogg(db, 2, "user", "whole record", $"{us.user_id}|{us.user_name}|{us.login}|{us.password}|{us.role}", "NULL");
                db.staff.Update(us);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult ViewTask(int id)
        {

            return View(DetectApplication(id));
        }
        [HttpPost]
        public IActionResult ViewResult(int id, string oto, string STO, string PGM, string PKPvGM, string IOHT, string conclusion)
        {

            ApplicationViewModel model = DetectApplication(id);

            AppPar temp = new AppPar();
            temp.getHours(oto, STO, PGM, PKPvGM, IOHT, model.app_product.mass, model.app_product.price);

            ResultViewModel resultViewModel = new ResultViewModel();
            resultViewModel.applicationViewModel = model;
            resultViewModel.appPar = temp;
            resultViewModel.conclusion = conclusion;

            return View(resultViewModel);
        }
        [HttpPost]
        public IActionResult DoneResult(int id, string conclusion, double cusTime, double cusPrise, string radioGroup)
        {

            ApplicationViewModel appModel = DetectApplication(id);

            history newHi = appModel.app_history;
            newHi.conclusion = conclusion;
            newHi.customs_clearance_time = cusTime;
            newHi.customs_clearance_cost = cusPrise;

            db.history.Update(newHi);
            db.SaveChanges();
            application newApp = appModel.app_app;

            if (int.Parse(radioGroup)==1)
            {           
                newApp.status_id = 5;
            }
            else
            {
                newApp.status_id = 4;
            }

            db.application.Update(newApp);
            db.SaveChanges();


            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult ViewAllTasks(int id)
        {
            return View(DetectStaff(id));
        }
    }
}
