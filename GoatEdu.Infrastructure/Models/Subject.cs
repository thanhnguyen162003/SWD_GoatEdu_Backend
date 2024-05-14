using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("subjects")]
    public partial class Subject
    {
        public Subject()
        {
            Chapters = new HashSet<Chapter>();
            Discussions = new HashSet<Discussion>();
            Flashcards = new HashSet<Flashcard>();
            UserSubjects = new HashSet<UserSubject>();
        }

        [Key]
        [Column("subject_id")]
        public Guid SubjectId { get; set; }
        [Column("subject_name", TypeName = "character varying")]
        public string? SubjectName { get; set; }
        [Column("subject_code", TypeName = "character varying")]
        public string? SubjectCode { get; set; }
        [Column("information", TypeName = "character varying")]
        public string? Information { get; set; }
        [Column("class", TypeName = "character varying")]
        public string? Class { get; set; }
        [Column("image", TypeName = "character varying")]
        public string? Image { get; set; }
        [Column("isDeleted")]
        public bool? IsDeleted { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<Chapter> Chapters { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Discussion> Discussions { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Flashcard> Flashcards { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<UserSubject> UserSubjects { get; set; }
    }
}
