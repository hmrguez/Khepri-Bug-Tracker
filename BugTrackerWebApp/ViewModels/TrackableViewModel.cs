using System.ComponentModel.DataAnnotations;
using BugTrackerWebApp.Data.Enum;

namespace BugTrackerWebApp.ViewModels;

public class TrackableViewModel : ViewModelBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; }
    public string? ManagerEmail { get; set; }
    public TrackType TrackType { get; set; }
}