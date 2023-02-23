using BugTrackerWebApp.Models;

namespace BugTrackerWebApp.ViewModels;

public class TrackableListViewModel : ViewModelBase
{
    public int ProjectId { get; set; }
    public IEnumerable<Trackable> Trackables { get; set; }
}