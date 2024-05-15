using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("moderators")]
    public partial class Moderator : BaseEntity
    {
        [Key]
        [Column("moderator_id")]
        public Guid ModeratorId { get; set; }
        [Column("moderator_name", TypeName = "character varying")]
        public string? ModeratorName { get; set; }
        [Column("password", TypeName = "character varying")]
        public string? Password { get; set; }
        [Column("email", TypeName = "character varying")]
        public string? Email { get; set; }
        [Column("phone_number", TypeName = "character varying")]
        public string? PhoneNumber { get; set; }
        [Column("is_password_change")]
        public bool? IsPasswordChange { get; set; }
        [Column("created_by", TypeName = "character varying")]
        public string? CreatedBy { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        
    }
}
