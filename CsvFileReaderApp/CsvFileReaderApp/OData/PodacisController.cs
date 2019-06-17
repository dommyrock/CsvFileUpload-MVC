using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using CsvFileReaderApp.Models;

namespace CsvFileReaderApp.OData
{
    /*
     * THIS CONTROLLER WAS AUTO GENERATED  FOR ODATA TESTING !!!!!
     *
     *
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using CsvFileReaderApp.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Podaci>("Podacis");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */

    public class PodacisController : ODataController
    {
        private CsvContext db = new CsvContext();

        // GET: odata/Podacis
        [EnableQuery]
        public IQueryable<Podaci> GetPodacis()
        {
            return db.Podacis;
        }

        // GET: odata/Podacis(5)
        [EnableQuery]
        public SingleResult<Podaci> GetPodaci([FromODataUri] int key)
        {
            return SingleResult.Create(db.Podacis.Where(podaci => podaci.Id == key));
        }

        // PUT: odata/Podacis(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Podaci> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Podaci podaci = db.Podacis.Find(key);
            if (podaci == null)
            {
                return NotFound();
            }

            patch.Put(podaci);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PodaciExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(podaci);
        }

        // POST: odata/Podacis
        public IHttpActionResult Post(Podaci podaci)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Podacis.Add(podaci);
            db.SaveChanges();

            return Created(podaci);
        }

        // PATCH: odata/Podacis(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Podaci> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Podaci podaci = db.Podacis.Find(key);
            if (podaci == null)
            {
                return NotFound();
            }

            patch.Patch(podaci);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PodaciExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(podaci);
        }

        // DELETE: odata/Podacis(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Podaci podaci = db.Podacis.Find(key);
            if (podaci == null)
            {
                return NotFound();
            }

            db.Podacis.Remove(podaci);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PodaciExists(int key)
        {
            return db.Podacis.Count(e => e.Id == key) > 0;
        }
    }
}