using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface IEmailService
{
    Task SendPatientResetEmail(Clinic clinic, User user, string tempPassword);
    Task SendPatientResultsCompleteEmail(string apiKey, string toEmail, string username);
}