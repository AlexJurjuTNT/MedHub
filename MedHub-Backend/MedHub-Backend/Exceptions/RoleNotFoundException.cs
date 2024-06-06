namespace MedHub_Backend.Exceptions;

public class RoleNotFoundException : Exception
{
    public RoleNotFoundException(string message) : base(message) { }
}