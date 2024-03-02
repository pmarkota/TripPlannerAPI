using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Table("PackingList")]
    public partial class PackingList
    {
        [Key]
        [Column("packing_id")]
        public long PackingId { get; set; }
        [Column("trip_id")]
        public long? TripId { get; set; }
        [Column("item", TypeName = "character varying")]
        public string? Item { get; set; }
        [Column("category", TypeName = "character varying")]
        public string? Category { get; set; }
        [Column("quantity")]
        public long? Quantity { get; set; }

        [ForeignKey("TripId")]
        [InverseProperty("PackingLists")]
        public virtual Trip? Trip { get; set; }
    }
}
