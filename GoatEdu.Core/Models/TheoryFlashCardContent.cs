using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("TheoryFlashCardContent")]
    public partial class TheoryFlashCardContent : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("question", TypeName = "character varying")]
        public string? Question { get; set; }
        [Column("answer", TypeName = "character varying")]
        public string? Answer { get; set; }
        [Column("theoryId")]
        public Guid? TheoryId { get; set; }
        [Column("status", TypeName = "character varying")]
        public string? Status { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updatedAt", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("TheoryId")]
        [InverseProperty("TheoryFlashCardContents")]
        public virtual Theory? Theory { get; set; }
    }
}
