using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("notes")]
    public partial class Note : BaseEntity
    {
        [Key]
        [Column("note_id")]
        public Guid NoteId { get; set; }
        [Column("note_name", TypeName = "character varying")]
        public string? NoteName { get; set; }
        [Column("note_body", TypeName = "character varying")]
        public string? NoteBody { get; set; }
        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("created_by", TypeName = "character varying")]
        public string? CreatedBy { get; set; }
        [Column("updated_by", TypeName = "character varying")]
        public string? UpdatedBy { get; set; }
        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Notes")]
        public virtual User? User { get; set; }
    }
}
