﻿namespace API.Requests.Budget
{
    public class BudgetPostRequest
    {
        public string Category { get; set; }
        public double Amount { get; set; }
        public int TripId { get; set; }
    }
}
