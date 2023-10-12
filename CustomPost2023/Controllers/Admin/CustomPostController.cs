using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomPost2023.Controllers.Admin
{
    public class CustomPostController : Controller
    {
        // GET: CustomsPostsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomsPostsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomsPostsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomsPostsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomsPostsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomsPostsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomsPostsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomsPostsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
