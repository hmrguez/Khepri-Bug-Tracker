using BugTrackerWebApp.Data;
using BugTrackerWebApp.Models;
using BugTrackerWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BugTrackerWebApp.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    #region Login

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid) return View(loginViewModel);

        var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

        if (user != null)
        {
            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        
        TempData["Error"] = "Wrong credentials. Please, try again";
        return View(loginViewModel);
    }

    #endregion

    #region Register

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid) return View(registerViewModel);

        var user = await _userManager.FindByEmailAsync(registerViewModel.Email);

        if (user != null)
        {
            TempData["Error"] = "This email address is already taken";
            registerViewModel.Email = null;
            return View(registerViewModel);
        }

        if (registerViewModel.Password != registerViewModel.ConfirmedPassword)
        {
            TempData["Error"] = "Password and confirmed password do not match";
            registerViewModel.Password = registerViewModel.ConfirmedPassword = null;
            return View(registerViewModel);
        }
        
        var newUser = new AppUser()
        {
            Email = registerViewModel.Email,
            UserName = registerViewModel.Email
        };

        var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

        if (newUserResponse.Succeeded)
        {
            return RedirectToAction("Login");
        }
        
        TempData["Error"] = "Unsecure password: use upper and lower case letters, numbers and non-alphanumeric symbols for better security";
        return View(registerViewModel);
    }

    #endregion

    #region Logout

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login");
    }

    #endregion
    
}