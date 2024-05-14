using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("chapters")]
    public partial class Chapter
    {
        public Chapter()
        {
            Lessons = new HashSet<Lesson>();
        }

        [Key]
        [Column("chapter_id")]
        public Guid ChapterId { get; set; }
        [Column("chapter_name", TypeName = "character varying")]
        public string? ChapterName { get; set; }
        [Column("chapter_level")]
        public int? ChapterLevel { get; set; }
        [Column("subject_id")]
        public Guid? SubjectId { get; set; }
        [Column("isDeleted")]
        public bool? IsDeleted { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("created_by", TypeName = "character varying")]
        public string? CreatedBy { get; set; }
        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("SubjectId")]
        [InverseProperty("Chapters")]
        public virtual Subject? Subject { get; set; }
        [InverseProperty("Chapter")]
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
