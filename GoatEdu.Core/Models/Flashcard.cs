using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Flashcard
    {
        public Flashcard()
        {
            FlashcardContents = new HashSet<FlashcardContent>();
            Tags = new HashSet<Tag>();
        }

        public Guid Id { get; set; }
        public string? FlashcardName { get; set; }
        public string? FlashcardDescription { get; set; }
        public Guid? UserId { get; set; }
        public Guid? TagId { get; set; }
        public Guid? SubjectId { get; set; }
        public string? Status { get; set; }
        public int? Star { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Subject? Subject { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<FlashcardContent> FlashcardContents { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
