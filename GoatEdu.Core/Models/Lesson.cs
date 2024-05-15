using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("lessons")]
    public partial class Lesson : BaseEntity
    {
        public Lesson()
        {
            Quizzes = new HashSet<Quiz>();
            Theories = new HashSet<Theory>();
        }

        [Key]
        [Column("lesson_id")]
        public Guid LessonId { get; set; }
        [Column("lesson_name", TypeName = "character varying")]
        public string? LessonName { get; set; }
        [Column("lesson_body", TypeName = "character varying")]
        public string? LessonBody { get; set; }
        [Column("lesson_material", TypeName = "character varying")]
        public string? LessonMaterial { get; set; }
        [Column("chapter_id")]
        public Guid? ChapterId { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("created_by", TypeName = "character varying")]
        public string? CreatedBy { get; set; }
        [Column("updated_by", TypeName = "character varying")]
        public string? UpdatedBy { get; set; }
        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("ChapterId")]
        [InverseProperty("Lessons")]
        public virtual Chapter? Chapter { get; set; }
        [InverseProperty("Lesson")]
        public virtual ICollection<Quiz> Quizzes { get; set; }
        [InverseProperty("Lesson")]
        public virtual ICollection<Theory> Theories { get; set; }
    }
}
