using Microsoft.AspNetCore.Mvc;

namespace BugTrackerWebApp.Controllers;

public class ProjectController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}