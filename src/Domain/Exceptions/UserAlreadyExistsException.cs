namespace Domain.Exceptions
{
    public class UserAlreadyExistsException : DomainException
    {
        public UserAlreadyExistsException(string identifier)
            : base($"User with identifier '{identifier}' already exists")
        {
        }
    }
}