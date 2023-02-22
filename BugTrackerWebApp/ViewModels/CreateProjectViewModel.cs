namespace BugTrackerWebApp.ViewModels;

public class CreateProjectViewModel : ViewModelBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AppUserId { get; set; }
    public string Description { get; set; }
    public string GithubLink { get; set; }
}