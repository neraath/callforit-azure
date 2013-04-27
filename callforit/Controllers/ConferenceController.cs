﻿using System.Collections.Generic;
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
            var conference = Conferences.SingleOrDefault(x => x.Id.Equals(id));
            if (conference == null)
            {
                return HttpNotFound();
            }
            return View(conference);
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
            var conference = Conferences.SingleOrDefault(x => x.Id.Equals(id));
            if (conference == null)
            {
                return HttpNotFound();
            }
            return View(conference);
        }

        //
        // POST: /Conference/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Conference conference)
        {
            var existingConference = Conferences.SingleOrDefault(x => x.Id.Equals(id));
            if (existingConference == null)
            {
                return HttpNotFound();
            }

            if (ModelState.IsValid)
            {
                Conferences.Remove(existingConference);
                Conferences.Add(conference);
                return RedirectToAction("Index");
            }

            return View(conference);
        }

        //
        // GET: /Conference/Delete/5

        public ActionResult Delete(int id)
        {
            var existingConference = Conferences.SingleOrDefault(x => x.Id.Equals(id));
            if (existingConference == null)
            {
                return HttpNotFound();
            }

            return View(existingConference);
        }

        //
        // POST: /Conference/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var existingConference = Conferences.SingleOrDefault(x => x.Id.Equals(id));
            if (existingConference == null)
            {
                return HttpNotFound();
            }

            Conferences.Remove(existingConference);
            return RedirectToAction("Index");
        }
    }
}
