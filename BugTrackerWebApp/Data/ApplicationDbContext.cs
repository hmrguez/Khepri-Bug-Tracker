using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BugTrackerWebApp.Models;

namespace BugTrackerWebApp.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Trackable> Trackables { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
}