namespace MedHub_Backend.Service.Authentication;

public interface IPasswordService
{
    string GenerateRandomPassword(int length);
}