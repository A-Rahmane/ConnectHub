namespace Domain.ValueObjects
{
    public record Bio
    {
        private const int MaxLength = 500;
        public string Value { get; }

        public Bio(string value)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > MaxLength)
                throw new ArgumentException($"Bio cannot exceed {MaxLength} characters.");
            Value = value?.Trim() ?? string.Empty;
        }

        public static implicit operator string(Bio bio) => bio.Value;
        public static implicit operator Bio(string value) => new(value);
    }
}
