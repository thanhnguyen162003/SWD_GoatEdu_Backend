using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Answer")]
    public partial class Answer : BaseEntity
    {

        public Answer()
        {
            Votes = new HashSet<Vote>();
        }
        
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("answerName", TypeName = "character varying")]
        public string? AnswerName { get; set; }
        [Column("answerBody", TypeName = "character varying")]
        public string? AnswerBody { get; set; }
        [Column("answerBodyHtml", TypeName = "character varying")]
        public string? AnswerBodyHtml { get; set; }
        [Column("userId")]
        public Guid? UserId { get; set; }
        [Column("questionId")]
        public Guid? QuestionId { get; set; }
        [Column("answerImage", TypeName = "character varying")]
        public string? AnswerImage { get; set; }
        [Column("answerVote")]
        public int? AnswerVote { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("createdBy", TypeName = "character varying")]
        public string? CreatedBy { get; set; }
        [Column("updatedBy", TypeName = "character varying")]
        public string? UpdatedBy { get; set; }
        [Column("updatedAt", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("QuestionId")]
        [InverseProperty("Answers")]
        public virtual Discussion? Question { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Answers")]
        public virtual User? User { get; set; }
        [InverseProperty("Answer")]
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
