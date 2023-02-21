using System.ComponentModel.DataAnnotations;

namespace BugTrackerWebApp.ViewModels;

public class RegisterViewModel : ViewModelBase
{
    [Required(ErrorMessage = "Email is required")]
    [Display(Name = "Email address")]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string ConfirmedPassword { get; set; }
}