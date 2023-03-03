using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BugTrackerWebApp.Models;
using BugTrackerWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BugTrackerWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<AppUser> _userManager;

    public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        return RedirectToAction("Index", "Project");
        
        // TODO: Implement landing page
        
        var currentUser = await _userManager.GetUserAsync(User);
        return View(new ViewModelBase { UserName = currentUser?.Email });
    }
    
    public IActionResult Privacy()
    {
        return View();
    }
    
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}