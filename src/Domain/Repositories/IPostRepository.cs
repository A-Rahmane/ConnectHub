using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<IEnumerable<Post>> GetByAuthorIdAsync(int authorId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Post>> GetTimelineAsync(int userId, int page, int pageSize, CancellationToken cancellationToken = default);
        Task<IEnumerable<Post>> GetTrendingPostsAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    }
}