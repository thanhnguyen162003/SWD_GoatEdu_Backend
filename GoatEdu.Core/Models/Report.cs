using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Report")]
    public partial class Report : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("reportTitle", TypeName = "character varying")]
        public string? ReportTitle { get; set; }
        [Column("reportContent", TypeName = "character varying")]
        public string? ReportContent { get; set; }
        [Column("userId")]
        public Guid? UserId { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("createdBy", TypeName = "character varying")]
        public string? CreatedBy { get; set; }
        [Column("status", TypeName = "character varying")]
        public string? Status { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Reports")]
        public virtual User? User { get; set; }
    }
}
