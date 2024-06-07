using MedHub_Backend.Service.Interface;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MedHub_Backend.Service;

public class EmailService(
    IConfiguration configuration
) : IEmailService
{
    private async Task SendEmail(string toEmail, string username, string subject, string content, string htmlContent)
    {
        var apiKey = configuration["SendGrid:ApiKey"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("alexandru.i.jurju@gmail.com", "MedHub Program");
        var to = new EmailAddress(toEmail, username);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, content, htmlContent);
        var response = await client.SendEmailAsync(msg);
    }

    public async Task SendPatientResetEmail(string toEmail, string username, string password)
    {
        string subject = "MedHub - Account Information";
        string content = $"This is an automated message \n Your password is ${password} \n Use it to signin to your account and change it";

        await SendEmail(toEmail, username, subject, content, "");
    }

    public async Task SendPatientResultsCompleteEmail(string toEmail, string username)
    {
        string subject = "MedHub - Test Complete";
        string content = $"This is an automated message \n Your test results have arrived";

        await SendEmail(toEmail, username, subject, content, "");
    }
}