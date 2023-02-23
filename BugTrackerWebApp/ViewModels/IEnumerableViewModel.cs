namespace BugTrackerWebApp.ViewModels;

public class EnumerableViewModel<T> : ViewModelBase
{
    public IEnumerable<T> Collection { get; set; }
}