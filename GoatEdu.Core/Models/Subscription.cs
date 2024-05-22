using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class Subscription
    {
        public Subscription()
        {
            Transactions = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }
        public string? SubscriptionName { get; set; }
        public string? SubscriptionBody { get; set; }
        public double? Price { get; set; }
        public TimeSpan? Duration { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
