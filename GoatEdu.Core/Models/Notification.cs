using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("notifications")]
    public partial class Notification : BaseEntity
    {
        [Key]
        [Column("notification_id")]
        public Guid NotificationId { get; set; }
        [Column("notification_name", TypeName = "character varying")]
        public string? NotificationName { get; set; }
        [Column("notification_message", TypeName = "character varying")]
        public string? NotificationMessage { get; set; }
        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("read_at", TypeName = "timestamp without time zone")]
        public DateTime? ReadAt { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Notifications")]
        public virtual User? User { get; set; }
    }
}
