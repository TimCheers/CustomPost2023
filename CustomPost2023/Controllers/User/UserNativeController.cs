using CustomPost2023.Data.Models;
using CustomPost2023.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomPost2023.Controllers.User
{
    public class UserNativeController : Controller
    {
        ApplicationContext db = new ApplicationContext();
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
                tmp.app_vehicle_type = db.vehicle_type.FirstOrDefault(p => p.vehicle_type_id.Equals(tmp.app_custom_post.fk_vehicle_id));
                tmp.app_prod_type = db.product_type.FirstOrDefault(p => p.type_product_id.Equals(tmp.app_product.fk_type_product_id));
                model.applications.Add(tmp);
            }
            return model;
        }
        public ActionResult Index(int id)
        {
            return View(DetectUser(id));
        }
        [HttpPost]
        public IActionResult ApplicationCreate(int id)
        {
            UserViewModel newModel = DetectUser(id);
            newModel.new_application = new ApplicationViewModel();
            return View(newModel);
        }
    }
}
