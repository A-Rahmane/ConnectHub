namespace Domain.Services
{
    public interface IContentModerationService
    {
        Task<ModerationResult> ModeratePostContentAsync(string content);
        Task<ModerationResult> ModerateCommentContentAsync(string content);
        Task<bool> IsContentSafeAsync(string content);
    }
}