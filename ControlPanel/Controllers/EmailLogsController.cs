using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControlPanel.Models;

namespace ControlPanel.Controllers
{
    [Authorize]
    public class EmailLogsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EmailLogs
        public ActionResult Index()
        {
            return View(db.EmailLogs.OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: EmailLogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailLog emailLog = db.EmailLogs.Find(id);
            if (emailLog == null)
            {
                return HttpNotFound();
            }
            return View(emailLog);
        }

        // GET: EmailLogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmailLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Sender,Receiver,Subject,Body,CreationDate,LastModificationDate,Creator,Modifier,AttachmentId")] EmailLog emailLog)
        {
            if (ModelState.IsValid)
            {
                db.EmailLogs.Add(emailLog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(emailLog);
        }

        // GET: EmailLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailLog emailLog = db.EmailLogs.Find(id);
            if (emailLog == null)
            {
                return HttpNotFound();
            }
            return View(emailLog);
        }

        // POST: EmailLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Sender,Receiver,Subject,Body,CreationDate,LastModificationDate,Creator,Modifier,AttachmentId")] EmailLog emailLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emailLog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emailLog);
        }

        // GET: EmailLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailLog emailLog = db.EmailLogs.Find(id);
            if (emailLog == null)
            {
                return HttpNotFound();
            }
            return View(emailLog);
        }

        // POST: EmailLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmailLog emailLog = db.EmailLogs.Find(id);
            db.EmailLogs.Remove(emailLog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
