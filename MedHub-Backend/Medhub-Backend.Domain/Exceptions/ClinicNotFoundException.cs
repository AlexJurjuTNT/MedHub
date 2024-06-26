namespace Medhub_Backend.Domain.Exceptions;

public class ClinicNotFoundException : Exception
{
    public ClinicNotFoundException(int clinicId) : base($"Clinic with id {clinicId} not found")
    {
    }
}