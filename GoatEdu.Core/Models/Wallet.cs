using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("Wallet")]
    public partial class Wallet : BaseEntity
    {
        public Wallet()
        {
            Users = new HashSet<User>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("numberWallet")]
        public double? NumberWallet { get; set; }
        [Column("userId")]
        public Guid? UserId { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updatedAt", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [InverseProperty("Wallet")]
        public virtual ICollection<User> Users { get; set; }
    }
}
