using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("theories")]
    public partial class Theory
    {
        public Theory()
        {
            TheoryFlashcardContents = new HashSet<TheoryFlashcardContent>();
        }

        [Key]
        [Column("theory_id")]
        public Guid TheoryId { get; set; }
        [Column("theory_name", TypeName = "character varying")]
        public string? TheoryName { get; set; }
        [Column("file", TypeName = "character varying")]
        public string? File { get; set; }
        [Column("image", TypeName = "character varying")]
        public string? Image { get; set; }
        [Column("theory_content", TypeName = "character varying")]
        public string? TheoryContent { get; set; }
        [Column("lesson_id")]
        public Guid? LessonId { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [Column("isDeleted")]
        public bool? IsDeleted { get; set; }

        [ForeignKey("LessonId")]
        [InverseProperty("Theories")]
        public virtual Lesson? Lesson { get; set; }
        [InverseProperty("Theory")]
        public virtual ICollection<TheoryFlashcardContent> TheoryFlashcardContents { get; set; }
    }
}
