using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("flashcard_contents")]
    public partial class FlashcardContent
    {
        [Key]
        [Column("flashcard_content_id")]
        public Guid FlashcardContentId { get; set; }
        [Column("flashcard_content_question", TypeName = "character varying")]
        public string? FlashcardContentQuestion { get; set; }
        [Column("flashcard_content_answer", TypeName = "character varying")]
        public string? FlashcardContentAnswer { get; set; }
        [Column("flashcard_id")]
        public Guid? FlashcardId { get; set; }
        [Column("image", TypeName = "character varying")]
        public string? Image { get; set; }
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

        [ForeignKey("FlashcardId")]
        [InverseProperty("FlashcardContents")]
        public virtual Flashcard? Flashcard { get; set; }
    }
}
