using Domain.Common;

namespace Domain.Events
{
    public class ProfileUpdatedEvent : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public int ProfileId { get; }
        public int UserId { get; }
        public string DisplayName { get; }

        public ProfileUpdatedEvent(int profileId, int userId, string displayName)
        {
            ProfileId = profileId;
            UserId = userId;
            DisplayName = displayName;
        }
    }
}