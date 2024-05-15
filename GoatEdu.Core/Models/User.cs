using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    [Table("users")]
    public partial class User : BaseEntity
    {
        public User()
        {
            Achievements = new HashSet<Achievement>();
            Answers = new HashSet<Answer>();
            Discussions = new HashSet<Discussion>();
            Flashcards = new HashSet<Flashcard>();
            Notes = new HashSet<Note>();
            Notifications = new HashSet<Notification>();
            Reports = new HashSet<Report>();
            UserSubjects = new HashSet<UserSubject>();
        }

        [Key]
        [Column("user_id")]
        public Guid UserId { get; set; }
        [Column("username", TypeName = "character varying")]
        public string? Username { get; set; }
        [Column("password", TypeName = "character varying")]
        public string? Password { get; set; }
        [Column("email", TypeName = "character varying")]
        public string? Email { get; set; }
        [Column("phone_number", TypeName = "character varying")]
        public string? PhoneNumber { get; set; }
        [Column("subscription", TypeName = "character varying")]
        public string? Subscription { get; set; }
        [Column("subscription_end", TypeName = "timestamp without time zone")]
        public DateTime? SubscriptionEnd { get; set; }
        [Column("role_id")]
        public Guid? RoleId { get; set; }
        [Column("created_at", TypeName = "timestamp without time zone")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "timestamp without time zone")]
        public DateTime? UpdatedAt { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("Users")]
        public virtual Role? Role { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Achievement> Achievements { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Answer> Answers { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Discussion> Discussions { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Flashcard> Flashcards { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Note> Notes { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Notification> Notifications { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<Report> Reports { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserSubject> UserSubjects { get; set; }
        
        [InverseProperty("User")]
        public virtual Wallet Wallet { get; set; }
    }
}
