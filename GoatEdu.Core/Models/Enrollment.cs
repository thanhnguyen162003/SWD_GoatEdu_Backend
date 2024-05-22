using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Enrollment
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? SubjectId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Subject? Subject { get; set; }
        public virtual User? User { get; set; }
        public virtual EnrollmentProcess? EnrollmentProcess { get; set; }
    }
}
