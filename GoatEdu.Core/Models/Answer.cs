using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Answer
    {
        public Answer()
        {
            Votes = new HashSet<Vote>();
        }

        public Guid Id { get; set; }
        public string? AnswerName { get; set; }
        public string? AnswerBody { get; set; }
        public Guid? UserId { get; set; }
        public Guid? QuestionId { get; set; }
        public string? AnswerImage { get; set; }
        public int? AnswerVote { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Discussion? Question { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
