using MedHub_Backend.Model;
using MedHub_Backend.Service.Interface;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MedHub_Backend.Service;

public class EmailService : IEmailService
{
    public async Task SendPatientResetPasswordEmail(Clinic clinic, User user, string tempPassword)
    {
        var subject = "MedHub - Account Information";
        var content = $"This is an automated message from {clinic.Name} \n " +
                      $"Your password is {tempPassword} \n " +
                      $"Use it to signin to your account and change it";

        await SendEmail(clinic.SendgridApiKey, user.Email, user.Username, subject, content, "");
    }

    public async Task SendPatientResultsCompleteEmail(string apiKey, string toEmail, string username)
    {
        var subject = "MedHub - Test Complete";
        var content = "This is an automated message \n Your test results have arrived";

        await SendEmail(apiKey, toEmail, username, subject, content, "");
    }

    private async Task SendEmail(string apiKey, string toEmail, string username, string subject, string content, string htmlContent)
    {
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("alexandru.i.jurju@gmail.com", "MedHub Program");
        var to = new EmailAddress(toEmail, username);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, content, htmlContent);
        var response = await client.SendEmailAsync(msg);
    }
}