using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Media : BaseEntity, IAuditableEntity
    {
        public int? PostId { get; set; }
        public int UploadedByProfileId { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string FilePath { get; set; } = string.Empty;

        [StringLength(500)]
        public string? ThumbnailPath { get; set; }

        [Required]
        [StringLength(100)]
        public string MimeType { get; set; } = string.Empty;

        public long FileSize { get; set; }
        public MediaType MediaType { get; set; }
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public int Duration { get; set; } = 0; // For videos/audio in seconds

        [StringLength(255)]
        public string? AltText { get; set; }

        public int DisplayOrder { get; set; } = 0;

        // Audit properties
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        public Post? Post { get; set; }
        public Profile UploadedBy { get; set; } = null!;
    }
}