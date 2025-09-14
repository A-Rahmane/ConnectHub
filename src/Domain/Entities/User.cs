using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;
using Domain.Events;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class User : AggregateRoot, IAuditableEntity, ISoftDeletable
    {
        [Required]
        [StringLength(50)]
        public Username Username { get; private set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public Email Email { get; private set; } = null!;

        [Required]
        public Password Password { get; private set; } = null!;

        public string? SecurityStamp { get; private set; }
        public DateTime? LastLoginAt { get; private set; }
        public DateTime? EmailVerifiedAt { get; private set; }
        public UserStatus Status { get; private set; } = UserStatus.Active;
        public bool TwoFactorEnabled { get; private set; } = false;
        public string? RefreshToken { get; private set; }
        public DateTime? RefreshTokenExpiryTime { get; private set; }

        // Audit properties
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Soft delete properties
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        // Navigation properties
        public Profile? Profile { get; set; }
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();
        public ICollection<Report> ReportedByReports { get; set; } = new List<Report>();

        // Private constructor for EF Core
        private User() { }

        // Factory method for creating users
        public static User Create(string username, string email, string password)
        {
            var user = new User
            {
                Username = new Username(username),
                Email = new Email(email),
                Password = Password.FromPlainText(password),
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow
            };

            user.AddDomainEvent(new UserCreatedEvent(user.Id, username, email));
            return user;
        }

        // Business methods
        public void ChangePassword(string currentPassword, string newPassword)
        {
            if (!Password.Verify(currentPassword))
                throw new InvalidOperationException("Current password is incorrect.");

            Password = Password.FromPlainText(newPassword);
            SecurityStamp = Guid.NewGuid().ToString();
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new UserPasswordChangedEvent(Id, Username.Value));
        }

        public void UpdateEmail(string newEmail)
        {
            if (Email.Value.Equals(newEmail, StringComparison.OrdinalIgnoreCase))
                return;

            Email = new Email(newEmail);
            EmailVerifiedAt = null; // Reset verification when email changes
            SecurityStamp = Guid.NewGuid().ToString();
            UpdatedAt = DateTime.UtcNow;
        }

        public void VerifyEmail()
        {
            EmailVerifiedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            if (Status == UserStatus.Active)
                return;

            Status = UserStatus.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Suspend(string reason)
        {
            Status = UserStatus.Suspended;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Ban(string reason)
        {
            Status = UserStatus.Banned;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateLastLogin()
        {
            LastLoginAt = DateTime.UtcNow;
        }

        public void SetRefreshToken(string token, DateTime expiry)
        {
            RefreshToken = token;
            RefreshTokenExpiryTime = expiry;
        }

        public void ClearRefreshToken()
        {
            RefreshToken = null;
            RefreshTokenExpiryTime = null;
        }

        public bool IsEmailVerified() => EmailVerifiedAt.HasValue;
        public bool IsActive() => Status == UserStatus.Active && !IsDeleted;
        public bool CanLogin() => IsActive() && IsEmailVerified();
    }
}