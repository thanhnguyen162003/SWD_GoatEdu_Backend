using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Lesson")]
    public partial class Lesson : BaseEntity
    {
        public Lesson()
        {
            Quizzes = new HashSet<Quiz>();
            Theories = new HashSet<Theory>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("lessonName", TypeName = "character varying")]
        public string? LessonName { get; set; }
        [Column("lessonBody", TypeName = "character varying")]
        public string? LessonBody { get; set; }
        [Column("lessonMaterial", TypeName = "character varying")]
        public string? LessonMaterial { get; set; }
        [Column("chapterId")]
        public Guid? ChapterId { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("createdBy", TypeName = "character varying")]
        public string? CreatedBy { get; set; }
        [Column("updatedBy", TypeName = "character varying")]
        public string? UpdatedBy { get; set; }
        [Column("updatedAt", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [Column("displayOrder")]
        public int? DisplayOrder { get; set; }

        [ForeignKey("ChapterId")]
        [InverseProperty("Lessons")]
        public virtual Chapter? Chapter { get; set; }
        [InverseProperty("Lesson")]
        public virtual ICollection<Quiz> Quizzes { get; set; }
        [InverseProperty("Lesson")]
        public virtual ICollection<Theory> Theories { get; set; }
    }
}
