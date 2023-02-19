using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BugTrackerWebApp.Models;

public class AppUser : IdentityUser
{
    public string? Name { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Description { get; set; }
    public ICollection<Trackable> Trackables { get; set; }
    public ICollection<Project> Projects { get; set; }
}