using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Discussion")]
    public partial class Discussion : BaseEntity
    {
        public Discussion()
        {
            Answers = new HashSet<Answer>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("discussionName", TypeName = "character varying")]
        public string? DiscussionName { get; set; }
        [Column("discussionBody", TypeName = "character varying")]
        public string? DiscussionBody { get; set; }
        [Column("userId")]
        public Guid? UserId { get; set; }
        [Column("discussionImage", TypeName = "character varying")]
        public string? DiscussionImage { get; set; }
        [Column("discussionVote")]
        public int? DiscussionVote { get; set; }
        [Column("tag", TypeName = "character varying")]
        public string? Tag { get; set; }
        [Column("subjectId")]
        public Guid? SubjectId { get; set; }
        [Column("isSolved")]
        public bool? IsSolved { get; set; }
        [Column("status", TypeName = "character varying")]
        public string? Status { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("createdBy", TypeName = "character varying")]
        public string? CreatedBy { get; set; }
        [Column("updatedBy", TypeName = "character varying")]
        public string? UpdatedBy { get; set; }
        [Column("updatedAt", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
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
