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
    public class ContactusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Contactus
        public ActionResult Index()
        {
            return View(db.Contactus.OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: Contactus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactus contactus = db.Contactus.Find(id);
            if (contactus == null)
            {
                return HttpNotFound();
            }
            return View(contactus);
        }

        // GET: Contactus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contactus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Title,Text,Email,phoneNumber,CreationDate,LastModificationDate,Creator,Modifier,AttachmentId")] Contactus contactus)
        {
            if (ModelState.IsValid)
            {
                db.Contactus.Add(contactus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactus);
        }

        // GET: Contactus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactus contactus = db.Contactus.Find(id);
            if (contactus == null)
            {
                return HttpNotFound();
            }
            return View(contactus);
        }

        // POST: Contactus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Title,Text,Email,phoneNumber,CreationDate,LastModificationDate,Creator,Modifier,AttachmentId")] Contactus contactus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactus);
        }

        // GET: Contactus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactus contactus = db.Contactus.Find(id);
            if (contactus == null)
            {
                return HttpNotFound();
            }
            return View(contactus);
        }

        // POST: Contactus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contactus contactus = db.Contactus.Find(id);
            db.Contactus.Remove(contactus);
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
