using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Dotnet.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Controllers;

public class AdministrationController : Controller
{

    private readonly SignInManager<IdentityUser> signInManager;
    private readonly UserManager<IdentityUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    public AdministrationController(SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
    }



    [HttpGet]
    public IActionResult CreateRole()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
    {
        if(ModelState.IsValid)
        {
            var role = new IdentityRole
            {
                Name = model.RoleName
            };

            var result = await roleManager.CreateAsync(role);

            if(result.Succeeded)
            {
                return RedirectToAction("RolesList");
            }
        }

        ModelState.AddModelError("","Faild to create role");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> RolesList()
    {
        var list = await roleManager.Roles.ToListAsync();
        return View(list);
    }

    

    [HttpGet]
    public async Task<IActionResult> EditRole(string id)
    {

        var role = await roleManager.FindByIdAsync(id);
        if(role == null)
        {
            ModelState.AddModelError("","invalid role name");
        }
        return View(role);
    }

    [HttpPost]
    public async Task<IActionResult> EditRole(IdentityRole model)
    {
        var role = await roleManager.FindByIdAsync(model.Id);
        if(role == null)
        {
            ModelState.AddModelError("","Role not found");
            View(model);
        }
        role.Name = model.Name;
        var result = await roleManager.UpdateAsync(role);
        if(result.Succeeded)
        {
            return RedirectToAction("RolesList");
        }
        ModelState.AddModelError("","Unable to Edit Role");
        return View(model);
    }


    

    [HttpGet]
    public async Task<IActionResult> DeleteRole(string id)
    {
        var role = await roleManager.FindByIdAsync(id);
        return View(role);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteRole(IdentityRole model)
    {
        var role = await roleManager.FindByIdAsync(model.Id);
        if(role == null)
        {
            ModelState.AddModelError("","Invalid Role");
            return View(model);
        }
        var result = await roleManager.DeleteAsync(role);
        if(result.Succeeded)
        {
            return RedirectToAction("RolesList");
        }
        var list = result.Errors.ToList();
        list.ForEach(x=> ModelState.AddModelError("",x.Description));
        return View(model);
    }
}