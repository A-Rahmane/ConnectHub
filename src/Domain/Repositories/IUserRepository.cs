using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> IsUsernameUniqueAsync(string username, int? excludeUserId = null, CancellationToken cancellationToken = default);
        Task<bool> IsEmailUniqueAsync(string email, int? excludeUserId = null, CancellationToken cancellationToken = default);
    }
}