using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("QuestionInQuiz")]
    public partial class QuestionInQuiz : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("quizId")]
        public Guid? QuizId { get; set; }
        [Column("quizQuestion", TypeName = "character varying")]
        public string? QuizQuestion { get; set; }
        [Column("quizAnswer1", TypeName = "character varying")]
        public string? QuizAnswer1 { get; set; }
        [Column("quizAnswer2", TypeName = "character varying")]
        public string? QuizAnswer2 { get; set; }
        [Column("quizAnswer3", TypeName = "character varying")]
        public string? QuizAnswer3 { get; set; }
        [Column("quizCorrect", TypeName = "character varying")]
        public string? QuizCorrect { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updatedAt", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("QuizId")]
        [InverseProperty("QuestionInQuizzes")]
        public virtual Quiz? Quiz { get; set; }
    }
}
