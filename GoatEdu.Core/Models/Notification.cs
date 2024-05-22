using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Notification
    {
        public Guid Id { get; set; }
        public string? NotificationName { get; set; }
        public string? NotificationMessage { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? ReadAt { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User? User { get; set; }
    }
}
