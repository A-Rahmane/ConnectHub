using System.ComponentModel.DataAnnotations;
using Domain.Common;

namespace Domain.Entities
{
    public class Hashtag : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; }

        public int UsageCount { get; set; } = 0;
        public bool IsBlocked { get; set; } = false;

        // Navigation properties
        public ICollection<PostHashtag> PostHashtags { get; set; } = new List<PostHashtag>();
    }
}