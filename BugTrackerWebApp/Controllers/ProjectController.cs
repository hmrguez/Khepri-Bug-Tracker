using BugTrackerWebApp.Interfaces;
using BugTrackerWebApp.Models;
using BugTrackerWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackerWebApp.Controllers;

public class ProjectController : Controller
{
    private readonly IProjectRepository _projectRepository;
    private readonly ITrackableRepository _trackableRepository;

    public ProjectController(IProjectRepository projectRepository, ITrackableRepository trackableRepository)
    {
        _projectRepository = projectRepository;
        _trackableRepository = trackableRepository;
    }
    
    public async Task<IActionResult> Index()
    {
        var projects = await _projectRepository.GetAll();
        return View(new EnumerableViewModel<Project>{Enumerable = projects, Temp = "Project"});
    }

    public async Task<IActionResult> IndexForTrack()
    {
        var projects = await _projectRepository.GetAll();
        return View(new EnumerableViewModel<Project>{Enumerable = projects, Temp = "Project"});
    }

    //Create controller just like the trackable one
    public async Task<IActionResult> Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Project project)
    {
        if (ModelState.IsValid)
        {
            _projectRepository.Add(project);
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("","Failed to create");
            return View(project);
        }
    }
    
    //Delete controller just like the trackable one
    public async Task<IActionResult> Delete(int id)
    {
        var project = await _projectRepository.GetById(id);
        if (project == null)
        {
            return NotFound();
        }
        return View(new ProjectBoxViewModel{Project = project, Temp = "Delete Project"});
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