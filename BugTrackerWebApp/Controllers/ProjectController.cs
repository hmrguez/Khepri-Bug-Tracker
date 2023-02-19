using BugTrackerWebApp.Interfaces;
using BugTrackerWebApp.Models;
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
        return View(project);
    }
    
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var project = await _projectRepository.GetById(id);
        if (project == null)
        {
            return NotFound();
        }
        _projectRepository.Delete(project);
        return RedirectToAction("Index");
    }
}