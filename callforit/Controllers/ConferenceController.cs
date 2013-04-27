using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using callforit.Models;
using System;

namespace callforit.Controllers
{
    public class ConferenceController : Controller
    {
        //
        // GET: /Conference/

        public ActionResult Index()
        {
            System.Diagnostics.Trace.TraceError("ZOMG this is bad!");
            return View(GetAll());
        }

        //
        // GET: /Conference/Details/5

        public ActionResult Details(int id)
        {
            var existingConference = FindById(id);
            if (existingConference == null) return HttpNotFound();
            return View(existingConference);
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
                AddOrReplaceConference(conference);
                return RedirectToAction("Index");
            }

            return View(conference);
        }

        //
        // GET: /Conference/Edit/5

        public ActionResult Edit(int id)
        {
            var existingConference = FindById(id);
            if (existingConference == null) return HttpNotFound();
            return View(existingConference);
        }

        //
        // POST: /Conference/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Conference conference)
        {
            var existingConference = FindById(id);
            if (existingConference == null) return HttpNotFound();

            if (ModelState.IsValid)
            {
                AddOrReplaceConference(conference);
                return RedirectToAction("Index");
            }

            return View(conference);
        }

        //
        // GET: /Conference/Delete/5

        public ActionResult Delete(int id)
        {
            var existingConference = FindById(id);
            if (existingConference == null) return HttpNotFound();
            return View(existingConference);
        }

        //
        // POST: /Conference/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            return null;
            //var existingConference = FindById(id);
            //if (existingConference == null) return HttpNotFound();

            //Conferences.Remove(existingConference);
            //return RedirectToAction("Index");
        }

        #region Private Data Access

        private Conference FindById(int id)
        {
            using (var context = new EFContext())
            {
                return context.Conferences.SingleOrDefault(x => x.Id.Equals(id));
            }
        }

        private void AddOrReplaceConference(Conference conference)
        {
            using (var context = new EFContext())
            {
                if (conference.Id == default(int))
                {
                    // Add Scenario
                    context.Conferences.Add(conference);
                    context.SaveChanges();
                }
                else
                {
                    // Update Scenario
                    throw new Exception("Tim you haven't made that work yet");
                }
            }
        }

        private IEnumerable<Conference> GetAll()
        {
            using (var context = new EFContext())
            {
                return context.Conferences.ToList();
            }
        }
        #endregion
    }
}
