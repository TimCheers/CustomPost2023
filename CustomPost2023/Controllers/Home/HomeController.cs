using CustomPost2023.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomPost2023.Controllers.Home
{
    public class HomeController : Controller
    {
        ApplicationContext db = new ApplicationContext();

        public IActionResult Index(string username, string password)
        {
            if (username != null)
            {
                if (db.user.Where(p => p.login.Equals(username)).ToList().Count() > 0)
                {
                    user curUs = db.user.FirstOrDefault(p => p.login.Equals(username));
                    if (curUs.password == password)
                    {
                        TempData["login"] = username;
                        TempData["password"] = password;
                        if (curUs.role == "user")
                            return RedirectToAction("Index", "UserNative", new { id = curUs.user_id });
                        if (curUs.role == "admin")
                            return RedirectToAction("Loggs", "Admin");
                    }

                    return View();
                }
                else
                {
                    staff curStaff = db.staff.FirstOrDefault(p => p.email.Equals(username));
                    if (curStaff.password == password)
                    {
                        TempData["login"] = username;
                        TempData["password"] = password;
                        return RedirectToAction("Index", "StaffNative", new { id = curStaff.id });
                    }

                    return View();
                }
            }

            return View();
        }
    }
}