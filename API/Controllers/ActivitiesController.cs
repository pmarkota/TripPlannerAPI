using API.Models;
using API.Requests.Activity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly postgresContext _db;

            public ActivitiesController(postgresContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Activities);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var activity = _db.Activities.Find(id);
            if (activity == null)
            {
                return NotFound();
            }
            return Ok(activity);
        }

        //post, that returns all activities from certain trip
        [HttpPost("trip")]
        public IActionResult GetByTrip(long tripId)
        {
            var activities = _db.Activities.Where(a => a.TripId == tripId);
            if (activities == null)
            {
                return NotFound();
            }
            return Ok(activities);
        }

        //post with activitypostrequest model
        [HttpPost]
        public IActionResult Post([FromBody] ActivityPostRequest request)
        {
            var activity = new Activity
            {
                Name = request.Name,
                Category = request.Category,
                Description = request.Description
            };
            _db.Activities.Add(activity);
            _db.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = activity.ActivityId }, activity);
        }

        //put with activityputrequest model 
        [HttpPut]
        public IActionResult Put([FromBody] ActivityPutRequest request)
        {
            var activity = _db.Activities.Find(request.Id);
            if (activity == null)
            {
                return NotFound();
            }
            activity.Name = request.Name;
            activity.Category = request.Category;
            activity.Description = request.Description;
            _db.SaveChanges();
            return Ok(activity);
        }

        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var activity = _db.Activities.Find(id);
            if (activity == null)
            {
                return NotFound();
            }
            _db.Activities.Remove(activity);
            _db.SaveChanges();
            return NoContent();
        }


    }
}
