using Domain.Common;

namespace Domain.Events
{
    public class PostLikedEvent : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public int PostId { get; }
        public int UserId { get; }
        public int AuthorId { get; }

        public PostLikedEvent(int postId, int userId, int authorId)
        {
            PostId = postId;
            UserId = userId;
            AuthorId = authorId;
        }
    }
}