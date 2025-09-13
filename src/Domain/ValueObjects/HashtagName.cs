using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public record HashtagName
    {
        private static readonly Regex HashtagRegex = new(
            @"^[a-zA-Z0-9_]{1,100}$",
            RegexOptions.Compiled);

        public string Value { get; }

        public HashtagName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Hashtag name cannot be empty.", nameof(value));

            // Remove # if present
            var cleanValue = value.StartsWith("#") ? value[1..] : value;

            if (!HashtagRegex.IsMatch(cleanValue))
                throw new ArgumentException(
                    "Hashtag must contain only letters, numbers, and underscores (1-100 characters).",
                    nameof(value));

            Value = cleanValue.ToLowerInvariant();
        }

        public string DisplayValue => $"#{Value}";

        public static implicit operator string(HashtagName hashtag) => hashtag.Value;
        public static implicit operator HashtagName(string value) => new(value);

        public override string ToString() => DisplayValue;
    }
}