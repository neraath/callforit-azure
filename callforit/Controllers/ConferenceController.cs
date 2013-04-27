using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
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
                ClaimsPrincipal principal = ClaimsPrincipal.Current;
                var fullname = string.Format("{0} {1}", principal.FindFirst(ClaimTypes.GivenName).Value,
                                             principal.FindFirst(ClaimTypes.Surname).Value);
                conference.CreatorName = fullname;
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
            DeleteConference(id);
            return RedirectToAction("Index");
        }

        #region Private Data Access

        private Conference FindById(int id)
        {
            using (var context = new EFContext())
            {
                return context.Conferences.SingleOrDefault(x => x.Id.Equals(id));
            }
        }

        private void SendEmailNotificationAboutNewConference(Conference conference)
        {
            var namespaceManager =
                NamespaceManager.CreateFromConnectionString(CloudConfigurationManager.GetSetting("CallForItServiceBus"));

            if (namespaceManager.QueueExists("NewConferenceQueue") == false)
                namespaceManager.CreateQueue("NewConferenceQueue");

            var queueClient =
                QueueClient.CreateFromConnectionString(CloudConfigurationManager.GetSetting("CallForItServiceBus"),
                                                       "NewConferenceQueue");

            var notification = new EmailNotification();

            notification.ToEmailAddress = "improvingwa@gmail.com";
            notification.FromEmailAddress = "conferences@callforit.com";
            notification.Subject = "New Conference!";
            notification.Body = String.Format("Great news! {0} has been scheduled for {1}.", conference.Name,
                                              conference.StartDate);

            queueClient.Send(new BrokeredMessage(notification));
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

                    SendEmailNotificationAboutNewConference(conference);
                }
                else
                {
                    // Update Scenario
                    conference = context.Conferences.Attach(conference);
                    context.Entry(conference).State = System.Data.EntityState.Modified;
                    context.SaveChanges();
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
        private void DeleteConference(int id)
        {
            using (var context = new EFContext())
            {
                var conf = context.Conferences.SingleOrDefault(e => e.Id == id);
                context.Conferences.Remove(conf);
                context.SaveChanges();
            }
        }
        #endregion
    }
}
