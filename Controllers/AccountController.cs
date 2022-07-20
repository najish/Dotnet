using Dotnet.ViewModels;
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

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if(!ModelState.IsValid)
        {
            ModelState.AddModelError("","enter valid credential");
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
        var user = await userManager.FindByIdAsync(model.Id);
        if(user == null)
        {
            ModelState.AddModelError("","Invalid User");
            return View(model);
        }
        var result = await userManager.DeleteAsync(user);
        if(result.Succeeded)
            return RedirectToAction("UserList");
        return View(model);
    }



    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var user = await userManager.FindByEmailAsync(model.Email);
        if(user == null)
        {
            ModelState.AddModelError("","Invalid username");
            return View(model);
        }

        var result = await signInManager.PasswordSignInAsync(user,model.Password,model.RememberMe,false);
        if(result.Succeeded)
            return RedirectToAction("GetStudents","Student");
        ModelState.AddModelError("","Password is wrong try again :-)");
        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("GetStudents","Student");
    }

    [HttpGet]
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
        var user = await userManager.FindByIdAsync(id);
        var roles = await (roleManager.Roles).ToListAsync();
        var list = new List<string>();
        // foreach(var role in roles)
        // {
        //     list.Add(role.Name);
        // }
        var enroll = new List<bool>();


        for(int i = 0; i < roles.Count; i++)
        {
            list.Add(roles[i].Name);
            var result = await userManager.IsInRoleAsync(user,roles[i].Name); 
            enroll.Add(result);
        }
        

        
        var model = new EditUserViewModel
        {
            User = user,
            Roles = list,
            EnrolledRoles = enroll
        };
        return View(model);
    }


    [HttpPost]
    public async Task<IActionResult> EditUser(EditUserViewModel model)
    {
        var user = await userManager.FindByIdAsync(model.User.Id);


        if(user == null)
        {   
            ModelState.AddModelError("","user not found");
            return View(model);
        }
        user.Email = model.User.Email;
        user.UserName = model.User.UserName;
        var reuslt = await userManager.UpdateAsync(user);

        if(reuslt.Succeeded)
        {

            for(int i = 0; i < model.EnrolledRoles.Count; i++)
            {
                if(model.EnrolledRoles[i])
                {
                    var res = await userManager.IsInRoleAsync(model.User,model.Roles[i]);
                    if(res)
                    {
                        continue;
                    }
                    else
                    {
                        var roleResult = await userManager.AddToRoleAsync(model.User,model.Roles[i]);

                        if(roleResult.Succeeded == false)
                        {
                            foreach(var error in roleResult.Errors)
                            {
                                ModelState.AddModelError("",error.Description);
                            }
                        }
                    }
                }                
            }
            return RedirectToAction("UserList");
        }

        foreach(var error in reuslt.Errors)
        {
            ModelState.AddModelError("",error.Description);
            if(model.EnrolledRoles == null)
            {
                model.EnrolledRoles = new List<bool>();
                
            }
            if(model.Roles == null)
            {
                model.Roles = new List<string>();
            }
        }
        return View(model);
    }


    [HttpGet]
    public async Task<IActionResult> ForgetPassword()
    {

        return View();
    }
}