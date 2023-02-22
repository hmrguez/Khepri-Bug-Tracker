using BugTrackerWebApp.Data.Enum;
using BugTrackerWebApp.Interfaces;
using BugTrackerWebApp.Models;
using BugTrackerWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace BugTrackerWebApp.Controllers;

public class TrackableController : Controller
{
    private readonly ITrackableRepository _trackableRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly UserManager<AppUser> _userManager;

    public TrackableController(ITrackableRepository trackableRepository, IProjectRepository projectRepository, UserManager<AppUser> userManager)
    {
        _trackableRepository = trackableRepository;
        _projectRepository = projectRepository;
        _userManager = userManager;
    }
    
    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var trackables = await _trackableRepository.GetAll();
        return View(new EnumerableViewModel<Trackable>{Enumerable = trackables, UserName = currentUser?.Email});
    }
    
    public async Task<IActionResult> IndexByProject(string projectName)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var trackables = await _trackableRepository.GetByProjectName(projectName);
        return View("Index", new EnumerableViewModel<Trackable>{Enumerable = trackables, UserName = currentUser?.Email});
    }

    public async Task<IActionResult> Detail(int id)
    {
        var trackable = await _trackableRepository.GetById(id);
        return View(trackable);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TrackableViewModel trackableVm)
    {
        if (ModelState.IsValid)
        {
            var project = await _projectRepository.GetByName(trackableVm.ProjectName);
            var track = new Trackable()
            {
                Name = trackableVm.Name,
                Description = trackableVm.Description,
                ProjectId = project.Id,
                DateCreated = DateTime.Now,
                Status = Status.Opened,
                TrackType = trackableVm.TrackType
            };
            _trackableRepository.Add(track);
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("","Failed to create");
            return View(trackableVm);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var track = await _trackableRepository.GetById(id);
        if (track == null) return NotFound();
        var currentUser = await _userManager.GetUserAsync(User);
        return View(new TrackBoxViewModel{Trackable = track, UserName = currentUser?.Email});
    }
    
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteTrack(TrackBoxViewModel trackBoxViewModel)
    {
        var track = await _trackableRepository.GetById(trackBoxViewModel.Trackable.Id);
        if (track == null) return View("Error");
        _trackableRepository.Delete(track);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var trackable = await _trackableRepository.GetById(id);
        if (trackable == null) return View("Error");
        var trackVm = new EditTrackableViewModel()
        {
            Name = trackable.Name,
            Description = trackable.Description,
            ProjectName = trackable.Project.Name,
            TrackType = trackable.TrackType,
            Status = trackable.Status
        };
        return View(trackVm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, EditTrackableViewModel editTrackableViewModel)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("","Failed to edit Task");
            return View("Edit", editTrackableViewModel);
        }

        var track = await _trackableRepository.GetByIdNoTracking(id);
        if (track != null)
        {
            var project = await _projectRepository.GetByName(editTrackableViewModel.ProjectName);
            var trackable = new Trackable()
            {
                Id = id,
                Name = editTrackableViewModel.Name,
                Description = editTrackableViewModel.Description,
                ProjectId = project.Id,
                Project = project,
                Status = editTrackableViewModel.Status,
                TrackType = editTrackableViewModel.TrackType,
                DateCreated = track.DateCreated
            };

            _trackableRepository.Update(trackable);
            return RedirectToAction("Index");
        }
        else
        {
            return View(editTrackableViewModel);
        }
    }

    public async Task<IActionResult> QuickCheck(int id)
    {
        var trackable = await _trackableRepository.GetById(id);
        trackable.Status = Status.Completed;
        _trackableRepository.Update(trackable);
        return RedirectToAction("Index");
    }
}