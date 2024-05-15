using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("achievements")]
    public partial class Achievement : BaseEntity
    {
        [Key]
        [Column("achievement_id")]
        public Guid AchievementId { get; set; }
        [Column("achievement_name", TypeName = "character varying")]
        public string? AchievementName { get; set; }
        [Column("achievement_content", TypeName = "character varying")]
        public string? AchievementContent { get; set; }
        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Achievements")]
        public virtual User? User { get; set; }
    }
}
