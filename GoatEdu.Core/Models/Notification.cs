using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Notification")]
    public partial class Notification
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("notificationName", TypeName = "character varying")]
        public string? NotificationName { get; set; }
        [Column("notificationMessage", TypeName = "character varying")]
        public string? NotificationMessage { get; set; }
        [Column("userId")]
        public Guid? UserId { get; set; }
        [Column("readAt", TypeName = "timestamp without time zone")]
        public DateTime? ReadAt { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Notifications")]
        public virtual User? User { get; set; }
    }
}
