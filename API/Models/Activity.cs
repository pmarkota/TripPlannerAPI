using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    /// <summary>
    /// activity table
    /// </summary>
    [Table("Activity")]
    public partial class Activity
    {
        [Key]
        [Column("activity_id")]
        public long ActivityId { get; set; }
        [Column("name", TypeName = "character varying")]
        public string? Name { get; set; }
        [Column("category", TypeName = "character varying")]
        public string? Category { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("trip_id")]
        public long? TripId { get; set; }

        [ForeignKey("TripId")]
        [InverseProperty("Activities")]
        public virtual Trip? Trip { get; set; }
    }
}
