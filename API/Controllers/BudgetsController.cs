using API.Models;
using API.Requests.Budget;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsController : ControllerBase
    {
        private readonly postgresContext _db;

        public BudgetsController(postgresContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Budgets);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var budget = _db.Budgets.Find(id);
            if (budget == null)
            {
                return NotFound();
            }
            return Ok(budget);
        }

        //get for a specific trip
        [HttpPost("trip")]
        public IActionResult GetBudgetsForTrip([FromBody] PostGetBudgetsForTrip request)
        {
            var budgets = _db.Budgets.Where(b => b.TripId == request.TripId);
            if (budgets == null)
            {
                return NotFound();
            }
            return Ok(budgets);
        }

        //post with budgetpostrequest model
        [HttpPost]
        public IActionResult Post([FromBody] BudgetPostRequest request)
        {
            var budget = new Budget
            {
                Category = request.Category,
                Amount = request.Amount,
                TripId = request.TripId
            };
            _db.Budgets.Add(budget);
            _db.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = budget.BudgetId }, budget);
        }

        //put with budgetputrequest model
        [HttpPut]
        public IActionResult Put([FromBody] BudgetPutRequest request)
        {
            var budget = _db.Budgets.Find(request.Id);
            if (budget == null)
            {
                return NotFound();
            }
            budget.Category = request.Category;
            budget.Amount = request.Amount;
            budget.TripId = request.TripId;
            _db.SaveChanges();
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete(long id)
        {
            var budget = _db.Budgets.Find(id);
            if (budget == null)
            {
                return NotFound();
            }
            _db.Budgets.Remove(budget);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
