using Domain.Common;

namespace Domain.Events
{
    public class FollowRequestCreatedEvent : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public int FollowerId { get; }
        public int FollowingId { get; }

        public FollowRequestCreatedEvent(int followerId, int followingId)
        {
            FollowerId = followerId;
            FollowingId = followingId;
        }
    }
}