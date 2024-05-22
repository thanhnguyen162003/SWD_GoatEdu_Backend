using System;
using System.Collections.Generic;

namespace GoatEdu.Core.Models
{
    public partial class User
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

        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Fullname { get; set; }
        public string? Image { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Subscription { get; set; }
        public DateTime? SubscriptionEnd { get; set; }
        public string? Provider { get; set; }
        public bool? EmailVerify { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? WalletId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Role? Role { get; set; }
        public virtual Wallet? Wallet { get; set; }
        public virtual ICollection<Achievement> Achievements { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Discussion> Discussions { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<Flashcard> Flashcards { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
