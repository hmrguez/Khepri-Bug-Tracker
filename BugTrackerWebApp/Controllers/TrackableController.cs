using BugTrackerWebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackerWebApp.Controllers;

public class TrackableController : Controller
{
    private readonly ITrackableRepository _trackableRepository;

    public TrackableController(ITrackableRepository trackableRepository)
    {
        _trackableRepository = trackableRepository;
    }
    
    public async Task<IActionResult> Index()
    {
        var trackables = await _trackableRepository.GetAll();
        return View(trackables);
    }
}