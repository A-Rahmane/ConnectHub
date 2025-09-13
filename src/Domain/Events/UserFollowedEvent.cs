using Domain.Common;
using Domain.Enums;

namespace Domain.Events
{
    public class UserFollowedEvent : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public int FollowerId { get; }
        public int FollowingId { get; }
        public FollowStatus Status { get; }

        public UserFollowedEvent(int followerId, int followingId, FollowStatus status)
        {
            FollowerId = followerId;
            FollowingId = followingId;
            Status = status;
        }
    }
}
