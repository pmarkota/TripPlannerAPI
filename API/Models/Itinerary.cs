using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    /// <summary>
    /// Itinerary Table
    /// </summary>
    [Table("Itinerary")]
    public partial class Itinerary
    {
        [Key]
        [Column("itinerary_id")]
        public long ItineraryId { get; set; }
        [Column("trip_id")]
        public long? TripId { get; set; }
        [Column("date")]
        public DateTime? Date { get; set; }
        [Column("activity_id")]
        public long? ActivityId { get; set; }
        [Column("location")]
        public string? Location { get; set; }

        [ForeignKey("ActivityId")]
        [InverseProperty("Itineraries")]
        public virtual Activity? Activity { get; set; }
        [ForeignKey("TripId")]
        [InverseProperty("Itineraries")]
        public virtual Trip? Trip { get; set; }
    }
}
