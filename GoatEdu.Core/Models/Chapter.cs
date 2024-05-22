using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Chapter
    {
        public Chapter()
        {
            Lessons = new HashSet<Lesson>();
            Quizzes = new HashSet<Quiz>();
        }

        public Guid Id { get; set; }
        public string? ChapterName { get; set; }
        public int? ChapterLevel { get; set; }
        public Guid? SubjectId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Subject? Subject { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
