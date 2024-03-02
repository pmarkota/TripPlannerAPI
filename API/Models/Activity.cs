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
        public Activity()
        {
            Itineraries = new HashSet<Itinerary>();
        }

        [Key]
        [Column("activity_id")]
        public long ActivityId { get; set; }
        [Column("name", TypeName = "character varying")]
        public string? Name { get; set; }
        [Column("category", TypeName = "character varying")]
        public string? Category { get; set; }
        [Column("description")]
        public string? Description { get; set; }

        [InverseProperty("Activity")]
        public virtual ICollection<Itinerary> Itineraries { get; set; }
    }
}
