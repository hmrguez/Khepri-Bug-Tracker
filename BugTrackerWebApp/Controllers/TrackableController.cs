using BugTrackerWebApp.Data;
using BugTrackerWebApp.Data.Enum;
using BugTrackerWebApp.Interfaces;
using BugTrackerWebApp.Models;
using BugTrackerWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackerWebApp.Controllers;

public class TrackableController : Controller
{
    private readonly ITrackableRepository _trackableRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly IEmailService _emailService;

    public TrackableController(ITrackableRepository trackableRepository, IProjectRepository projectRepository, UserManager<AppUser> userManager, IEmailService emailService)
    {
        _trackableRepository = trackableRepository;
        _projectRepository = projectRepository;
        _userManager = userManager;
        _emailService = emailService;
    }

    #region List

    public async Task<IActionResult> Index(int projectId)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var trackables = await _trackableRepository.GetByProjectId(projectId);
        return View(new TrackableListViewModel{ ProjectId = projectId, Trackables = trackables, UserName = currentUser?.Email});
    }

    #endregion

    #region Detail

    public async Task<IActionResult> Detail(int id)
    {
        var trackable = await _trackableRepository.GetById(id);
        var currentUser = await _userManager.GetUserAsync(User);
        return View(new TrackBoxViewModel{Trackable = trackable, UserName = currentUser.Email});
    }

    #endregion

    #region Create

    public async Task<IActionResult> Create(int projectId)
    {
        var vm = new TrackableViewModel
        {
            ProjectId = projectId
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TrackableViewModel trackableVm)
    {
        if (ModelState.IsValid)
        {
            var track = new Trackable()
            {
                Name = trackableVm.Name,
                Description = trackableVm.Description,
                ProjectId = trackableVm.ProjectId,
                DateCreated = DateTime.Now,
                LeadEmail = trackableVm.ManagerEmail,
                Status = Status.Opened,
                TrackType = trackableVm.TrackType
            };
            _trackableRepository.Add(track);
            var currentUser = await _userManager.GetUserAsync(User);
            var trackables = await _trackableRepository.GetByProjectId(track.ProjectId);
            return View("Index", new TrackableListViewModel{ ProjectId = track.ProjectId, Trackables = trackables, UserName = currentUser?.Email});
        }
        else
        {
            ModelState.AddModelError("","Failed to create");
            return View(trackableVm);
        }
    }

    #endregion

    #region Delete

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
        var currentUser = await _userManager.GetUserAsync(User);
        var trackables = await _trackableRepository.GetByProjectId(track.ProjectId);
        return View("Index", new TrackableListViewModel{ ProjectId = track.ProjectId, Trackables = trackables, UserName = currentUser?.Email});

    }

    #endregion

    #region Edit

    public async Task<IActionResult> Edit(int id)
    {
        var trackable = await _trackableRepository.GetById(id);
        if (trackable == null) return View("Error");
        var trackVm = new EditTrackableViewModel()
        {
            Name = trackable.Name,
            Description = trackable.Description,
            ProjectName = trackable.Project.Name,
            ProjectId = trackable.ProjectId,
            TrackType = trackable.TrackType,
            Status = trackable.Status,
            LeadDevEmail = trackable.LeadEmail
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
            var currentUser = await _userManager.GetUserAsync(User);
            
            var currentStatus = track.Status;
            var newStatus = editTrackableViewModel.Status;

            if (currentStatus != Status.Completed && newStatus == Status.Completed && track.LeadEmail != null)
            {
                var completedEmail = EmailTemplates.CompletedTaskEmail(currentUser,track, track.LeadEmail);
                _emailService.SendEmail(completedEmail);
            }
            
            var project = await _projectRepository.GetById(editTrackableViewModel.ProjectId);
            var trackable = new Trackable()
            {
                Id = id,
                Name = editTrackableViewModel.Name,
                Description = editTrackableViewModel.Description,
                ProjectId = project.Id,
                Project = project,
                Status = editTrackableViewModel.Status,
                TrackType = editTrackableViewModel.TrackType,
                DateCreated = track.DateCreated,
                LeadEmail = editTrackableViewModel.LeadDevEmail
            };

            _trackableRepository.Update(trackable);
            var trackables = await _trackableRepository.GetByProjectId(trackable.ProjectId);
            return View("Index", new TrackableListViewModel{ ProjectId = trackable.ProjectId, Trackables = trackables, UserName = currentUser?.Email});

        }
        else
        {
            return View(editTrackableViewModel);
        }
    }
    
    public async Task<IActionResult> QuickCheck(int id)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var trackable = await _trackableRepository.GetById(id);
        
        if(trackable.Status != Status.Completed && trackable.LeadEmail != null)
        {
            var completedEmail = EmailTemplates.CompletedTaskEmail(currentUser, trackable, trackable.LeadEmail);
            _emailService.SendEmail(completedEmail);
        }
        
        trackable.Status = Status.Completed;
        _trackableRepository.Update(trackable);
        var trackables = await _trackableRepository.GetByProjectId(trackable.ProjectId);
        return View("Index", new TrackableListViewModel{ ProjectId = trackable.ProjectId, Trackables = trackables, UserName = currentUser?.Email});
    }

    #endregion
    
}