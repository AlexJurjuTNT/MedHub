using Medhub_Backend.Domain.Entities;

namespace Medhub_Backend.Application.Abstractions.Service;

public interface IEmailService
{
    Task SendPatientResetPasswordEmail(Clinic clinic, User user, string tempPassword);
    Task SendPatientResultsCompleteEmail(Clinic clinic, User user);
    Task SendCreatedTestRequestEmail(Clinic clinic, TestRequest testRequest);
}