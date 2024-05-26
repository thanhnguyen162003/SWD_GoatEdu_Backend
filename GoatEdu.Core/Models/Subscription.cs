using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Subscription")]
    public partial class Subscription : BaseEntity
    {
        public Subscription()
        {
            Transactions = new HashSet<Transaction>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("subscriptionName", TypeName = "character varying")]
        public string? SubscriptionName { get; set; }
        [Column("subscriptionBody", TypeName = "character varying")]
        public string? SubscriptionBody { get; set; }
        [Column("price")]
        public double? Price { get; set; }
        [Column("duration")]
        public TimeSpan? Duration { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [InverseProperty("Subscription")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
