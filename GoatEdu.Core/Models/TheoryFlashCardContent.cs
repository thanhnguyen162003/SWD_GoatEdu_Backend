using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class TheoryFlashCardContent
    {
        public Guid Id { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public Guid? TheoryId { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Theory? Theory { get; set; }
    }
}
