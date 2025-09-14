using System.ComponentModel.DataAnnotations;
using Domain.Common;
using Domain.ValueObjects;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Profile : BaseEntity, IAuditableEntity
    {
        public int UserId { get; private set; }

        [Required]
        [StringLength(100)]
        public DisplayName DisplayName { get; private set; } = string.Empty;

        [StringLength(500)]
        public Bio? Bio { get; private set; }

        [StringLength(255)]
        public string? AvatarUrl { get; private set; }

        [StringLength(255)]
        public string? CoverImageUrl { get; private set; }

        [StringLength(100)]
        public string? Location { get; private set; }

        [StringLength(255)]
        public string? Website { get; private set; }

        public DateTime? DateOfBirth { get; private set; }
        public bool IsPrivate { get; private set; } = false;
        public bool IsVerified { get; private set; } = false;
        public int FollowerCount { get; private set; } = 0;
        public int FollowingCount { get; private set; } = 0;
        public int PostCount { get; private set; } = 0;

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

        // Private constructor for EF Core
        private Profile() { }

        // Factory method
        public static Profile Create(int userId, string displayName, string? bio = null)
        {
            return new Profile
            {
                UserId = userId,
                DisplayName = new DisplayName(displayName),
                Bio = string.IsNullOrWhiteSpace(bio) ? null : new Bio(bio),
                CreatedAt = DateTime.UtcNow
            };
        }

        // Business methods
        public void UpdateProfile(string displayName, string? bio = null, string? location = null, string? website = null)
        {
            DisplayName = new DisplayName(displayName);
            Bio = string.IsNullOrWhiteSpace(bio) ? null : new Bio(bio);
            Location = location?.Trim();
            Website = website?.Trim();
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateAvatar(string avatarUrl)
        {
            AvatarUrl = avatarUrl;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateCoverImage(string coverImageUrl)
        {
            CoverImageUrl = coverImageUrl;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPrivacy(bool isPrivate)
        {
            IsPrivate = isPrivate;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Verify()
        {
            IsVerified = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Unverify()
        {
            IsVerified = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void IncrementFollowerCount()
        {
            FollowerCount++;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DecrementFollowerCount()
        {
            if (FollowerCount > 0)
            {
                FollowerCount--;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void IncrementFollowingCount()
        {
            FollowingCount++;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DecrementFollowingCount()
        {
            if (FollowingCount > 0)
            {
                FollowingCount--;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public void IncrementPostCount()
        {
            PostCount++;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DecrementPostCount()
        {
            if (PostCount > 0)
            {
                PostCount--;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public bool CanBeFollowedBy(Profile follower)
        {
            if (follower.UserId == UserId)
                throw new InvalidFollowException("Users cannot follow themselves.");

            return true; // Additional business rules can be added here
        }

        public void SetDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth > DateTime.Now.AddYears(-13))
                throw new ArgumentException("User must be at least 13 years old.");

            DateOfBirth = dateOfBirth;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}