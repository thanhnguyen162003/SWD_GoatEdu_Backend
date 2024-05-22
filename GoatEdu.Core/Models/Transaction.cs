using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Transaction
    {
        public Guid Id { get; set; }
        public string? TransactionName { get; set; }
        public string? Note { get; set; }
        public Guid? WalletId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? SubscriptionId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Subscription? Subscription { get; set; }
    }
}
