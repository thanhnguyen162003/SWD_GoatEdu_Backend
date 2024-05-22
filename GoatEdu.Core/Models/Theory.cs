using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Theory
    {
        public Theory()
        {
            TheoryFlashCardContents = new HashSet<TheoryFlashCardContent>();
        }

        public Guid Id { get; set; }
        public string? TheoryName { get; set; }
        public string? File { get; set; }
        public string? Image { get; set; }
        public string? TheoryContent { get; set; }
        public Guid? LessonId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Lesson? Lesson { get; set; }
        public virtual ICollection<TheoryFlashCardContent> TheoryFlashCardContents { get; set; }
    }
}
