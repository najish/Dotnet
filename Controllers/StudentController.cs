using Dotnet.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Controllers;

public class StudentController : Controller
{
    private readonly IStudentRepository studentRepo;
    public StudentController(IStudentRepository studentRepo)
    {
        this.studentRepo = studentRepo;
    }


    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add()
    {
        return View();
    }
}