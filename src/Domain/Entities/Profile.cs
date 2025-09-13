using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Profile : BaseEntity, IAuditableEntity
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public DisplayName DisplayName { get; set; } = string.Empty;

        [StringLength(500)]
        public Bio? Bio { get; set; }

        [StringLength(255)]
        public string? AvatarUrl { get; set; }

        [StringLength(255)]
        public string? CoverImageUrl { get; set; }

        [StringLength(100)]
        public string? Location { get; set; }

        [StringLength(255)]
        public string? Website { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public bool IsPrivate { get; set; } = false;
        public bool IsVerified { get; set; } = false;
        public int FollowerCount { get; set; } = 0;
        public int FollowingCount { get; set; } = 0;
        public int PostCount { get; set; } = 0;

        // Audit properties
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        public User User { get; set; } = null!;
        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Follow> Followers { get; set; } = new List<Follow>();
        public ICollection<Follow> Following { get; set; } = new List<Follow>();
    }
}