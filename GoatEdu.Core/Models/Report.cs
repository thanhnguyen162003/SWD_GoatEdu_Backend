using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("reports")]
    public partial class Report : BaseEntity
    {
        [Key]
        [Column("report_id")]
        public Guid ReportId { get; set; }
        [Column("report_title", TypeName = "character varying")]
        public string? ReportTitle { get; set; }
        [Column("report_content", TypeName = "character varying")]
        public string? ReportContent { get; set; }
        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("created_by", TypeName = "character varying")]
        public string? CreatedBy { get; set; }
        [Column("status", TypeName = "character varying")]
        public string? Status { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Reports")]
        public virtual User? User { get; set; }
    }
}
