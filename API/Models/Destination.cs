using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    /// <summary>
    /// destinations table
    /// </summary>
    [Table("Destination")]
    public partial class Destination
    {
        [Key]
        [Column("destination_id")]
        public long DestinationId { get; set; }
        [Column("name", TypeName = "character varying")]
        public string? Name { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("country", TypeName = "character varying")]
        public string? Country { get; set; }
        [Column("currency", TypeName = "character varying")]
        public string? Currency { get; set; }
    }
}
