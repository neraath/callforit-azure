using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using callforit.Models;

namespace callforit.Controllers
{
    public class ConferenceController : Controller
    {
        //
        // GET: /Conference/

        public ActionResult Index()
        {
            System.Diagnostics.Trace.TraceError("OMG THIS IS BAD.");
            return View(Conferences.ToList());
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
            var existingConference = FindById(id);
            if (existingConference == null) return HttpNotFound();

            Conferences.Remove(existingConference);
            return RedirectToAction("Index");
        }

        #region Private Data Access

        private static IList<Conference> Conferences = new List<Conference>();

        private Conference FindById(int id)
        {
            return Conferences.SingleOrDefault(x => x.Id.Equals(id));
        }

        private void AddOrReplaceConference(Conference conference)
        {
            if (conference.Id == default(int))
            {
                conference.Id = Conferences.Count + 1;
            }

            var existingConference = FindById(conference.Id);
            if (existingConference != default(Conference)) Conferences.Remove(existingConference);

            Conferences.Add(conference);
        }

        #endregion
    }
}
