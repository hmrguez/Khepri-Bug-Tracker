using BugTrackerWebApp.Data.Enum;

namespace BugTrackerWebApp.ViewModels;

public class TrackableViewModel : ViewModelBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ProjectName { get; set; }
    public TrackType TrackType { get; set; }
}