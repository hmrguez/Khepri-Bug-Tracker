using BugTrackerWebApp.Models;

namespace BugTrackerWebApp.Interfaces;

public interface ITrackableRepository
{
    Task<IEnumerable<Trackable>> GetAll();
    Task<Trackable> GetById(int id);
    Task<Trackable> GetByIdNoTracking(int id);
    bool Add(Trackable club);
    bool Update(Trackable club);
    bool Delete(Trackable club);
    bool Save();
}