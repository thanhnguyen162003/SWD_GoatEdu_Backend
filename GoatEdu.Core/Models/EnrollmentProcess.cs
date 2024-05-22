using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class EnrollmentProcess
    {
        public Guid Id { get; set; }
        public Guid? EnrollmentId { get; set; }
        public int? Process { get; set; }
        public Guid? ChapterId { get; set; }
        public string? Status { get; set; }

        public virtual Enrollment? Enrollment { get; set; }
    }
}
