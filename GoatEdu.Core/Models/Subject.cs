using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Chapters = new HashSet<Chapter>();
            Discussions = new HashSet<Discussion>();
            Enrollments = new HashSet<Enrollment>();
            Flashcards = new HashSet<Flashcard>();
            Quizzes = new HashSet<Quiz>();
        }

        public Guid Id { get; set; }
        public string? SubjectName { get; set; }
        public string? SubjectCode { get; set; }
        public string? Information { get; set; }
        public string? Class { get; set; }
        public string? Image { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }
        public virtual ICollection<Discussion> Discussions { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Flashcard> Flashcards { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
