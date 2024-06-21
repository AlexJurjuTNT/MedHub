using MedHub_Backend.Model;

namespace MedHub_Backend.Service.Interface;

public interface IEmailService
{
    Task SendPatientResetPasswordEmail(Clinic clinic, User user, string tempPassword);
    Task SendPatientResultsCompleteEmail(Clinic clinic, User user);
    Task SendCreatedTestRequestEmail(Clinic clinic, TestRequest testRequest);
}