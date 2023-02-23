using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BugTrackerWebApp.Models;

public class AppUser : IdentityUser
{
    public string? Name { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Description { get; set; }
}