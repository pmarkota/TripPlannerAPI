﻿using API.Models;
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
        [HttpGet("trip/{id}")]
        public IActionResult GetByTrip(long id)
        {
            var budget = _db.Budgets.Find(id);
            if (budget == null)
            {
                return NotFound();
            }
            return Ok(budget);
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
    }
}