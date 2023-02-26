using BugTrackerWebApp.Models;

namespace BugTrackerWebApp.Data;

public static class EmailTemplates
{
    public static EmailDto CompletedTaskEmail(AppUser currentUser, Trackable task, string leadDevEmail)
    {
        return new EmailDto
        {
            To = leadDevEmail,
            Subject = $"Task {task.Name} completed by {currentUser.Email}",
            Body = $"<p>Task {task.Name} completed by {currentUser.Email}</p>"
        };
    }
}