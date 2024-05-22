using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Quiz
    {
        public Quiz()
        {
            QuestionInQuizzes = new HashSet<QuestionInQuiz>();
        }

        public Guid Id { get; set; }
        public string? Quiz1 { get; set; }
        public int? QuizLevel { get; set; }
        public Guid? LessonId { get; set; }
        public Guid? ChapterId { get; set; }
        public Guid? SubjectId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsRequire { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Chapter? Chapter { get; set; }
        public virtual Lesson? Lesson { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual ICollection<QuestionInQuiz> QuestionInQuizzes { get; set; }
    }
}
