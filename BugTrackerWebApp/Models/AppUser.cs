using System.ComponentModel.DataAnnotations;

namespace BugTrackerWebApp.Models;

public class AppUser
{
    [Key] public string Id { get; set; }
    public string? Name { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Description { get; set; }
    public ICollection<Trackable> Trackables { get; set; }
}