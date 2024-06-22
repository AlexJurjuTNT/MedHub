namespace Medhub_Backend.Business.Service.Interface;

public interface IPasswordService
{
    string GenerateRandomPassword(int length);
}