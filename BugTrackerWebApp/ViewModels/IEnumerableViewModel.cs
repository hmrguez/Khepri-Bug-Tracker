namespace BugTrackerWebApp.ViewModels;

public class EnumerableViewModel<T> : ViewModelBase
{
    public IEnumerable<T> Enumerable { get; set; }
}