using Medhub_Backend.Domain.Model;

namespace Medhub_Backend.Business.Service.Interface;

public interface IEmailService
{
    Task SendPatientResetPasswordEmail(Clinic clinic, User user, string tempPassword);
    Task SendPatientResultsCompleteEmail(Clinic clinic, User user);
    Task SendCreatedTestRequestEmail(Clinic clinic, TestRequest testRequest);
}