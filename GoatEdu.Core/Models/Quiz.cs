using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Quiz")]
    public partial class Quiz : BaseEntity
    {
        public Quiz()
        {
            QuestionInQuizzes = new HashSet<QuestionInQuiz>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("quiz", TypeName = "character varying")]
        public string? Quiz1 { get; set; }
        [Column("quizLevel")]
        public int? QuizLevel { get; set; }
        [Column("lessonId")]
        public Guid? LessonId { get; set; }
        [Column("chapterId")]
        public Guid? ChapterId { get; set; }
        [Column("subjectId")]
        public Guid? SubjectId { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updatedAt", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [Column("isRequire")]
        public bool? IsRequire { get; set; }

        [ForeignKey("ChapterId")]
        [InverseProperty("Quizzes")]
        public virtual Chapter? Chapter { get; set; }
        [ForeignKey("LessonId")]
        [InverseProperty("Quizzes")]
        public virtual Lesson? Lesson { get; set; }
        [ForeignKey("SubjectId")]
        [InverseProperty("Quizzes")]
        public virtual Subject? Subject { get; set; }
        [InverseProperty("Quiz")]
        public virtual ICollection<QuestionInQuiz> QuestionInQuizzes { get; set; }
    }
}
