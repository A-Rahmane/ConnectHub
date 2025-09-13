namespace Domain.ValueObjects
{
    public record Password
    {
        private const int MinLength = 8;
        private const int MaxLength = 128;

        public string Hash { get; }

        private Password(string hash)
        {
            Hash = hash;
        }

        public static Password FromPlainText(string plainText)
        {
            if (string.IsNullOrWhiteSpace(plainText))
                throw new ArgumentException("Password cannot be empty.", nameof(plainText));

            if (plainText.Length < MinLength)
                throw new ArgumentException($"Password must be at least {MinLength} characters long.", nameof(plainText));

            if (plainText.Length > MaxLength)
                throw new ArgumentException($"Password cannot exceed {MaxLength} characters.", nameof(plainText));

            var hash = BCrypt.Net.BCrypt.HashPassword(plainText);
            return new Password(hash);
        }

        public static Password FromHash(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
                throw new ArgumentException("Password hash cannot be empty.", nameof(hash));

            return new Password(hash);
        }

        public bool Verify(string plainText)
        {
            return BCrypt.Net.BCrypt.Verify(plainText, Hash);
        }

        public static implicit operator string(Password password) => password.Hash;
    }
}