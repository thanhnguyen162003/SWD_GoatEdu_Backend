using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Keyless]
    [Table("calculations")]
    public partial class Calculation
    {
        [Column("subject_id")]
        public Guid? SubjectId { get; set; }
        [Column("lesson_count")]
        public int? LessonCount { get; set; }
        [Column("chapter_count")]
        public int? ChapterCount { get; set; }
        [Column("quiz_count")]
        public int? QuizCount { get; set; }
        [Column("theory_count")]
        public int? TheoryCount { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
    }
}
