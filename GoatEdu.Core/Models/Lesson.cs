using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Lesson
    {
        public Lesson()
        {
            Quizzes = new HashSet<Quiz>();
            Theories = new HashSet<Theory>();
        }

        public Guid Id { get; set; }
        public string? LessonName { get; set; }
        public string? LessonBody { get; set; }
        public string? LessonMaterial { get; set; }
        public Guid? ChapterId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Chapter? Chapter { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
        public virtual ICollection<Theory> Theories { get; set; }
    }
}
