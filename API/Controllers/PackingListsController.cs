using API.Models;
using API.Requests.PackingList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackingListsController : ControllerBase
    {
        private readonly postgresContext _db;

        public PackingListsController(postgresContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.PackingLists);
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var packingList = _db.PackingLists.Find(id);
            if (packingList == null)
            {
                return NotFound();
            }
            return Ok(packingList);
        }

        [HttpGet("trip/{id}")]
        public IActionResult GetByTrip(long id)
        {
            var packingLists = _db.PackingLists.Where(p => p.TripId == id);
            if (packingLists == null)
            {
                return NotFound();
            }
            return Ok(packingLists);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PackingListPostRequest request)
        {
            var packingList = new PackingList
            {
                TripId = request.TripId,
                Item = request.Item,
                Quantity = request.Quantity
            };
            _db.PackingLists.Add(packingList);
            _db.SaveChanges();
            return Ok(packingList);
        }

        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] PackingListPutRequest request)
        {
            var packingList = _db.PackingLists.Find(id);
            if (packingList == null)
            {
                return NotFound();
            }
            packingList.TripId = request.TripId;
            packingList.Item = request.Item;
            packingList.Quantity = request.Quantity;
            _db.SaveChanges();
            return Ok(packingList);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var packingList = _db.PackingLists.Find(id);
            if (packingList == null)
            {
                return NotFound();
            }
            _db.PackingLists.Remove(packingList);
            _db.SaveChanges();
            return Ok();
        }
    }
}
