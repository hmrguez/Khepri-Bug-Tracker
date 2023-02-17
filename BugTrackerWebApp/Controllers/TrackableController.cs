using Microsoft.AspNetCore.Mvc;

namespace BugTrackerWebApp.Controllers;

public class TrackableController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}