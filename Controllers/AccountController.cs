using Dotnet.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
                return RedirectToAction("GetStudents","Student");
            }
        }
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
}