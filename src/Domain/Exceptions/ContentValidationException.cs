namespace Domain.Exceptions
{
    public class ContentValidationException : DomainException
    {
        public ContentValidationException(string message) : base(message) { }
    }
}
