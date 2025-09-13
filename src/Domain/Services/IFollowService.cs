namespace Domain.Services
{
    public interface IFollowService
    {
        Task<bool> CanUserFollowAsync(int followerId, int followingId);
        Task<FollowResult> ProcessFollowRequestAsync(int followerId, int followingId);
        Task<bool> IsUserFollowingAsync(int followerId, int followingId);
    }
}