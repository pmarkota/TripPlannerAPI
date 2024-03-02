using API.Models;
using API.Requests.Itinerary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItinerariesController : ControllerBase
    {
        private readonly postgresContext _db;

        public ItinerariesController(postgresContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Itineraries);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var itinerary = _db.Itineraries.Find(id);
            if (itinerary == null)
            {
                return NotFound();
            }
            return Ok(itinerary);
        }

        //get all itineraries for a trip
        [HttpGet("trip/{id}")]
        public IActionResult GetByTrip(long id)
        {
            var itineraries = _db.Itineraries.Where(i => i.TripId == id);
            if (itineraries == null)
            {
                return NotFound();
            }
            return Ok(itineraries);
        }

        //post with itinerarypostrequest model
        [HttpPost]
        public IActionResult Post([FromBody] ItineraryPostRequest request)
        {
            var itinerary = new Itinerary
            {
                TripId = request.TripId,
                Date = request.Date,
                ActivityId = request.ActivityId,
                Location = request.Location
            };
            _db.Itineraries.Add(itinerary);
            _db.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = itinerary.ItineraryId }, itinerary);
        }

        //put with itineraryputrequest model 
        [HttpPut]
        public IActionResult Put([FromBody] ItineraryPutRequest request)
        {
            var itinerary = _db.Itineraries.Find(request.Id);
            if (itinerary == null)
            {
                return NotFound();
            }
            itinerary.TripId = request.TripId;
            itinerary.Date = request.Date;
            itinerary.ActivityId = request.ActivityId;
            itinerary.Location = request.Location;
            _db.SaveChanges();
            return Ok(itinerary);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var itinerary = _db.Itineraries.Find(id);
            if (itinerary == null)
            {
                return NotFound();
            }
            _db.Itineraries.Remove(itinerary);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
