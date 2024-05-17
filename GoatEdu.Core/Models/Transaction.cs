using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Transaction")]
    public partial class Transaction : BaseEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("transactionName", TypeName = "character varying")]
        public string? TransactionName { get; set; }
        [Column("note", TypeName = "character varying")]
        public string? Note { get; set; }
        [Column("walletId")]
        public Guid? WalletId { get; set; }
        [Column("startDate", TypeName = "timestamp without time zone")]
        public DateTime? StartDate { get; set; }
        [Column("endDate", TypeName = "timestamp without time zone")]
        public DateTime? EndDate { get; set; }
        [Column("subscriptionId")]
        public Guid? SubscriptionId { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [ForeignKey("SubscriptionId")]
        [InverseProperty("Transactions")]
        public virtual Subscription? Subscription { get; set; }
    }
}
