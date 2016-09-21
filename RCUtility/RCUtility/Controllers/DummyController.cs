using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RCUtility.Models;

namespace RCUtility.Controllers
{
    public class DummyController : ApiController
    {
        private RCStatusDatabaseEntities db = new RCStatusDatabaseEntities();

        // GET: api/Dummy
        public IQueryable<StatusTable> GetStatusTables()
        {
            return db.StatusTables;
        }

        // GET: api/Dummy/5
        [ResponseType(typeof(StatusTable))]
        public IHttpActionResult GetStatusTable(int id)
        {
            StatusTable statusTable = db.StatusTables.Find(id);
            if (statusTable == null)
            {
                return NotFound();
            }

            return Ok(statusTable);
        }

        // PUT: api/Dummy/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStatusTable(int id, StatusTable statusTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != statusTable.Id)
            {
                return BadRequest();
            }

            db.Entry(statusTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Dummy
        [ResponseType(typeof(StatusTable))]
        public IHttpActionResult PostStatusTable(StatusTable statusTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StatusTables.Add(statusTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StatusTableExists(statusTable.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = statusTable.Id }, statusTable);
        }

        // DELETE: api/Dummy/5
        [ResponseType(typeof(StatusTable))]
        public IHttpActionResult DeleteStatusTable(int id)
        {
            StatusTable statusTable = db.StatusTables.Find(id);
            if (statusTable == null)
            {
                return NotFound();
            }

            db.StatusTables.Remove(statusTable);
            db.SaveChanges();

            return Ok(statusTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StatusTableExists(int id)
        {
            return db.StatusTables.Count(e => e.Id == id) > 0;
        }
    }
}