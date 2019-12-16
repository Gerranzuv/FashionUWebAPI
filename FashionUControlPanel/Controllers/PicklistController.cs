using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Validation;
using System.IO;
using FashionUControlPanel.Models;

namespace najjar.biz.Controllers.Systemutil
{
    public class PicklistController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Picklist/
        public ActionResult Index()
        {
            fillUserData();

            return View(db.Picklists.ToList());
        }

        // GET: /Picklist/Details/5
        public ActionResult Details(int? id)
        {
            fillUserData();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picklist picklist = db.Picklists.Find(id);
            if (picklist == null)
            {
                return HttpNotFound();
            }
            return View(picklist);
        }

        // GET: /Picklist/Create
        public ActionResult Create()
        {
            fillUserData();

            return View();
        }

        // POST: /Picklist/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Picklist std)
        {
            fillUserData();
            std.CreationDate = DateTime.Now;
            std.LastModificationDate = DateTime.Now;
            std.Creator = getCurrentUser().Id;
            std.Modifier = getCurrentUser().Id;
            db.Picklists.Add(std);
            if (ModelState.IsValid)
            {
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: /Picklist/Edit/5
        public ActionResult Edit(int? id)
        {
            fillUserData();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picklist picklist = db.Picklists.Find(id);
            if (picklist == null)
            {
                return HttpNotFound();
            }
            return View(picklist);
        }

        // POST: /Picklist/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Picklist std)
        {
            fillUserData();
            Picklist toEdit = db.Picklists.Find(std.id);

            if (ModelState.IsValid)
            {
                toEdit.Name = std.Name;
                toEdit.Code = std.Code;
                toEdit.LastModificationDate = DateTime.Now;
                toEdit.Modifier = getCurrentUser().Id;
                db.Entry(toEdit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(std);
        }

        // GET: /Picklist/Delete/5
        public ActionResult Delete(int? id)
        {
            fillUserData();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Picklist picklist = db.Picklists.Find(id);
            if (picklist == null)
            {
                return HttpNotFound();
            }
            return View(picklist);
        }

        // POST: /Picklist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            fillUserData();

            Picklist picklist = db.Picklists.Find(id);
            db.Picklists.Remove(picklist);
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





        public void fillUserData()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDataContext()));
            var user = userManager.FindById(User.Identity.GetUserId());
            ViewBag.CurrentUser = user;
        }

        public ApplicationUser getCurrentUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDataContext()));
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());
            return user;
        }
    }

}
