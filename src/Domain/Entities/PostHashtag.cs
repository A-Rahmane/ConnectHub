using Domain.Common;

namespace Domain.Entities
{
    public class PostHashtag : BaseEntity
    {
        public int PostId { get; set; }
        public int HashtagId { get; set; }

        // Navigation properties
        public Post Post { get; set; } = null!;
        public Hashtag Hashtag { get; set; } = null!;
    }
}