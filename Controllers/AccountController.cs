using Dotnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Controllers;



public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;
    public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager,RoleManager<IdentityRole> roleManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }


    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if(!ModelState.IsValid)
        {
            ModelState.AddModelError("","Please fill the form correctly");
            return View(model);
        }

        var user = new IdentityUser
        {
            Email = model.Email,
            UserName = model.Email
        };

        var result = await userManager.CreateAsync(user,model.Password);
        if(result.Succeeded)
        {

            var signResult = await signInManager.PasswordSignInAsync(user,model.Password,false,false);
            if(signResult.Succeeded)
            {
                return RedirectToAction("UserList");
            }
            ModelState.AddModelError("","Failed to login");
        }
        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if(user == null)
        {
            ModelState.AddModelError("","Invalid User");
            return View(user);
        }

        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(IdentityUser model)
    {

        if(ModelState.IsValid)
        {
            var user = await userManager.FindByIdAsync(model.Id);
            if(user == null)
            {
                ModelState.AddModelError("","Invalid User");
                return View(model);
            }
            var result = await userManager.DeleteAsync(user);
            if(result.Succeeded)
                return RedirectToAction("UserList");

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("",error.Description);
            }
        }
        ModelState.AddModelError("","Please fill form correctly");
        return View(model);
    }



    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        if(string.IsNullOrEmpty(returnUrl) == false)
            ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model,string returnUrl)
    {
        if(ModelState.IsValid)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                ModelState.AddModelError("","Invalid username");
                return View(model);
            }
            var result = await signInManager.PasswordSignInAsync(user,model.Password,model.RememberMe,false);
            if(result.Succeeded)
            {
                if(string.IsNullOrEmpty(returnUrl) == false && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                return RedirectToAction("GetStudents","Student");
            }
            
            ModelState.AddModelError("","Wrong credential try again");
        }
        
        ModelState.AddModelError("","Please fill form correctly");
        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("GetStudents","Student");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UserList()
    {
        var list = await userManager.Users.ToListAsync();
        var model = new UserListViewModel
        {
            List = list
        };
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(string id)
    {
        
        EditUserViewModel model = new EditUserViewModel();
        var user = await userManager.FindByIdAsync(id);
        if(user == null)
        {
            ModelState.AddModelError("","Unable to Find User");
        }
        else
        {
            model = new EditUserViewModel
            {
                User = user,
            };
        }
        return View(model);
    }
    // This comment belongs to development branch

    [HttpPost]
    public async Task<IActionResult> EditUser(EditUserViewModel model)
    {
        if(ModelState.IsValid)
        {
            var user = await userManager.FindByIdAsync(model.User.Id);
            if(user == null)
                ModelState.AddModelError("","Unable to find user");
            else
            {
                user.Email = model.User.Email;
                user.UserName = model.User.UserName;
                var result = await userManager.UpdateAsync(user);

                if(result.Succeeded)
                    return RedirectToAction("UserList");
                ModelState.AddModelError("","Failed to Update User");
                return View(model);
            }

        }
        ModelState.AddModelError("","Please fill form correctly");        
        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> UpdateRoles(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        UpdateRolesViewModel model = new UpdateRolesViewModel();
        if(user == null)
        {
            ModelState.AddModelError("","Unable to find user");
        }
        else
        {
            var roles = await roleManager.Roles.ToListAsync();
            var roleNames = new List<string>();
            var list = new List<bool>();
            // roles.ForEach(async x => 
            // {
            //     roleNames.Add(x.Name);
            //     var result = await userManager.IsInRoleAsync(user,x.Name);
            //     list.Add(result);
            // });

            for(int i = 0; i < roles.Count; i++)
            {
                roleNames.Add(roles[i].Name);
                var result = await userManager.IsInRoleAsync(user,roles[i].Name);
                list.Add(result);
            }
            model = new UpdateRolesViewModel
            {
                User = user,
                RoleName = roleNames,
                EnrolledUser = list
            };

        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateRoles(UpdateRolesViewModel model,string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if(user == null)
        {
            ModelState.AddModelError("","Unable to find user");
            return View(model);
        }
        var roleName = await roleManager.Roles.ToListAsync();
        for(int i = 0; i < roleName.Count; i++)
        {
            var result = await userManager.IsInRoleAsync(user,roleName[i].Name);

            if(result && model.EnrolledUser[i] == false)
            {
                var r = await userManager.RemoveFromRoleAsync(user,roleName[i].Name);
                if(r.Succeeded)
                {
                    continue;
                }
                foreach(var error in r.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
            else if(result == false && model.EnrolledUser[i])
            {
                var r = await userManager.AddToRoleAsync(user,roleName[i].Name);
                if(r.Succeeded)
                    continue;
                foreach(var error in r.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
        }
        return RedirectToAction("UserList");
    }
}


