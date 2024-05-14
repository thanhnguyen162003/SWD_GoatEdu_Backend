using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("admins")]
    public partial class Admin
    {
        [Key]
        [Column("admin_id")]
        public Guid AdminId { get; set; }
        [Column("username", TypeName = "character varying")]
        public string? Username { get; set; }
        [Column("password", TypeName = "character varying")]
        public string? Password { get; set; }
        [Column("isDeleted")]
        public bool? IsDeleted { get; set; }
    }
}
