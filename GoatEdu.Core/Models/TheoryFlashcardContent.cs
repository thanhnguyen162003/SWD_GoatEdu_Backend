using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("theory_flashcard_content")]
    public partial class TheoryFlashcardContent : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("question", TypeName = "character varying")]
        public string? Question { get; set; }
        [Column("answer", TypeName = "character varying")]
        public string? Answer { get; set; }
        [Column("theory_id")]
        public Guid? TheoryId { get; set; }
        [Column("status", TypeName = "character varying")]
        public string? Status { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("TheoryId")]
        [InverseProperty("TheoryFlashcardContents")]
        public virtual Theory? Theory { get; set; }
    }
}
