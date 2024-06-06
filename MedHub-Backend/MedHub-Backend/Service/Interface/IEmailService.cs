namespace MedHub_Backend.Service.Interface;

public interface IEmailService
{
    Task SendPatientResetEmail(string toEmail, string username, string password);
}