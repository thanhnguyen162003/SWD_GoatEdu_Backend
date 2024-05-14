using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("flashcards")]
    public partial class Flashcard
    {
        public Flashcard()
        {
            FlashcardContents = new HashSet<FlashcardContent>();
        }

        [Key]
        [Column("flashcard_id")]
        public Guid FlashcardId { get; set; }
        [Column("flashcard_name", TypeName = "character varying")]
        public string? FlashcardName { get; set; }
        [Column("flashcard_description", TypeName = "character varying")]
        public string? FlashcardDescription { get; set; }
        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("tag", TypeName = "character varying")]
        public string? Tag { get; set; }
        [Column("subject_id")]
        public Guid? SubjectId { get; set; }
        [Column("status", TypeName = "character varying")]
        public string? Status { get; set; }
        [Column("star")]
        public int? Star { get; set; }
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
        [InverseProperty("Flashcards")]
        public virtual Subject? Subject { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Flashcards")]
        public virtual User? User { get; set; }
        [InverseProperty("Flashcard")]
        public virtual ICollection<FlashcardContent> FlashcardContents { get; set; }
    }
}
