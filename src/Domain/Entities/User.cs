using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class User : AggregateRoot, IAuditableEntity, ISoftDeletable
    {
        [Required]
        [StringLength(50)]
        public Username Username { get; set; } = null;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public Email Email { get; set; } = null;

        [Required]
        public Password Password { get; set; } = null;

        public string? SecurityStamp { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime? EmailVerifiedAt { get; set; }
        public UserStatus Status { get; set; } = UserStatus.Active;
        public bool TwoFactorEnabled { get; set; } = false;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

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
    }
}