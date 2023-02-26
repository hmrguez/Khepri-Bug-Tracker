using BugTrackerWebApp.Data;
using BugTrackerWebApp.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace BugTrackerWebApp.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }
    
    public void SendEmail(EmailDto request)
    {
        var userName = _config.GetSection("EmailUser").Value;
        var password = _config.GetSection("EmailPassword").Value;
        
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(userName));
        email.To.Add(MailboxAddress.Parse(request.To));
        email.Subject = request.Subject;
        email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

        using var smtp = new SmtpClient();
        smtp.Connect(_config.GetSection("EmailHost").Value, 587,SecureSocketOptions.StartTls);
        smtp.Authenticate(userName, password);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}