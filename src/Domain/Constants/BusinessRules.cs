namespace Domain.Constants
{
    public static class BusinessRules
    {
        public const int MaxPostContentLength = 2000;
        public const int MaxCommentContentLength = 1000;
        public const int MaxUsernameLength = 50;
        public const int MinUsernameLength = 3;
        public const int MaxDisplayNameLength = 100;
        public const int MaxBioLength = 500;
        public const int MaxHashtagsPerPost = 10;
        public const int MaxMediaFilesPerPost = 4;
    }
}