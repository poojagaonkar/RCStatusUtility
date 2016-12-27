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
    public class LeavesController : ApiController
    {
        private RCStatusDatabaseEntities1 db = new RCStatusDatabaseEntities1();

        // GET: api/Leaves
        public IQueryable<LeaveTable> GetLeaveTables()
        {
            return db.LeaveTables;
        }

        // GET: api/Leaves/5
        [ResponseType(typeof(LeaveTable))]
        public IHttpActionResult GetLeaveTable(int id)
        {
            LeaveTable leaveTable = db.LeaveTables.Find(id);
            if (leaveTable == null)
            {
                return NotFound();
            }

            return Ok(leaveTable);
        }

        // PUT: api/Leaves/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLeaveTable(int id, LeaveTable leaveTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != leaveTable.Id)
            {
                return BadRequest();
            }

            db.Entry(leaveTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveTableExists(id))
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

        // POST: api/Leaves
        [ResponseType(typeof(LeaveTable))]
        public IHttpActionResult PostLeaveTable(LeaveTable leaveTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LeaveTables.Add(leaveTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException dEx)
            {
                if (LeaveTableExists(leaveTable.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = leaveTable.Id }, leaveTable);
        }

        // DELETE: api/Leaves/5
        [ResponseType(typeof(LeaveTable))]
        public IHttpActionResult DeleteLeaveTable(int id)
        {
            LeaveTable leaveTable = db.LeaveTables.Find(id);
            if (leaveTable == null)
            {
                return NotFound();
            }

            db.LeaveTables.Remove(leaveTable);
            db.SaveChanges();

            return Ok(leaveTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LeaveTableExists(int id)
        {
            return db.LeaveTables.Count(e => e.Id == id) > 0;
        }
    }
}