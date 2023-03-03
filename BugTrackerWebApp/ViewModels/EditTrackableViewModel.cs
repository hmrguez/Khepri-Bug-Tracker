using BugTrackerWebApp.Data.Enum;

namespace BugTrackerWebApp.ViewModels;

public class EditTrackableViewModel : ViewModelBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ProjectId { get; set; }
    public Status Status { get; set; }
    public TrackType TrackType { get; set; }
    public string ProjectName { get; set; }
    public string? LeadDevEmail { get; set; }
}