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
    [Authorize(Roles = "Manager")]
    public class UsersDeviceTokensController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UsersDeviceTokens
        public ActionResult Index()
        {
            return View(db.UsersDeviceTokens.OrderByDescending(a=>a.CreationDate).ToList());
        }

        // GET: UsersDeviceTokens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersDeviceTokens usersDeviceTokens = db.UsersDeviceTokens.Find(id);
            if (usersDeviceTokens == null)
            {
                return HttpNotFound();
            }
            return View(usersDeviceTokens);
        }

        // GET: UsersDeviceTokens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersDeviceTokens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,token,UserId,CreationDate,LastModificationDate,Creator,Modifier,AttachmentId")] UsersDeviceTokens usersDeviceTokens)
        {
            if (ModelState.IsValid)
            {
                db.UsersDeviceTokens.Add(usersDeviceTokens);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usersDeviceTokens);
        }

        // GET: UsersDeviceTokens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersDeviceTokens usersDeviceTokens = db.UsersDeviceTokens.Find(id);
            if (usersDeviceTokens == null)
            {
                return HttpNotFound();
            }
            return View(usersDeviceTokens);
        }

        // POST: UsersDeviceTokens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,token,UserId,CreationDate,LastModificationDate,Creator,Modifier,AttachmentId")] UsersDeviceTokens usersDeviceTokens)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usersDeviceTokens).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usersDeviceTokens);
        }

        // GET: UsersDeviceTokens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersDeviceTokens usersDeviceTokens = db.UsersDeviceTokens.Find(id);
            if (usersDeviceTokens == null)
            {
                return HttpNotFound();
            }
            return View(usersDeviceTokens);
        }

        // POST: UsersDeviceTokens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsersDeviceTokens usersDeviceTokens = db.UsersDeviceTokens.Find(id);
            db.UsersDeviceTokens.Remove(usersDeviceTokens);
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
