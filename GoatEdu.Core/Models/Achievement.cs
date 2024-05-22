using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Achievement
    {
        public Guid Id { get; set; }
        public string? AchievementName { get; set; }
        public string? AchievementContent { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual User? User { get; set; }
    }
}
