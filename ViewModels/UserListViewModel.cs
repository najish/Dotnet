using Dotnet.Models;
using Microsoft.AspNetCore.Identity;

namespace Dotnet.ViewModels;


public class UserListViewModel
{
    public List<IdentityUser> List {get;set;} = new List<IdentityUser>();
}