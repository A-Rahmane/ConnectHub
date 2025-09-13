using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Report : BaseEntity, IAuditableEntity
    {
        public int ReportedByUserId { get; set; }
        public int? ReportedUserId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }

        public ReportType Type { get; set; }
        public ReportStatus Status { get; set; } = ReportStatus.Pending;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? AdminNotes { get; set; }

        public int? ReviewedByUserId { get; set; }
        public DateTime? ReviewedAt { get; set; }

        // Audit properties
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        public User ReportedByUser { get; set; } = null!;
        public User? ReportedUser { get; set; }
        public User? ReviewedByUser { get; set; }
        public Post? Post { get; set; }
        public Comment? Comment { get; set; }
    }
}