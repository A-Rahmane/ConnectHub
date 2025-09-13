namespace Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        IUserRepository Users { get; }
        IProfileRepository Profiles { get; }
        IFollowRepository Follows { get; }
        IPostRepository Posts { get; }
        ICommentRepository Comments { get; }
        // ... other repositories
    }
}
