using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    public string? Email {get;set;}
    [Required]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword",ErrorMessage = "Passwords doesn't match")]
    public string? Password {get;set;}

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string? ConfirmPassword {get;set;}

}