using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels;

public class CreateRoleViewModel
{

    [Required]
    [Display(Name ="RoleName")]
    public string? RoleName {get;set;}
}