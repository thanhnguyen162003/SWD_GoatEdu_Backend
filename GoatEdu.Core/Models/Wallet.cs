using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Wallet
    {
        public Guid Id { get; set; }
        public double? NumberWallet { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual User IdNavigation { get; set; } = null!;
    }
}
