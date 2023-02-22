using BugTrackerWebApp.Models;

namespace BugTrackerWebApp.Interfaces;

public interface IUserRepository
{
    public Task<IEnumerable<Project>> GetAllProjectsByUser(AppUser user);
}