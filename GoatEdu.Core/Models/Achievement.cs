using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Achievement")]
    public partial class Achievement : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("achievementName", TypeName = "character varying")]
        public string? AchievementName { get; set; }
        [Column("achievementContent", TypeName = "character varying")]
        public string? AchievementContent { get; set; }
        [Column("userId")]
        public Guid? UserId { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
       
        [ForeignKey("UserId")]
        [InverseProperty("Achievements")]
        public virtual User? User { get; set; }
    }
}
