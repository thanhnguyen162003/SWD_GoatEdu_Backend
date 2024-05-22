using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Report
    {
        public Guid Id { get; set; }
        public string? ReportTitle { get; set; }
        public string? ReportContent { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? Status { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual User? User { get; set; }
    }
}
