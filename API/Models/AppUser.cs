using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    /// <summary>
    /// users table
    /// </summary>
    [Table("AppUser")]
    public partial class AppUser
    {
        public AppUser()
        {
            Trips = new HashSet<Trip>();
        }

        [Key]
        [Column("user_id")]
        public long UserId { get; set; }
        [Column("email", TypeName = "character varying")]
        public string? Email { get; set; }
        [Column("username", TypeName = "character varying")]
        public string? Username { get; set; }
        [Column("password")]
        public string? Password { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Trip> Trips { get; set; }
    }
}
