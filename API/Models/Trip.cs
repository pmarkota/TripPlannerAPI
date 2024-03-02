using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    /// <summary>
    /// trips table
    /// </summary>
    [Table("Trip")]
    public partial class Trip
    {
        public Trip()
        {
            Budgets = new HashSet<Budget>();
            Itineraries = new HashSet<Itinerary>();
            PackingLists = new HashSet<PackingList>();
        }

        [Key]
        [Column("trip_id")]
        public long TripId { get; set; }
        [Column("user_id")]
        public long? UserId { get; set; }
        [Column("destination", TypeName = "character varying")]
        public string? Destination { get; set; }
        [Column("start_date")]
        public DateTime? StartDate { get; set; }
        [Column("end_date")]
        public DateTime? EndDate { get; set; }
        [Column("purpose", TypeName = "character varying")]
        public string? Purpose { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Trips")]
        public virtual AppUser? User { get; set; }
        [InverseProperty("Trip")]
        public virtual ICollection<Budget> Budgets { get; set; }
        [InverseProperty("Trip")]
        public virtual ICollection<Itinerary> Itineraries { get; set; }
        [InverseProperty("Trip")]
        public virtual ICollection<PackingList> PackingLists { get; set; }
    }
}
