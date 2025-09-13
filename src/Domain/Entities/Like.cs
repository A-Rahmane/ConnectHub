using Domain.Common;

namespace Domain.Entities
{
    public class Like : BaseEntity
    {
        public int ProfileId { get; set; }

        // Either PostId or CommentId will be set, not both
        public int? PostId { get; set; }
        public int? CommentId { get; set; }

        // Navigation properties
        public Profile Profile { get; set; } = null!;
        public Post? Post { get; set; }
        public Comment? Comment { get; set; }
    }
}