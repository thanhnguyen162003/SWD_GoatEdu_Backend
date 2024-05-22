using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Calculation
    {
        public Guid? Id { get; set; }
        public int? LessonCount { get; set; }
        public int? ChapterCount { get; set; }
        public int? QuizCount { get; set; }
        public int? TheoryCount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
