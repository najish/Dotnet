using Microsoft.AspNetCore.Identity;

namespace Dotnet.ViewModels;


public class EditUserViewModel
{
    public IdentityUser User { get; set; }
    public List<string> Roles {get; set;}

    public List<bool> EnrolledRoles {get; set;}
}