using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Chapter")]
    public partial class Chapter : BaseEntity
    {
        public Chapter()
        {
            Lessons = new HashSet<Lesson>();
            Quizzes = new HashSet<Quiz>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("chapterName", TypeName = "character varying")]
        public string? ChapterName { get; set; }
        [Column("chapterLevel")]
        public int? ChapterLevel { get; set; }
        [Column("subjectId")]
        public Guid? SubjectId { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("createdBy", TypeName = "character varying")]
        public string? CreatedBy { get; set; }
        [Column("updatedAt", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("SubjectId")]
        [InverseProperty("Chapters")]
        public virtual Subject? Subject { get; set; }
        [InverseProperty("Chapter")]
        public virtual ICollection<Lesson> Lessons { get; set; }
        [InverseProperty("Chapter")]
        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
