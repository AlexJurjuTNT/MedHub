using MedHub_Backend.Service.Interface;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MedHub_Backend.Service;

public class EmailService(
    IConfiguration configuration
) : IEmailService
{
    public async Task SendEmail(string toEmail, string username)
    {
        Console.WriteLine(configuration["SendGrid:ApiKey"]);
        var apiKey = configuration["SendGrid:ApiKey"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("alexandru.i.jurju@gmail.com", "Example User");
        var subject = "Sending with SendGrid is Fun";
        var to = new EmailAddress(toEmail, username);
        var plainTextContent = "and easy to do anywhere, even with C#";
        var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
    }
}