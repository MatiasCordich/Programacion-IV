using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Containers.WEB.Controllers
{
    public class ContenedorController : Controller
    {
        // GET: ContenedorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ContenedorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContenedorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContenedorController/Create
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

        // GET: ContenedorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContenedorController/Edit/5
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

        // GET: ContenedorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContenedorController/Delete/5
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
