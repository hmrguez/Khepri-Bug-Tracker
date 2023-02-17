using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BugTrackerWebApp.Data.Enum;

namespace BugTrackerWebApp.Models;

public class Trackable
{
    [Key] public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public TrackType TrackType { get; set; }
    public DateTime DateCreated { get; set; }
    public Status Status { get; set; }
    
    [ForeignKey("Project")] public int ProjectId { get; set; }
    public Project Project { get; set; }
    
    [ForeignKey("AppUser")] public string? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}