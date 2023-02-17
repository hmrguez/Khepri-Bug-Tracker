using System.ComponentModel.DataAnnotations;

namespace BugTrackerWebApp.Models;

public class Project
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string GithubLink { get; set; }
}