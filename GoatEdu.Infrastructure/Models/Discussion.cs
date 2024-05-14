using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("discussions")]
    public partial class Discussion
    {
        public Discussion()
        {
            Answers = new HashSet<Answer>();
        }

        [Key]
        [Column("discussion_id")]
        public Guid DiscussionId { get; set; }
        [Column("discussion_name", TypeName = "character varying")]
        public string? DiscussionName { get; set; }
        [Column("discussion_body", TypeName = "character varying")]
        public string? DiscussionBody { get; set; }
        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("discussion_image", TypeName = "character varying")]
        public string? DiscussionImage { get; set; }
        [Column("discussion_vote")]
        public int? DiscussionVote { get; set; }
        [Column("tag", TypeName = "character varying")]
        public string? Tag { get; set; }
        [Column("subject_id")]
        public Guid? SubjectId { get; set; }
        [Column("is_solved")]
        public bool? IsSolved { get; set; }
        [Column("status", TypeName = "character varying")]
        public string? Status { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("created_by", TypeName = "character varying")]
        public string? CreatedBy { get; set; }
        [Column("updated_by", TypeName = "character varying")]
        public string? UpdatedBy { get; set; }
        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [Column("isDeleted")]
        public bool? IsDeleted { get; set; }

        [ForeignKey("SubjectId")]
        [InverseProperty("Discussions")]
        public virtual Subject? Subject { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Discussions")]
        public virtual User? User { get; set; }
        [InverseProperty("Question")]
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
