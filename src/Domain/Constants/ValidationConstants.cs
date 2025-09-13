namespace Domain.Constants
{
    public static class ValidationConstants
    {
        public static class User
        {
            public const string UsernameRegexPattern = @"^[a-zA-Z0-9._-]{3,50}$";
            public const string EmailRegexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        }

        public static class Media
        {
            public static readonly string[] AllowedImageTypes = { "image/jpeg", "image/png", "image/gif", "image/webp" };
            public static readonly string[] AllowedVideoTypes = { "video/mp4", "video/avi", "video/mov" };
            public const long MaxFileSizeBytes = 100 * 1024 * 1024; // 100MB
        }
    }
}
