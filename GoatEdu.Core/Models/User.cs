using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    [Table("User")]
    public partial class User : BaseEntity
    {
        public User()
        {
            Achievements = new HashSet<Achievement>();
            Answers = new HashSet<Answer>();
            Discussions = new HashSet<Discussion>();
            Enrollments = new HashSet<Enrollment>();
            Flashcards = new HashSet<Flashcard>();
            Notes = new HashSet<Note>();
            Notifications = new HashSet<Notification>();
            Reports = new HashSet<Report>();
            Votes = new HashSet<Vote>();
        }

        [Key]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("username", TypeName = "character varying")]
        public string? Username { get; set; }
        [Column("fullname", TypeName = "character varying")]
        public string? Fullname { get; set; }
        [Column("image", TypeName = "character varying")]
        public string? Image { get; set; }
        [Column("password", TypeName = "character varying")]
        public string? Password { get; set; }
        [Column("email", TypeName = "character varying")]
        public string? Email { get; set; }
        [Column("phoneNumber", TypeName = "character varying")]
        public string? PhoneNumber { get; set; }
        [Column("subscription", TypeName = "character varying")]
        public string? Subscription { get; set; }
        [Column("subscriptionEnd", TypeName = "timestamp without time zone")]
        public DateTime? SubscriptionEnd { get; set; }
        [Column("provider", TypeName = "character varying")]
        public string? Provider { get; set; }
        [Column("emailVerify")]
        public bool? EmailVerify { get; set; }
        [Column("roleId")]
        public Guid? RoleId { get; set; }
        [Column("walletId")]
        public Guid? WalletId { get; set; }
        [Column("createdAt", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updatedAt", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [Column("isNewUser")]
        public bool? IsNewUser { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("Users")]
        public virtual Role? Role { get; set; }
        [ForeignKey("WalletId")]
        [InverseProperty("User")]
        public virtual Wallet? Wallet { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Achievement> Achievements { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Answer> Answers { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Discussion> Discussions { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Flashcard> Flashcards { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Note> Notes { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Notification> Notifications { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Report> Reports { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Vote> Votes { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Rate> Rates { get; set; }
    }
}
