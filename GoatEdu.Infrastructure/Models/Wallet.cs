using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("wallets")]
    public partial class Wallet
    {
        [Key]
        [Column("wallet_id")]
        public Guid WalletId { get; set; }
        [Column("numberwallet")]
        public double? Numberwallet { get; set; }
        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("transaction_id")]
        public Guid? TransactionId { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [Column("isDeleted")]
        public bool? IsDeleted { get; set; }

        [ForeignKey("TransactionId")]
        [InverseProperty("Wallets")]
        public virtual Transaction? Transaction { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Wallets")]
        public virtual User? User { get; set; }
    }
}
