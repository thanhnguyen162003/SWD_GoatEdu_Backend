using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Note
    {
        public Guid Id { get; set; }
        public string? NoteName { get; set; }
        public string? NoteBody { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual User? User { get; set; }
    }
}
