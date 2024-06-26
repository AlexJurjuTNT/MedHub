namespace Medhub_Backend.Domain.Exceptions;

public class RoleNotFoundException : Exception
{
    public RoleNotFoundException(string role) : base($"Role with name {role} not found")
    {
    }
}