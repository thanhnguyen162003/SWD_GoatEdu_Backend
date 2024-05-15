using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("answers")]
    public partial class Answer : BaseEntity
    {
        [Key]
        [Column("answer_id")]
        public Guid AnswerId { get; set; }
        [Column("answer_name", TypeName = "character varying")]
        public string? AnswerName { get; set; }
        [Column("answer_body", TypeName = "character varying")]
        public string? AnswerBody { get; set; }
        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("question_id")]
        public Guid? QuestionId { get; set; }
        [Column("answer_image", TypeName = "character varying")]
        public string? AnswerImage { get; set; }
        [Column("answer_vote")]
        public int? AnswerVote { get; set; }
        [Column("tag", TypeName = "character varying")]
        public string? Tag { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("created_by", TypeName = "character varying")]
        public string? CreatedBy { get; set; }
        [Column("updated_by", TypeName = "character varying")]
        public string? UpdatedBy { get; set; }
        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        
        [ForeignKey("QuestionId")]
        [InverseProperty("Answers")]
        public virtual Discussion? Question { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Answers")]
        public virtual User? User { get; set; }
    }
}
