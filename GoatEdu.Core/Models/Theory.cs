using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Theory")]
    public partial class Theory : BaseEntity
    {
        public Theory()
        {
            TheoryFlashCardContents = new HashSet<TheoryFlashCardContent>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("theoryName", TypeName = "character varying")]
        public string? TheoryName { get; set; }
        [Column("file", TypeName = "character varying")]
        public string? File { get; set; }
        [Column("image", TypeName = "character varying")]
        public string? Image { get; set; }
        [Column("theoryContent", TypeName = "character varying")]
        public string? TheoryContent { get; set; }
        [Column("lessonId")]
        public Guid? LessonId { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updatedAt", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("LessonId")]
        [InverseProperty("Theories")]
        public virtual Lesson? Lesson { get; set; }
        [InverseProperty("Theory")]
        public virtual ICollection<TheoryFlashCardContent> TheoryFlashCardContents { get; set; }
    }
}
