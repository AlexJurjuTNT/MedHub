namespace Medhub_Backend.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(int userId) : base($"User with id {userId} not found")
    {
    }

    public UserNotFoundException(string username) : base($"User with username {username} not found")
    {
    }
}