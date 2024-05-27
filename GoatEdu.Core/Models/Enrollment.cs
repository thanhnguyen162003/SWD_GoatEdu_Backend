using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Enrollment")]
    [Index("UserId", "SubjectId", Name = "Enrollment_userId_subjectId_key", IsUnique = true)]
    public partial class Enrollment
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("userId")]
        public Guid? UserId { get; set; }
        [Column("subjectId")]
        public Guid? SubjectId { get; set; }
        [Column("updatedAt", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }

        [ForeignKey("SubjectId")]
        [InverseProperty("Enrollments")]
        public virtual Subject? Subject { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Enrollments")]
        public virtual User? User { get; set; }
        [InverseProperty("Enrollment")]
        public virtual EnrollmentProcess? EnrollmentProcess { get; set; }
    }
}
