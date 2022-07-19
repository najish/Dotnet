using Dotnet.Models;
using Dotnet.Repository;
using Dotnet.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Controllers;

public class StudentController : Controller
{
    private readonly IStudentRepository studentRepo;
    public StudentController(IStudentRepository studentRepo)
    {
        this.studentRepo = studentRepo;
    }


    public async Task<IActionResult> GetStudents()
    {
        var list = await studentRepo.GetStudentsAsync();
        return View(list);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(StudentViewModel model)
    {
        await studentRepo.AddStudentAsync(model);
        return RedirectToAction("GetStudents");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var student = await studentRepo.GetStudentAsync(id);
        if(student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(StudentViewModel model)
    {
        await studentRepo.EditStudentAsync(model);
        return RedirectToAction("GetStudents");
    }
}
