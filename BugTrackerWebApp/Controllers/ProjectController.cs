using System.Security.Claims;
using BugTrackerWebApp.Interfaces;
using BugTrackerWebApp.Models;
using BugTrackerWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackerWebApp.Controllers;

public class ProjectController : Controller
{
    private readonly IProjectRepository _projectRepository;
    private readonly ITrackableRepository _trackableRepository;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<AppUser> _userManager;

    public ProjectController(IProjectRepository projectRepository, ITrackableRepository trackableRepository,IUserRepository userRepository, UserManager<AppUser> userManager)
    {
        _projectRepository = projectRepository;
        _trackableRepository = trackableRepository;
        _userRepository = userRepository;
        _userManager = userManager;
    }
    
    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var projects = await _userRepository.GetAllProjectsByUser(currentUser);
        return View(new EnumerableViewModel<Project>{Enumerable = projects, UserName = currentUser.Email});
    }

    public async Task<IActionResult> IndexForTrack()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var projects = await _userRepository.GetAllProjectsByUser(currentUser);
        return View(new EnumerableViewModel<Project>{Enumerable = projects, UserName = currentUser.Email});
    }

    //Create controller just like the trackable one
    public async Task<IActionResult> Create()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        return View(new CreateProjectViewModel{UserName = currentUser.Email, AppUserId = currentUser.Id});
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectViewModel projectVm)
    {
        if (ModelState.IsValid)
        {
            var project = new Project
            {
                AppUserId = projectVm.AppUserId,
                Description = projectVm.Description,
                GithubLink = projectVm.GithubLink,
                Id = projectVm.Id,
                Name = projectVm.Name
            };
            _projectRepository.Add(project);
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("","Failed to create");
            return View(projectVm);
        }
    }
    
    //Delete controller just like the trackable one
    public async Task<IActionResult> Delete(int id)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var project = await _projectRepository.GetById(id);
        if (project == null)
        {
            return NotFound();
        }
        return View(new ProjectBoxViewModel{Project = project, UserName = currentUser?.Email});
    }
    
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(ProjectBoxViewModel projectBoxViewModel)
    {
        var project = await _projectRepository.GetById(projectBoxViewModel.Project.Id);
        if (project == null)
        {
            return NotFound();
        }

        var tracksForProject = await _trackableRepository.GetByProjectName(project.Name);
        if (tracksForProject.Any())
        {
            return RedirectToAction("Index");
        }
        
        _projectRepository.Delete(project);
        return RedirectToAction("Index");
    }
}