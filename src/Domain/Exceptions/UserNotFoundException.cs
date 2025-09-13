namespace Domain.Exceptions
{
    public class UserNotFoundException : DomainException
    {
        public UserNotFoundException(int userId)
            : base($"User with ID {userId} was not found") { }

        public UserNotFoundException(string identifier)
            : base($"User with identifier '{identifier}' was not found") { }
    }
}
