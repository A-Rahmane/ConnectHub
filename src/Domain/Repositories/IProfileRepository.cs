using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProfileRepository : IBaseRepository<Profile>
    {
        Task<Profile?> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default);
        Task<Profile?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
        Task<IEnumerable<Profile>> SearchProfilesAsync(string searchTerm, CancellationToken cancellationToken = default);
    }
}
