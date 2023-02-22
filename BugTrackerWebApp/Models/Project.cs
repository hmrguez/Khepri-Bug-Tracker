using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTrackerWebApp.Models;

public class Project
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string GithubLink { get; set; }
    [ForeignKey("AppUser")] public string AppUserId { get; set; }
}