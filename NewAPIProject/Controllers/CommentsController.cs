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
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Web.Routing;

namespace NewAPIProject.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using NewAPIProject.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Comment>("Comments");
    builder.EntitySet<Product>("Products"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    [Authorize]
    public class CommentsController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CoreController core = new CoreController();

        // GET: odata/Comments
        [EnableQuery]
        public IQueryable<Comment> GetComments()
        {
            return db.Comments;
        }

        // GET: odata/Comments(5)
        [EnableQuery]
        public SingleResult<Comment> GetComment([FromODataUri] int key)
        {
            return SingleResult.Create(db.Comments.Where(comment => comment.id == key));
        }

        // PUT: odata/Comments(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Comment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Comment comment = await db.Comments.FindAsync(key);
            if (comment == null)
            {
                return NotFound();
            }

            patch.Put(comment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(comment);
        }

        // POST: odata/Comments
        public async Task<IHttpActionResult> Post(Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (comment.Text == null || comment.Text.Equals(""))
                return BadRequest("Text can't be null");
            if (comment.ProductId == null)
                return BadRequest("Product can't be null");
            Product product = db.Products.Find(comment.ProductId);
            if (product == null)
                return BadRequest("No Matching product!");
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            comment.Creator = core.getCurrentUser().Name;
            comment.Modifier = core.getCurrentUser().Id;
            comment.LastModificationDate = DateTime.Now;
            comment.CreationDate = DateTime.UtcNow.AddHours(CoreController.HOURS_TO_ADD);
            comment.Product = product;
            db.Comments.Add(comment);
            await db.SaveChangesAsync();

            return Created(comment);
        }

        // PATCH: odata/Comments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Comment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Comment comment = await db.Comments.FindAsync(key);
            if (comment == null)
            {
                return NotFound();
            }

            patch.Patch(comment);

            try
            {
                comment.Modifier = core.getCurrentUser().Id;
                comment.LastModificationDate = DateTime.Now;
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(comment);
        }

        // DELETE: odata/Comments(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Comment comment = await db.Comments.FindAsync(key);
            if (comment == null)
            {
                return NotFound();
            }

            db.Comments.Remove(comment);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Comments(5)/Product
        [EnableQuery]
        public SingleResult<Product> GetProduct([FromODataUri] int key)
        {
            return SingleResult.Create(db.Comments.Where(m => m.id == key).Select(m => m.Product));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CommentExists(int key)
        {
            return db.Comments.Count(e => e.id == key) > 0;
        }
    }
}
