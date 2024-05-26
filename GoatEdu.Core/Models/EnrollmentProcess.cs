using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("EnrollmentProcess")]
    [Index("EnrollmentId", Name = "EnrollmentProcess_enrollmentId_key", IsUnique = true)]
    public partial class EnrollmentProcess
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("enrollmentId")]
        public Guid? EnrollmentId { get; set; }
        [Column("process")]
        public int? Process { get; set; }
        [Column("chapterId")]
        public Guid? ChapterId { get; set; }
        [Column("status", TypeName = "character varying")]
        public string? Status { get; set; }

        [ForeignKey("EnrollmentId")]
        [InverseProperty("EnrollmentProcess")]
        public virtual Enrollment? Enrollment { get; set; }
    }
}
