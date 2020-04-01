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
    builder.EntitySet<Company>("Companies");
    builder.EntitySet<ApplicationUser>("Users"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CompaniesController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CoreController core = new CoreController();

        // GET: odata/Companies
        [EnableQuery]
        public IQueryable<Company> GetCompanies()
        {
           
            return db.Companyies;
        }

        // GET: odata/Companies(5)
        [EnableQuery]
        public SingleResult<Company> GetCompany([FromODataUri] int key)
        {
            return SingleResult.Create(db.Companyies.Where(company => company.id == key));
        }

        // PUT: odata/Companies(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Company> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Company company = await db.Companyies.FindAsync(key);
            if (company == null)
            {
                return NotFound();
            }

            patch.Put(company);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(company);
        }

        // POST: odata/Companies
        public async Task<IHttpActionResult> Post(Company company)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            company.CreationDate = DateTime.Now;
            company.LastModificationDate = DateTime.Now;
            company.Creator = core.getCurrentUser().UserName;
            company.Modifier = core.getCurrentUser().UserName;
            db.Companyies.Add(company);
            await db.SaveChangesAsync();

            return Created(company);
        }

        // PATCH: odata/Companies(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Company> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Company company = await db.Companyies.FindAsync(key);
            if (company == null)
            {
                return NotFound();
            }

            patch.Patch(company);

            try
            {
                company.LastModificationDate = DateTime.Now;
                company.Modifier = core.getCurrentUser().Id;
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(company);
        }

        // DELETE: odata/Companies(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Company company = await db.Companyies.FindAsync(key);
            if (company == null)
            {
                return NotFound();
            }

            db.Companyies.Remove(company);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        //// GET: odata/Companies(5)/CompanyUser
        //[EnableQuery]
        //public SingleResult<ApplicationUser> GetCompanyUser([FromODataUri] int key)
        //{
        //    return SingleResult.Create(db.Companyies.Where(m => m.id == key).Select(m => m.CompanyUser));
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompanyExists(int key)
        {
            return db.Companyies.Count(e => e.id == key) > 0;
        }
    }
}
