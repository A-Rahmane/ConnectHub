using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Follow : BaseEntity, IAuditableEntity
    {
        public int FollowerId { get; set; }
        public int FollowingId { get; set; }
        public FollowStatus Status { get; set; } = FollowStatus.Active;
        public DateTime? AcceptedAt { get; set; }

        // Audit properties
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        // Navigation properties
        public Profile Follower { get; set; } = null!;
        public Profile Following { get; set; } = null!;
    }
}