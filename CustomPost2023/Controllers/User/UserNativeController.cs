using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomPost2023.Controllers.User
{
    public class UserNativeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
    }
}
