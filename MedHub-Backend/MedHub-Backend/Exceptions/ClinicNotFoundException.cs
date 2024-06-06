namespace MedHub_Backend.Exceptions;

public class ClinicNotFoundException : Exception
{
    public ClinicNotFoundException(string message) : base(message) { }
}