namespace MedHub_Backend.Service.Interface;

public interface IEmailService
{
    Task SendEmail(string toEmail, string username);
}