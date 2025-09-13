using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public record Username
    {
        private static readonly Regex UsernameRegex = new(
            @"^[a-zA-Z0-9._-]{3,50}$",
            RegexOptions.Compiled);

        public string Value { get; }

        public Username(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Username cannot be empty.", nameof(value));

            if (!UsernameRegex.IsMatch(value))
                throw new ArgumentException(
                    "Username must be 3-50 characters and contain only letters, numbers, dots, underscores, and hyphens.",
                    nameof(value));

            Value = value.ToLowerInvariant();
        }

        public static implicit operator string(Username username) => username.Value;
        public static implicit operator Username(string value) => new(value);

        public override string ToString() => Value;
    }
}