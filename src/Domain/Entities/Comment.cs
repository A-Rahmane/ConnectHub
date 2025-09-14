using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities
{
    public class Comment : AggregateRoot, IAuditableEntity, ISoftDeletable
    {
        public int PostId { get; set; }
        public int AuthorId { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; } = string.Empty;

        // For threaded comments
        public int? ParentCommentId { get; set; }
        public int Level { get; set; } = 0;
        public int LikeCount { get; set; } = 0;
        public int ReplyCount { get; set; } = 0;

        // Audit properties
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Soft delete properties
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        // Navigation properties
        public Post Post { get; set; } = null!;
        public Profile Author { get; set; } = null!;
        public Comment? ParentComment { get; set; }
        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}