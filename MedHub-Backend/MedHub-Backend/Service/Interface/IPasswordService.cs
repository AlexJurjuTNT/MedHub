namespace MedHub_Backend.Service.Interface;

public interface IPasswordService
{
    string GenerateRandomPassword(int length);
}