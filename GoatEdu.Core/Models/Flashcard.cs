using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Flashcard")]
    public partial class Flashcard : BaseEntity
    {
        public Flashcard()
        {
            FlashcardContents = new HashSet<FlashcardContent>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("flashcardName", TypeName = "character varying")]
        public string? FlashcardName { get; set; }
        [Column("flashcardDescription", TypeName = "character varying")]
        public string? FlashcardDescription { get; set; }
        [Column("userId")]
        public Guid? UserId { get; set; }
        [Column("tag", TypeName = "character varying")]
        public string? Tag { get; set; }
        [Column("subjectId")]
        public Guid? SubjectId { get; set; }
        [Column("status", TypeName = "character varying")]
        public string? Status { get; set; }
        [Column("star")]
        public int? Star { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("createdBy", TypeName = "character varying")]
        public string? CreatedBy { get; set; }
        [Column("updatedBy", TypeName = "character varying")]
        public string? UpdatedBy { get; set; }
        [Column("updatedAt", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
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
