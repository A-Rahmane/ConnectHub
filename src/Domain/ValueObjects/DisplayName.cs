namespace Domain.ValueObjects
{
    public record DisplayName
    {
        private const int MaxLength = 100;
        private const int MinLength = 1;
        public string Value { get; }

        public DisplayName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Display name cannot be empty.");
            if (value.Length > MaxLength)
                throw new ArgumentException($"Display name cannot exceed {MaxLength} characters.");
            Value = value.Trim();
        }

        public static implicit operator string(DisplayName name) => name.Value;
        public static implicit operator DisplayName(string value) => new(value);
    }
}
