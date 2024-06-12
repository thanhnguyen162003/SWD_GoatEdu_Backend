using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Note")]
    public partial class Note : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("noteName", TypeName = "character varying")]
        public string? NoteName { get; set; }
        [Column("noteBody", TypeName = "character varying")]
        public string? NoteBody { get; set; }
        [Column("noteBodyHtml", TypeName = "character varying")]
        public string? NoteBodyHtml { get; set; }
        [Column("userId")]
        public Guid? UserId { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("createdBy", TypeName = "character varying")]
        public string? CreatedBy { get; set; }
        [Column("updatedBy", TypeName = "character varying")]
        public string? UpdatedBy { get; set; }
        [Column("updatedAt", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Notes")]
        public virtual User? User { get; set; }
    }
}
