using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomPost2023.Controllers.Admin
{
    public class VehicleTypeController : Controller
    {
        // GET: VehicleTypeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VehicleTypeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VehicleTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehicleTypeController/Create
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

        // GET: VehicleTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VehicleTypeController/Edit/5
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

        // GET: VehicleTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VehicleTypeController/Delete/5
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
