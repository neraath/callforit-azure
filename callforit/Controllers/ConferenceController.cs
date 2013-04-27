using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using callforit.Models;

namespace callforit.Controllers
{
    public class ConferenceController : Controller
    {
        public static IList<Conference> Conferences = new List<Conference>();

        //
        // GET: /Conference/

        public ActionResult Index()
        {
            return View(Conferences.ToList());
        }

        //
        // GET: /Conference/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Conference/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Conference/Create

        [HttpPost]
        public ActionResult Create(Conference conference)
        {
            if (ModelState.IsValid)
            {
                conference.Id = Conferences.Count + 1;
                Conferences.Add(conference);
                return RedirectToAction("Index");
            }

            return View(conference);
        }

        //
        // GET: /Conference/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Conference/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Conference/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Conference/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
