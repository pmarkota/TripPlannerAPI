using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Requests.Trip;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly postgresContext _db;

        public TripsController(postgresContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Trips);
        }

        // GET api/trips/5, use TripGetById to get trip only if it belongs to certain user , but make it a post request
        [HttpPost("getbyid")]
        public IActionResult GetById([FromBody] TripGetById request)
        {
            var trip = _db.Trips.Find(request.TripId);
            if (trip == null || trip.UserId != request.UserId)
            {
                return NotFound();
            }
            return Ok(trip);
        }

        //get all trips from certain user.
        [HttpGet("user/{id}")]
        public IActionResult GetTripsByUser(int id)
        {
            var trips = _db.Trips.Where(t => t.UserId == id);
            if (trips == null)
            {
                return NotFound();
            }
            return Ok(trips);
        }

        //post for trip with TripPostRequest
        [HttpPost]
        public IActionResult Post([FromBody] TripPostRequest request)
        {
            // Validate the registration request
            if (request == null)
            {
                return BadRequest("Invalid trip request");
            }

            var newTrip = _db.Trips.Add(new Trip
            {
                Purpose = request.Purpose,
                Destination = request.Destination,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                UserId = request.UserId
            });



            _db.SaveChanges();
            return Ok( new {message="New trip added successfuly"});

        }

        // PUT api/trips/5 using TripPutRequest. that class has Id in it
        [HttpPut]
        public IActionResult Put([FromBody] TripPutRequest request)
        {
            // Validate the registration request
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid trip request");
            }

            var trip = _db.Trips.Find(request.Id);
            if (trip == null)
            {
                return NotFound();
            }

            trip.Purpose = request.Purpose;
            trip.Destination = request.Destination;
            trip.StartDate = request.StartDate;
            trip.EndDate = request.EndDate;

            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var trip = _db.Trips.Find(id);
            if (trip == null)
            {
                return NotFound();
            }

            _db.Trips.Remove(trip);
            _db.SaveChanges();
            return Ok();
        }

    }
}
