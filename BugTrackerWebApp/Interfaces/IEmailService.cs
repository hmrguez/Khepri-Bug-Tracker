using BugTrackerWebApp.Data;

namespace BugTrackerWebApp.Interfaces;

public interface IEmailService
{
    void SendEmail(EmailDto request);
}