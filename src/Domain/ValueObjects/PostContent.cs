using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public record PostContent
    {
        private const int MaxLength = 2000;
        private static readonly Regex MentionRegex = new(@"@(\w+)", RegexOptions.Compiled);
        private static readonly Regex HashtagRegex = new(@"#(\w+)", RegexOptions.Compiled);

        public string Value { get; }

        public PostContent(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Post content cannot be empty.", nameof(value));

            if (value.Length > MaxLength)
                throw new ArgumentException($"Post content cannot exceed {MaxLength} characters.", nameof(value));

            Value = value.Trim();
        }

        public IEnumerable<string> ExtractMentions()
        {
            return MentionRegex.Matches(Value)
                .Select(m => m.Groups[1].Value)
                .Distinct();
        }

        public IEnumerable<string> ExtractHashtags()
        {
            return HashtagRegex.Matches(Value)
                .Select(m => m.Groups[1].Value)
                .Distinct();
        }

        public int CharacterCount => Value.Length;
        public int WordCount => Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;

        public static implicit operator string(PostContent content) => content.Value;
        public static implicit operator PostContent(string value) => new(value);

        public override string ToString() => Value;
    }
}