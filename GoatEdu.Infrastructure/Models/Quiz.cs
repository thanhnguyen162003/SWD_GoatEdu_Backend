using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("quizzes")]
    public partial class Quiz
    {
        [Key]
        [Column("quiz_id")]
        public Guid QuizId { get; set; }
        [Column("quiz", TypeName = "character varying")]
        public string? Quiz1 { get; set; }
        [Column("answer_correct", TypeName = "character varying")]
        public string? AnswerCorrect { get; set; }
        [Column("answer_1", TypeName = "character varying")]
        public string? Answer1 { get; set; }
        [Column("answer_2", TypeName = "character varying")]
        public string? Answer2 { get; set; }
        [Column("answer_3", TypeName = "character varying")]
        public string? Answer3 { get; set; }
        [Column("lesson_id")]
        public Guid? LessonId { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [Column("isDeleted")]
        public bool? IsDeleted { get; set; }

        [ForeignKey("LessonId")]
        [InverseProperty("Quizzes")]
        public virtual Lesson? Lesson { get; set; }
    }
}
