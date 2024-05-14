using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("transactions")]
    public partial class Transaction
    {
        public Transaction()
        {
            Wallets = new HashSet<Wallet>();
        }

        [Key]
        [Column("transaction_id")]
        public Guid TransactionId { get; set; }
        [Column("transaction_name", TypeName = "character varying")]
        public string? TransactionName { get; set; }
        [Column("note", TypeName = "character varying")]
        public string? Note { get; set; }
        [Column("start_date", TypeName = "timestamp without time zone")]
        public DateTime? StartDate { get; set; }
        [Column("end_date", TypeName = "timestamp without time zone")]
        public DateTime? EndDate { get; set; }
        [Column("subscription_id")]
        public Guid? SubscriptionId { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("isDeleted")]
        public bool? IsDeleted { get; set; }

        [ForeignKey("SubscriptionId")]
        [InverseProperty("Transactions")]
        public virtual Subscription? Subscription { get; set; }
        [InverseProperty("Transaction")]
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
