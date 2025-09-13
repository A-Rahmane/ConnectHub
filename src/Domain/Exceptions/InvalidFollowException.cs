namespace Domain.Exceptions
{
    public class InvalidFollowException : DomainException
    {
        public InvalidFollowException(string message) : base(message)
        {
        }
    }
}