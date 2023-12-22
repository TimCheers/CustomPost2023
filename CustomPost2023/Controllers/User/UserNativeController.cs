using CustomPost2023.Data.Models;
using CustomPost2023.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomPost2023.Data.Calculations;

namespace CustomPost2023.Controllers.User
{
    public class UserNativeController : Controller
    {
        ApplicationContext db = new ApplicationContext();
        private loggs logg = new loggs();

        private UserViewModel DetectUser(int id)
        {
            var n = db.user.Where(p => p.user_id.Equals(id)).FirstOrDefault();
            UserViewModel model = new UserViewModel();
            model.User = n;
            model.applications = new List<ApplicationViewModel>();
            List<application> curApp = db.application.Where(p => p.user_id.Equals(n.user_id)).ToList();

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
                tmp.app_vehicle_type =
                    db.vehicle_type.FirstOrDefault(p => p.vehicle_type_id.Equals(tmp.app_custom_post.fk_vehicle_id));
                tmp.app_prod_type =
                    db.product_type.FirstOrDefault(p => p.type_product_id.Equals(tmp.app_product.fk_type_product_id));
                model.applications.Add(tmp);
            }

            return model;
        }

        public ActionResult Index(int id)
        {
            return View(DetectUser(id));
        }

        [HttpPost]
        public async Task<IActionResult> ApplicationCreate(int id, int CPost)
        {
            UserViewModel model = DetectUser(id);
            model.ProductTypes = db.product_type.ToList();
            model.VehicleTypes = db.vehicle_type.ToList();
            model.ExportCountriesList = db.export_countries.ToList();
            model.new_application = new ApplicationViewModel();
            model.new_application.app_custom_post = db.custom_post.FirstOrDefault(p => p.customs_post_id.Equals(CPost));
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ApplicationCreateDone(UserViewModel model)
        {
            application newApplication = new application();
            newApplication.product_id = CreateProduct(model.new_application.app_product);
            newApplication.status_id = 3;
            newApplication.export_country_id = db.export_countries.FirstOrDefault(p =>
                p.country_title.Equals(model.new_application.app_export_country.country_title)).id;
            newApplication.custom_post_id = db.custom_post
                .FirstOrDefault(p => p.customs_post_id.Equals(model.new_application.app_custom_post.customs_post_id))
                .customs_post_id;
            newApplication.user_id = model.User.user_id;
            newApplication.staff_id = AppToStaff.DisplayApplicationCountByStaffId(db, newApplication.custom_post_id);
            db.application.Add(newApplication);
            db.SaveChanges();
            history hi = new history();
            hi.application_id = newApplication.id;
            hi.custom_date = DateOnly.FromDateTime(DateTime.Now);
            hi.conclusion = "";
            db.history.Add(hi);
            db.SaveChanges();
            // CreateApp(newApplication);
            // CreateHistory(newApplication);
            return RedirectToAction("Index");
        }

        private void CreateApp(application newApplication)
        {
            
        }

        private void CreateHistory(application newApplication)
        {
            
        }

        private int CreateProduct(product product)
        {
            db.product.Add(product);
            logg.SendLogg(db, 1, "product", "whole record", "NULL",
                $"{product.product_id}|{product.product_title}|{product.mass}|{product.fk_type_product_id}" +
                $"|{product.price}|{product.quantity}|{product.description}|{product.characteristics}");
            db.SaveChanges();
            return product.product_id;
        }

        public IActionResult CustomsPostSelection(string nameStr, string locStr, string thrStr, string vehStr, int id)
        {
            var model = from ap in db.Set<custom_post>()
                join pr in db.Set<vehicle_type>() on ap.fk_vehicle_id equals pr.vehicle_type_id
                join us in db.Set<user>() on id equals us.user_id
                select new { ap, pr, us };
            if (!string.IsNullOrEmpty(nameStr))
            {
                model = model.Where(p => p.ap.customs_post_title.Contains(nameStr));
            }
            if (!string.IsNullOrEmpty(locStr))
            {
                model = model.Where(p => p.ap.location.Contains(locStr));
            }
            if (!string.IsNullOrEmpty(thrStr))
            {
                model = model.Where(p => p.ap.throughput.Equals(int.Parse(thrStr)));
            }
            if (!string.IsNullOrEmpty(vehStr))
            {
                model = model.Where(p => p.ap.fk_vehicle_id.Equals(int.Parse(vehStr)));
            }
            return View(model);
        }
    }
}