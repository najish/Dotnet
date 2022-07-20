using Dotnet.Models;
using Dotnet.Repository;
using Dotnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Controllers;

[Authorize]
public class StudentController : Controller
{
    private readonly IStudentRepository studentRepo;
    public StudentController(IStudentRepository studentRepo)
    {
        this.studentRepo = studentRepo;
    }

    [AllowAnonymous]
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


    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var model = await studentRepo.GetStudentAsync(id);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(StudentViewModel student)
    {
        await studentRepo.DeleteStudentAsync(student);
        return RedirectToAction("GetStudents");
    }
}
