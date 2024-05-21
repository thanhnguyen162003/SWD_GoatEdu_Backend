using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Role")]
    public partial class Role : BaseEntity
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("roleName", TypeName = "character varying")]
        public string? RoleName { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        [JsonIgnore]
        public DateTime? CreatedAt { get; set; }
        [Column("updatedAt", TypeName = "timestamp without time zone")]
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        [InverseProperty("Role")]
        public virtual ICollection<User> Users { get; set; }
    }
}
