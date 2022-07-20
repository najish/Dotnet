using Dotnet.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Controllers;


public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly SignInManager<IdentityUser> signInManager;
    public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
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

        return View(user);
    }


    [HttpPost]
    public async Task<IActionResult> EditUser(IdentityUser model)
    {
        var user = await userManager.FindByIdAsync(model.Id);


        if(user == null)
            return View(user);
        user.Email = model.Email;
        user.UserName = model.UserName;
        await userManager.UpdateAsync(user);
        return RedirectToAction("UserList");
    }


    [HttpGet]
    public async Task<IActionResult> ForgetPassword()
    {

        return View();
    }
}