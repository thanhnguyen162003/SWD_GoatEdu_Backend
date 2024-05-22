using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class FlashcardContent
    {
        public Guid Id { get; set; }
        public string? FlashcardContentQuestion { get; set; }
        public string? FlashcardContentAnswer { get; set; }
        public Guid? FlashcardId { get; set; }
        public string? Image { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Flashcard? Flashcard { get; set; }
    }
}
