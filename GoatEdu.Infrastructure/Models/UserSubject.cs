using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("user_subject")]
    public partial class UserSubject
    {
        [Key]
        [Column("user_id")]
        public Guid UserId { get; set; }
        [Key]
        [Column("subject_id")]
        public Guid SubjectId { get; set; }
        [Column("process")]
        public double? Process { get; set; }
        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey("SubjectId")]
        [InverseProperty("UserSubjects")]
        public virtual Subject Subject { get; set; } = null!;
        [ForeignKey("UserId")]
        [InverseProperty("UserSubjects")]
        public virtual User User { get; set; } = null!;
    }
}
