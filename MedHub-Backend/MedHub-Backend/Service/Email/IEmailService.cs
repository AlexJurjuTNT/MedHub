namespace MedHub_Backend.Service.Email;

public interface IEmailService
{
    Task SendPatientResetPasswordEmail(Model.Clinic clinic, Model.User user, string tempPassword);
    Task SendPatientResultsCompleteEmail(Model.Clinic clinic, Model.User user);
    Task SendCreatedTestRequestEmail(Model.Clinic clinic, Model.TestRequest testRequest);
}