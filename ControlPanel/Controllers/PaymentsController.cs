using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControlPanel.Models;
using ControlPanel.ViewModels;

namespace ControlPanel.Controllers
{
    [Authorize(Roles = "Manager")]
    public class PaymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payments
        public ActionResult Index(int? CompanyUserId,String fromDate="", String toDate="")
        {
            DateTime from = new DateTime(2000, 1, 1);
            DateTime to = new DateTime(3000, 1, 1);
            if (!fromDate.Equals("") && fromDate != null)
            {
                DateTime.TryParse(fromDate, out from);
            }
            if (!toDate.Equals("") && toDate != null)
            {
                DateTime.TryParse(toDate, out to);
            }

            List<Payment> payments = db.Payments.Include(p => p.Company).Include(p => p.prodcut).ToList();

            payments = payments.Where(a => a.CreationDate.CompareTo(from) >= 0
            && a.CreationDate.CompareTo(to) <= 0).ToList();
            
            if (CompanyUserId != null)
            {
                payments= payments.Where(a => a.CompanyId ==CompanyUserId).ToList();
            }

            List<PaymnetViewModel> finalPayments = new List<PaymnetViewModel>();
            foreach (var item in payments)
            {
                PaymnetViewModel temp = new PaymnetViewModel();
                temp.Amount = item.Amount;
                temp.UserAmount = (item.Amount * item.Company.CompanyRatio)/100;
                temp.CompanyAmount = temp.Amount - temp.UserAmount;
                temp.CompanyName = item.Company.name;
                temp.prodcutCode = item.prodcut.ItemCode;
                temp.Status = item.Status;
                temp.productType = item.prodcut.Type;
                temp.CreationDate = item.CreationDate;
                temp.Method = item.Method;
                temp.currency = item.currency;
                finalPayments.Add(temp);
            }
            ViewBag.CompanyUserId = new SelectList(db.Companies, "Id", "Name");
            
            return View(finalPayments);
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companies, "id", "name");
            ViewBag.productId = new SelectList(db.Products, "id", "DesignerName");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Status,productId,CompanyId,Amount,Method,currency,CreationDate,LastModificationDate,Creator,Modifier,AttachmentId")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companies, "id", "name", payment.CompanyId);
            ViewBag.productId = new SelectList(db.Products, "id", "DesignerName", payment.productId);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "id", "name", payment.CompanyId);
            ViewBag.productId = new SelectList(db.Products, "id", "DesignerName", payment.productId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Status,productId,CompanyId,Amount,Method,currency,CreationDate,LastModificationDate,Creator,Modifier,AttachmentId")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companies, "id", "name", payment.CompanyId);
            ViewBag.productId = new SelectList(db.Products, "id", "DesignerName", payment.productId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
