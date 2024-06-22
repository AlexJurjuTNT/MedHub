namespace Medhub_Backend.Domain.Exceptions;

public class ClinicNotFoundException : Exception
{
    public ClinicNotFoundException(string message) : base(message)
    {
    }
}