namespace Medhub_Backend.Domain.Exceptions;

public class PasswordMismatchException : Exception
{
    public PasswordMismatchException(string message) : base(message)
    {
    }
}