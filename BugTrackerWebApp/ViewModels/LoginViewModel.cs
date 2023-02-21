using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BugTrackerWebApp.ViewModels;

public class LoginViewModel : ViewModelBase
{
    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "Email address is required")] public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}