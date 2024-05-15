using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("subscriptions")]
    public partial class Subscription : BaseEntity
    {
        public Subscription()
        {
            Transactions = new HashSet<Transaction>();
        }

        [Key]
        [Column("subscription_id")]
        public Guid SubscriptionId { get; set; }
        [Column("subscription_name", TypeName = "character varying")]
        public string? SubscriptionName { get; set; }
        [Column("subscription_body", TypeName = "character varying")]
        public string? SubscriptionBody { get; set; }
        [Column("price")]
        public double? Price { get; set; }
        [Column("duration")]
        public TimeSpan? Duration { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [InverseProperty("Subscription")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
