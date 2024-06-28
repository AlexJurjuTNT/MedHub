namespace Medhub_Backend.Application.Service.Interface;

public interface IPasswordService
{
    string GenerateRandomPassword(int length);
}