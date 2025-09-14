using Domain.Common;

namespace Domain.Events
{
    public class CommentUnlikedEvent : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public int CommentId { get; }
        public int UserId { get; }

        public CommentUnlikedEvent(int commentId, int userId)
        {
            CommentId = commentId;
            UserId = userId;
        }
    }
}