using BugTrackerWebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackerWebApp.Controllers;

public class ProjectController : Controller
{
    private readonly IProjectRepository _projectRepository;

    public ProjectController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    public async Task<IActionResult> Index()
    {
        var projects = await _projectRepository.GetAll();
        return View(projects);
    }
}