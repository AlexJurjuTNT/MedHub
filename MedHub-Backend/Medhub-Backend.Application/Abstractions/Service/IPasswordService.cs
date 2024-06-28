namespace Medhub_Backend.Application.Abstractions.Service;

public interface IPasswordService
{
    string GenerateRandomPassword(int length);
}