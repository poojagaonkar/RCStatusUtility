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
    public class DailyStatusController : ApiController
    {
        private RCStatusDatabaseEntities2 db = new RCStatusDatabaseEntities2();

        // GET: api/DailyStatus
        public IQueryable<DailyStatu> GetDailyStatus()
        {
            return db.DailyStatus;
        }

        // GET: api/DailyStatus/5
        [ResponseType(typeof(DailyStatu))]
        public IHttpActionResult GetDailyStatu(int id)
        {
            DailyStatu dailyStatu = db.DailyStatus.Find(id);
            if (dailyStatu == null)
            {
                return NotFound();
            }

            return Ok(dailyStatu);
        }

        // PUT: api/DailyStatus/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDailyStatu(int id, DailyStatu dailyStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dailyStatu.Id)
            {
                return BadRequest();
            }

            db.Entry(dailyStatu).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DailyStatuExists(id))
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

        // POST: api/DailyStatus
        [ResponseType(typeof(DailyStatu))]
        public IHttpActionResult PostDailyStatu(DailyStatu dailyStatu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DailyStatus.Add(dailyStatu);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DailyStatuExists(dailyStatu.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dailyStatu.Id }, dailyStatu);
        }

        // DELETE: api/DailyStatus/5
        [ResponseType(typeof(DailyStatu))]
        public IHttpActionResult DeleteDailyStatu(int id)
        {
            DailyStatu dailyStatu = db.DailyStatus.Find(id);
            if (dailyStatu == null)
            {
                return NotFound();
            }

            db.DailyStatus.Remove(dailyStatu);
            db.SaveChanges();

            return Ok(dailyStatu);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DailyStatuExists(int id)
        {
            return db.DailyStatus.Count(e => e.Id == id) > 0;
        }
    }
}