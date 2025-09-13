namespace Domain.ValueObjects
{
    public record MediaDimensions
    {
        public int Width { get; }
        public int Height { get; }

        public MediaDimensions(int width, int height)
        {
            if (width < 0)
                throw new ArgumentException("Width cannot be negative.", nameof(width));
            if (height < 0)
                throw new ArgumentException("Height cannot be negative.", nameof(height));

            Width = width;
            Height = height;
        }

        public double AspectRatio => Height == 0 ? 0 : (double)Width / Height;
        public bool IsLandscape => Width > Height;
        public bool IsPortrait => Height > Width;
        public bool IsSquare => Width == Height;

        public override string ToString() => $"{Width}x{Height}";
    }
}