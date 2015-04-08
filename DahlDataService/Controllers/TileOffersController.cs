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
using DahlDataModel;

namespace DahlDataService.Controllers
{
    public class TileOffersController : ApiController
    {
        private DahlEntities db = new DahlEntities();

        // GET: api/TileOffers
        public IQueryable<TileOffer> GetTileOffers()
        {
            return db.TileOffers;
        }

        // GET: api/TileOffers/5
        [ResponseType(typeof(TileOffer))]
        public IHttpActionResult GetTileOffer(int id)
        {
            TileOffer tileOffer = db.TileOffers.Find(id);
            if (tileOffer == null)
            {
                return NotFound();
            }

            return Ok(tileOffer);
        }

        // PUT: api/TileOffers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTileOffer(int id, TileOffer tileOffer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tileOffer.TileOfferId)
            {
                return BadRequest();
            }

            db.Entry(tileOffer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TileOfferExists(id))
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

        // POST: api/TileOffers
        [ResponseType(typeof(TileOffer))]
        public IHttpActionResult PostTileOffer(TileOffer tileOffer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TileOffers.Add(tileOffer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tileOffer.TileOfferId }, tileOffer);
        }

        // DELETE: api/TileOffers/5
        [ResponseType(typeof(TileOffer))]
        public IHttpActionResult DeleteTileOffer(int id)
        {
            TileOffer tileOffer = db.TileOffers.Find(id);
            if (tileOffer == null)
            {
                return NotFound();
            }

            db.TileOffers.Remove(tileOffer);
            db.SaveChanges();

            return Ok(tileOffer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TileOfferExists(int id)
        {
            return db.TileOffers.Count(e => e.TileOfferId == id) > 0;
        }
    }
}