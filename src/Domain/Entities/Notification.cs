using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Notification : BaseEntity
    {
        public int UserId { get; set; }
        public int? ActorId { get; set; } // Profile who triggered the notification
        public NotificationType Type { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Data { get; set; } // JSON data for additional context

        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }

        // Related entity references
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public int? FollowId { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public Profile? Actor { get; set; }
        public Post? Post { get; set; }
        public Comment? Comment { get; set; }
        public Follow? Follow { get; set; }
    }
}