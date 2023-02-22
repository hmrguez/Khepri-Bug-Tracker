using BugTrackerWebApp.Models;

namespace BugTrackerWebApp.ViewModels;

public class TrackableListViewModel : EnumerableViewModel<Trackable>
{
    public string? ProjectName { get; set; }
}