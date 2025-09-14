using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Post : AggregateRoot, IAuditableEntity, ISoftDeletable
    {
        public int AuthorId { get; set; }

        [Required]
        [StringLength(2000)]
        public PostContent Content { get; set; } = new PostContent(string.Empty);

        public bool IsPublished { get; set; } = true;
        public int ViewCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public int ShareCount { get; set; } = 0;

        // For reposted content
        public int? OriginalPostId { get; set; }
        public bool IsRepost { get; set; } = false;

        [StringLength(500)]
        public string? RepostComment { get; set; }

        // Audit properties
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Soft delete properties
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        // Navigation properties
        public Profile Author { get; set; } = null!;
        public Post? OriginalPost { get; set; }
        public ICollection<Post> Reposts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Media> MediaFiles { get; set; } = new List<Media>();
        public ICollection<PostHashtag> PostHashtags { get; set; } = new List<PostHashtag>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}