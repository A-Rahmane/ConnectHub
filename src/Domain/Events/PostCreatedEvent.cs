using Domain.Common;

namespace Domain.Events
{
    public class PostCreatedEvent : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public int PostId { get; }
        public int AuthorId { get; }
        public string Content { get; }

        public PostCreatedEvent(int postId, int authorId, string content)
        {
            PostId = postId;
            AuthorId = authorId;
            Content = content;
        }
    }
}