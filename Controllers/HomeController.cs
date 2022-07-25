using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Dotnet.Models;
using Microsoft.AspNetCore.Authorization;
using Dotnet.ViewModels;

namespace Dotnet.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }


    [HttpPost]
    public IActionResult Index(AnotherStudentViewModel model)
    {
        return Index();
    }
    public IActionResult Index()
    {
        var model = new AnotherStudentViewModel();
        if(model.Student is null)
        {
            _logger.LogInformation("student property is null");
        }
        else
            _logger.LogInformation("student perperty is not null");
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
