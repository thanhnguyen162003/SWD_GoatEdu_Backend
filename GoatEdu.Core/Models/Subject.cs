using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Subject")]
    public partial class Subject : BaseEntity
    {
        public Subject()
        {
            Chapters = new HashSet<Chapter>();
            Discussions = new HashSet<Discussion>();
            Enrollments = new HashSet<Enrollment>();
            Flashcards = new HashSet<Flashcard>();
            Quizzes = new HashSet<Quiz>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("subjectName", TypeName = "character varying")]
        public string? SubjectName { get; set; }
        [Column("subjectCode", TypeName = "character varying")]
        public string? SubjectCode { get; set; }
        [Column("information", TypeName = "character varying")]
        public string? Information { get; set; }
        [Column("class", TypeName = "character varying")]
        public string? Class { get; set; }
        [Column("image", TypeName = "character varying")]
        public string? Image { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updatedAt", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<Chapter> Chapters { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Discussion> Discussions { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Flashcard> Flashcards { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
