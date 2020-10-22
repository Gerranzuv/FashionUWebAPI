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
    public class ShippingRequestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShippingRequests
        public ActionResult Index()
        {
            var shippingRequests = db.ShippingRequests.Include(s => s.Payment).Include(s => s.prodcut);
            return View(shippingRequests.ToList());
        }

        // GET: ShippingRequests
        public ActionResult OpenShippingRequests()
        {
            var shippingRequests = db.ShippingRequests.Where(a => a.Status.Equals("Active")).Include(s => s.Payment).Include(s => s.prodcut);
            return View(shippingRequests.ToList());
        }
        public ActionResult CancelledShippingRequests()
        {
            var shippingRequests = db.ShippingRequests.Where(a => a.Status.Equals("Cancelled")).Include(s => s.Payment).Include(s => s.prodcut);
            return View(shippingRequests.ToList());
        }

        public ActionResult DoneShippingRequests()
        {
            var shippingRequests = db.ShippingRequests.Include(s => s.Payment).Where(a => a.Status.Equals("Done")).Include(s => s.prodcut);
            return View(shippingRequests.ToList());
        }

        public ActionResult ScheduledShippingRequests()
        {
            var shippingRequests = db.ShippingRequests.Where(a => a.Status.Equals("Scheduled")).Include(s => s.Payment).Include(s => s.prodcut);
            return View(shippingRequests.ToList());
        }

        // GET: ShippingRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingRequest shippingRequest = db.ShippingRequests.Find(id);
            if (shippingRequest == null)
            {
                return HttpNotFound();
            }
            return View(shippingRequest);
        }

        // GET: ShippingRequests/Create
        public ActionResult Create()
        {
            ViewBag.PaymentId = new SelectList(db.Payments, "id", "Status");
            ViewBag.productId = new SelectList(db.Products, "id", "DesignerName");
            return View();
        }

        // POST: ShippingRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Status,productId,scheduledDate,PaymentId,CreationDate,LastModificationDate,Creator,Modifier,AttachmentId")] ShippingRequest shippingRequest)
        {
            if (ModelState.IsValid)
            {
                db.ShippingRequests.Add(shippingRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PaymentId = new SelectList(db.Payments, "id", "Status", shippingRequest.PaymentId);
            ViewBag.productId = new SelectList(db.Products, "id", "DesignerName", shippingRequest.productId);
            return View(shippingRequest);
        }

        // GET: ShippingRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingRequest shippingRequest = db.ShippingRequests.Find(id);
            if (shippingRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.PaymentId = new SelectList(db.Payments, "id", "Status", shippingRequest.PaymentId);
            ViewBag.productId = new SelectList(db.Products, "id", "DesignerName", shippingRequest.productId);
            return View(shippingRequest);
        }

        // POST: ShippingRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Status,productId,scheduledDate,PaymentId,CreationDate,LastModificationDate,Creator,Modifier,AttachmentId")] ShippingRequest shippingRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shippingRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PaymentId = new SelectList(db.Payments, "id", "Status", shippingRequest.PaymentId);
            ViewBag.productId = new SelectList(db.Products, "id", "DesignerName", shippingRequest.productId);
            return View(shippingRequest);
        }

        // GET: ShippingRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingRequest shippingRequest = db.ShippingRequests.Find(id);
            if (shippingRequest == null)
            {
                return HttpNotFound();
            }
            return View(shippingRequest);
        }

        // POST: ShippingRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShippingRequest shippingRequest = db.ShippingRequests.Find(id);
            db.ShippingRequests.Remove(shippingRequest);
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
