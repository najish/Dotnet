using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
namespace Dotnet.ViewModels;

public class UpdateRolesViewModel
{
    public string Id {get;set;}
    public IdentityUser User {get;set;}
    public List<string> RoleName {get; set;}
    public List<bool> EnrolledUser {get; set;} 
}