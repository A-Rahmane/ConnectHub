using Domain.Common;

namespace Domain.Events
{
    public class CommentLikedEvent : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public int CommentId { get; }
        public int UserId { get; }

        public CommentLikedEvent(int commentId, int userId)
        {
            CommentId = commentId;
            UserId = userId;
        }
    }
}