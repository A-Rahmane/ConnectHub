using Domain.Common;

namespace Domain.Events
{
    public class CommentCreatedEvent : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public int CommentId { get; }
        public int PostId { get; }
        public int AuthorId { get; }
        public string Content { get; }

        public CommentCreatedEvent(int commentId, int postId, int authorId, string content)
        {
            CommentId = commentId;
            PostId = postId;
            AuthorId = authorId;
            Content = content;
        }
    }
}
