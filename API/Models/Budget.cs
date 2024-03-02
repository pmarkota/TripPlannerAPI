using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    /// <summary>
    /// budgets table
    /// </summary>
    [Table("Budget")]
    public partial class Budget
    {
        [Key]
        [Column("budget_id")]
        public long BudgetId { get; set; }
        [Column("trip_id")]
        public long? TripId { get; set; }
        [Column("category", TypeName = "character varying")]
        public string? Category { get; set; }
        [Column("amount")]
        public long? Amount { get; set; }

        [ForeignKey("TripId")]
        [InverseProperty("Budgets")]
        public virtual Trip? Trip { get; set; }
    }
}
