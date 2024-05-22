using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Tag
    {
        public Tag()
        {
            Discussions = new HashSet<Discussion>();
            Flashcards = new HashSet<Flashcard>();
        }

        public Guid Id { get; set; }
        public string? TagName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Discussion> Discussions { get; set; }
        public virtual ICollection<Flashcard> Flashcards { get; set; }
    }
}
