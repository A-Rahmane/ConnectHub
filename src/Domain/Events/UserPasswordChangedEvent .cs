using Domain.Common;

namespace Domain.Events
{
    public class UserPasswordChangedEvent : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public int UserId { get; }
        public string Username { get; }

        public UserPasswordChangedEvent(int userId, string username)
        {
            UserId = userId;
            Username = username;
        }
    }
}