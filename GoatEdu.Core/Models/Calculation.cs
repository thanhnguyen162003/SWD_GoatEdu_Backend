using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Keyless]
    [Table("Calculation")]
    public partial class Calculation
    {
        [Column("id")]
        public Guid? Id { get; set; }
        [Column("lessonCount")]
        public int? LessonCount { get; set; }
        [Column("chapterCount")]
        public int? ChapterCount { get; set; }
        [Column("quizCount")]
        public int? QuizCount { get; set; }
        [Column("theoryCount")]
        public int? TheoryCount { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updatedAt", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
    }
}
