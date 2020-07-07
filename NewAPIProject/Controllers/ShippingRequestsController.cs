using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using NewAPIProject.Models;

namespace NewAPIProject.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using NewAPIProject.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<ShippingRequest>("ShippingRequests");
    builder.EntitySet<Product>("Products"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    [Authorize]
    public class ShippingRequestsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CoreController core = new CoreController();
        private AccountController account = new AccountController();
        // GET: odata/ShippingRequests
        [EnableQuery]
        public IQueryable<ShippingRequest> GetShippingRequests()
        {
            //var user = core.getCurrentUser();
            //if(user.companyUser)
            //    return db.ShippingRequests.Where(a => a.prodcut.Company.CompanyUserId.Equals(user.Id));
            //else
            //    return db.ShippingRequests.Where(a => a.Creator.Equals(user.Id));
            return db.ShippingRequests.OrderByDescending(a=>a.CreationDate);

        }

        // GET: odata/ShippingRequests(5)
        [EnableQuery]
        public SingleResult<ShippingRequest> GetShippingRequest([FromODataUri] int key)
        {
            return SingleResult.Create(db.ShippingRequests.Where(shippingRequest => shippingRequest.id == key));
        }

        // PUT: odata/ShippingRequests(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<ShippingRequest> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ShippingRequest shippingRequest = await db.ShippingRequests.FindAsync(key);
            if (shippingRequest == null)
            {
                return NotFound();
            }

            patch.Put(shippingRequest);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingRequestExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(shippingRequest);
        }

        // POST: odata/ShippingRequests
        public async Task<IHttpActionResult> Post(ShippingRequest shippingRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            shippingRequest.CreationDate = DateTime.Now.AddHours(CoreController.HOURS_TO_ADD);
            shippingRequest.LastModificationDate = DateTime.Now;
            shippingRequest.Creator = core.getCurrentUser().UserName;
            shippingRequest.Modifier = core.getCurrentUser().UserName;
            db.ShippingRequests.Add(shippingRequest);
            await db.SaveChangesAsync();

            return Created(shippingRequest);
        }

        // PATCH: odata/ShippingRequests(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<ShippingRequest> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ShippingRequest shippingRequest = await db.ShippingRequests.FindAsync(key);
            if (shippingRequest == null)
            {
                return NotFound();
            }
            bool scheduledDateUpdated = false;
            bool StatusUpdated = false;
            foreach (var fieldname in patch.GetChangedPropertyNames())
            {
                if (fieldname.Equals("scheduledDate"))
                {
                    scheduledDateUpdated = true;

                }
                if (fieldname.Equals("Status"))
                {
                    StatusUpdated = true;

                }
            }
                patch.Patch(shippingRequest);

            try
            {
                if (scheduledDateUpdated)
                {
                    shippingRequest.Status = "Scheduled";
                }
                if (StatusUpdated ) {
                    if(shippingRequest.Status.Equals("Cancelled"))
                    {
                        shippingRequest.CancelationDate = DateTime.Now;
                    }
                    if (shippingRequest.Status.Equals("Done")) {
                        Product product = db.Products.Find(shippingRequest.productId);
                        Payment payment=account.AddNewPayment(product, core.getCurrentUser());
                        shippingRequest.PaymentId = payment.id;
                        shippingRequest.Payment = payment;
                    }
                }
                
                shippingRequest.LastModificationDate = DateTime.Now;
                shippingRequest.Modifier = core.getCurrentUser().Id;
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippingRequestExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(shippingRequest);
        }

        // DELETE: odata/ShippingRequests(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            ShippingRequest shippingRequest = await db.ShippingRequests.FindAsync(key);
            if (shippingRequest == null)
            {
                return NotFound();
            }

            db.ShippingRequests.Remove(shippingRequest);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/ShippingRequests(5)/prodcut
        [EnableQuery]
        public SingleResult<Product> Getprodcut([FromODataUri] int key)
        {
            return SingleResult.Create(db.ShippingRequests.Where(m => m.id == key).Select(m => m.prodcut));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShippingRequestExists(int key)
        {
            return db.ShippingRequests.Count(e => e.id == key) > 0;
        }
    }
}
