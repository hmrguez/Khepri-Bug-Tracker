using BugTrackerWebApp.Data;
using BugTrackerWebApp.Interfaces;
using BugTrackerWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerWebApp.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Project>> GetAll()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<Project> GetById(int id)
    {
        return await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
    }

    public bool Add(Project club)
    {
        _context.Add(club);
        return Save();
    }

    public bool Update(Project club)
    {
        throw new NotImplementedException();
    }

    public bool Delete(Project club)
    {
        _context.Remove(club);
        return Save();
    }

    public bool Save()
    {
        return _context.SaveChanges() >= 0;
    }
}