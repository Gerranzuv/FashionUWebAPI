using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewAPIProject.Models;

namespace NewAPIProject.Controllers
{
    public class SystemParametersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SystemParameters
        public async Task<ActionResult> Index()
        {
            return View(await db.SystemParameters.ToListAsync());
        }

        // GET: SystemParameters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemParameter systemParameter = await db.SystemParameters.FindAsync(id);
            if (systemParameter == null)
            {
                return HttpNotFound();
            }
            return View(systemParameter);
        }

        // GET: SystemParameters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SystemParameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Name,Code,Value,CreationDate,LastModificationDate,Creator,Modifier,AttachmentId")] SystemParameter systemParameter)
        {
            if (ModelState.IsValid)
            {
                db.SystemParameters.Add(systemParameter);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(systemParameter);
        }

        // GET: SystemParameters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemParameter systemParameter = await db.SystemParameters.FindAsync(id);
            if (systemParameter == null)
            {
                return HttpNotFound();
            }
            return View(systemParameter);
        }

        // POST: SystemParameters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Name,Code,Value,CreationDate,LastModificationDate,Creator,Modifier,AttachmentId")] SystemParameter systemParameter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(systemParameter).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(systemParameter);
        }

        // GET: SystemParameters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemParameter systemParameter = await db.SystemParameters.FindAsync(id);
            if (systemParameter == null)
            {
                return HttpNotFound();
            }
            return View(systemParameter);
        }

        // POST: SystemParameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SystemParameter systemParameter = await db.SystemParameters.FindAsync(id);
            db.SystemParameters.Remove(systemParameter);
            await db.SaveChangesAsync();
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
