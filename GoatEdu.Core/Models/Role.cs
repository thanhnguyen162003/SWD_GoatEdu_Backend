using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("roles")]
    public partial class Role : BaseEntity
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [Key]
        [Column("role_id")]
        public Guid RoleId { get; set; }
        [Column("role_name", TypeName = "character varying")]
        public string? RoleName { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<User> Users { get; set; }
    }
}
