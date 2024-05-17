using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("FlashcardContent")]
    public partial class FlashcardContent : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("flashcardContentQuestion", TypeName = "character varying")]
        public string? FlashcardContentQuestion { get; set; }
        [Column("flashcardContentAnswer", TypeName = "character varying")]
        public string? FlashcardContentAnswer { get; set; }
        [Column("flashcardId")]
        public Guid? FlashcardId { get; set; }
        [Column("image", TypeName = "character varying")]
        public string? Image { get; set; }
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
        [ForeignKey("FlashcardId")]
        [InverseProperty("FlashcardContents")]
        public virtual Flashcard? Flashcard { get; set; }
    }
}
