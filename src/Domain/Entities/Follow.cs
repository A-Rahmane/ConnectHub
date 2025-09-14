using Domain.Common;
using Domain.Enums;
using Domain.Events;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Follow : AggregateRoot, IAuditableEntity
    {
        public int FollowerId { get; private set; }
        public int FollowingId { get; private set; }
        public FollowStatus Status { get; private set; } = FollowStatus.Active;
        public DateTime? AcceptedAt { get; private set; }

        // Audit properties
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        public Profile Follower { get; set; } = null!;
        public Profile Following { get; set; } = null!;

        // Private constructor for EF Core
        private Follow() { }

        // Factory method for creating follow relationships
        public static Follow Create(int followerId, int followingId, bool requiresApproval = false)
        {
            if (followerId == followingId)
                throw new InvalidFollowException("Users cannot follow themselves.");

            var status = requiresApproval ? FollowStatus.Pending : FollowStatus.Active;
            var acceptedAt = requiresApproval ? null : DateTime.UtcNow;

            var follow = new Follow
            {
                FollowerId = followerId,
                FollowingId = followingId,
                Status = status,
                AcceptedAt = acceptedAt,
                CreatedAt = DateTime.UtcNow
            };

            // Raise appropriate domain event based on status
            if (status == FollowStatus.Active)
            {
                follow.AddDomainEvent(new UserFollowedEvent(followerId, followingId, status));
            }
            else
            {
                follow.AddDomainEvent(new FollowRequestCreatedEvent(followerId, followingId));
            }

            return follow;
        }

        // State transition methods
        public void Accept()
        {
            if (Status != FollowStatus.Pending)
                throw new InvalidOperationException("Only pending follow requests can be accepted.");

            Status = FollowStatus.Active;
            AcceptedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new FollowAcceptedEvent(FollowerId, FollowingId));
        }

        public void Reject()
        {
            if (Status != FollowStatus.Pending)
                throw new InvalidOperationException("Only pending follow requests can be rejected.");

            Status = FollowStatus.Rejected;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new FollowRejectedEvent(FollowerId, FollowingId));
        }

        public void Block()
        {
            Status = FollowStatus.Blocked;
            UpdatedAt = DateTime.UtcNow;

            AddDomainEvent(new FollowBlockedEvent(FollowerId, FollowingId));
        }

        public void Unfollow()
        {
            if (Status == FollowStatus.Blocked)
                throw new InvalidOperationException("Cannot unfollow a blocked relationship.");

            AddDomainEvent(new UserUnfollowedEvent(FollowerId, FollowingId));
        }

        // Query methods
        public bool IsActive => Status == FollowStatus.Active;
        public bool IsPending => Status == FollowStatus.Pending;
        public bool IsBlocked => Status == FollowStatus.Blocked;
        public bool IsRejected => Status == FollowStatus.Rejected;
    }
}