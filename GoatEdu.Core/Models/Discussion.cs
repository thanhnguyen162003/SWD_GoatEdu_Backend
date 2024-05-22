using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Discussion
    {
        public Discussion()
        {
            Answers = new HashSet<Answer>();
            Votes = new HashSet<Vote>();
            Tags = new HashSet<Tag>();
        }

        public Guid Id { get; set; }
        public string? DiscussionName { get; set; }
        public string? DiscussionBody { get; set; }
        public Guid? UserId { get; set; }
        public string? DiscussionImage { get; set; }
        public int? DiscussionVote { get; set; }
        public Guid? SubjectId { get; set; }
        public bool? IsSolved { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Subject? Subject { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
