using Domain.Entities;

namespace Domain.Repositories
{
    public interface IFollowRepository : IBaseRepository<Follow>
    {
        Task<Follow?> GetFollowRelationshipAsync(int followerId, int followingId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Follow>> GetFollowersAsync(int userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Follow>> GetFollowingAsync(int userId, CancellationToken cancellationToken = default);
        Task<int> GetFollowerCountAsync(int userId, CancellationToken cancellationToken = default);
        Task<int> GetFollowingCountAsync(int userId, CancellationToken cancellationToken = default);
    }
}
