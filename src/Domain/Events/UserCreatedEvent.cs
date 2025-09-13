using Domain.Common;

namespace Domain.Events
{
    public class UserCreatedEvent : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public int UserId { get; }
        public string Username { get; }
        public string Email { get; }

        public UserCreatedEvent(int userId, string username, string email)
        {
            UserId = userId;
            Username = username;
            Email = email;
        }
    }
}