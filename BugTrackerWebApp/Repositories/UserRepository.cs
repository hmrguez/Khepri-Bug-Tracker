using BugTrackerWebApp.Data;
using BugTrackerWebApp.Interfaces;
using BugTrackerWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerWebApp.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Project>> GetAllProjectsByUser(AppUser user)
    {
        return await _context.Projects.Where(x => x.AppUserId == user.Id).ToListAsync();
    }
}