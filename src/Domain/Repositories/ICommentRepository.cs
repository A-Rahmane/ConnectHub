using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetByPostIdAsync(int postId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Comment>> GetRepliesAsync(int parentCommentId, CancellationToken cancellationToken = default);
        Task<int> GetReplyCountAsync(int commentId, CancellationToken cancellationToken = default);
    }
}
