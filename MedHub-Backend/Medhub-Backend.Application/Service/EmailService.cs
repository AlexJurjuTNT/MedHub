using Medhub_Backend.Application.Abstractions.Service;
using Medhub_Backend.Domain.Entities;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Medhub_Backend.Application.Service;

public class EmailService : IEmailService
{
    public async Task SendPatientResetPasswordEmail(Clinic clinic, User user, string tempPassword)
    {
        var subject = "MedHub - Account Information";
        var content = $"This is an automated message from {clinic.Name} \n " +
                      $"Your username is {user.Username} \n " +
                      $"Your password is {tempPassword} \n " +
                      $"Use it to signin to your account and change it";

        await SendEmail(clinic.SendgridApiKey, clinic.Email, user.Email, user.Username, subject, content, "");
    }

    public async Task SendPatientResultsCompleteEmail(Clinic clinic, User user)
    {
        var subject = "MedHub - Test Complete";
        var content = "This is an automated message \n Your test results have arrived";

        await SendEmail(clinic.SendgridApiKey, clinic.Email, user.Email, user.Username, subject, content, "");
    }

    public async Task SendCreatedTestRequestEmail(Clinic clinic, TestRequest testRequest)
    {
        var subject = "MedHub - TestRequest";
        var content = "This is an automated message \n New Test Requests have been made \n";

        foreach (var type in testRequest.TestTypes) content += type.Name + " ";

        content += "\n";

        await SendEmail(clinic.SendgridApiKey, clinic.Email, testRequest.Patient.Email, testRequest.Patient.Username, subject, content, "");
    }

    private async Task SendEmail(string apiKey, string fromEmail, string toEmail, string username, string subject, string content, string htmlContent)
    {
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(fromEmail, "MedHub Program");
        var to = new EmailAddress(toEmail, username);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, content, htmlContent);
        var response = await client.SendEmailAsync(msg);
    }
}