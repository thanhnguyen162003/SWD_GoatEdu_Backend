using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("wallets")]
    public partial class Wallet : BaseEntity
    {
        public Wallet()
        {
            Transactions = new HashSet<Transaction>();
        }
        [Key]
        [Column("wallet_id")]
        public Guid WalletId { get; set; }
        [Column("numberwallet")]
        public double? Numberwallet { get; set; }
        [Column("user_id")]
        public Guid? UserId { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Wallet")]
        public virtual User User { get; set; }
        
        [InverseProperty("Wallet")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
