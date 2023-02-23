using BugTrackerWebApp.Models;

namespace BugTrackerWebApp.Interfaces;

public interface ITrackableRepository
{
    Task<IEnumerable<Trackable>> GetAll();
    Task<Trackable> GetById(int id);
    Task<Trackable> GetByIdNoTracking(int id);
    Task<IEnumerable<Trackable>> GetByProjectName(string projectName);
    Task<IEnumerable<Trackable>> GetByProjectId(int projectId);
    bool Add(Trackable club);
    bool Update(Trackable club);
    bool Delete(Trackable club);
    bool Save();
}