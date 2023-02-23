using BugTrackerWebApp.Data;
using BugTrackerWebApp.Interfaces;
using BugTrackerWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerWebApp.Repositories;

public class TrackableRepository : ITrackableRepository
{
    private readonly ApplicationDbContext _context;

    public TrackableRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Trackable>> GetAll()
    {
        return await _context.Trackables.Include(x=>x.Project).ToListAsync();
    }

    public async Task<Trackable> GetById(int id)
    {
        return await _context.Trackables.Include(x=>x.Project).FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<Trackable> GetByIdNoTracking(int id)
    {
        return await _context.Trackables.Include(x=>x.Project).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Trackable>> GetByProjectName(string projectName)
    {
        return await _context.Trackables
            .Include(x => x.Project)
            .Where(x => x.Project.Name == projectName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Trackable>> GetByProjectId(int projectId)
    {
        return await _context.Trackables
            .Include(x => x.Project)
            .Where(x => x.Project.Id == projectId)
            .ToListAsync();
    }

    public bool Add(Trackable club)
    {
        _context.Add(club);
        return Save();
    }

    public bool Update(Trackable club)
    {
        _context.Update(club);
        return Save();
    }

    public bool Delete(Trackable club)
    {
        _context.Remove(club);
        return Save();
    }

    public bool Save()
    {
        return _context.SaveChanges() >= 0;
    }
}