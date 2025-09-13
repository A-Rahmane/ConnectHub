namespace Domain.ValueObjects
{
    public record FileSize
    {
        private const long MaxFileSize = 100 * 1024 * 1024; // 100MB

        public long Bytes { get; }

        public FileSize(long bytes)
        {
            if (bytes < 0)
                throw new ArgumentException("File size cannot be negative.", nameof(bytes));

            if (bytes > MaxFileSize)
                throw new ArgumentException($"File size cannot exceed {MaxFileSize / 1024 / 1024}MB.", nameof(bytes));

            Bytes = bytes;
        }

        public double Kilobytes => Bytes / 1024.0;
        public double Megabytes => Bytes / 1024.0 / 1024.0;
        public double Gigabytes => Bytes / 1024.0 / 1024.0 / 1024.0;

        public string ToHumanReadable()
        {
            return Bytes switch
            {
                < 1024 => $"{Bytes} B",
                < 1024 * 1024 => $"{Kilobytes:F1} KB",
                < 1024 * 1024 * 1024 => $"{Megabytes:F1} MB",
                _ => $"{Gigabytes:F1} GB"
            };
        }

        public static implicit operator long(FileSize fileSize) => fileSize.Bytes;
        public static implicit operator FileSize(long bytes) => new(bytes);

        public override string ToString() => ToHumanReadable();
    }
}