using Dotnet.Models;
using Dotnet.Repository;
using Dotnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Dotnet.Controllers;

public class StudentController : Controller
{
    private readonly IStudentRepository studentRepo;
    private static int currentPage = 1,LastPage;
    private const int dataPerPage = 5, FirstPage = 1;
    private static int totalPage;
    public StudentController(IStudentRepository studentRepo)
    {
        this.studentRepo = studentRepo;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetStudents(string sortOrder,string searchString,int page = 0)
    {
        ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewData["AddressSortParm"] = String.IsNullOrEmpty(sortOrder) ? "address_desc" : "";
        ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
        ViewData["CurrentFilter"] = searchString;
        var list = await studentRepo.GetStudentsAsync();
        var students = from student in list
                       select student;

        if(String.IsNullOrEmpty(searchString) == false)
        {
            students = students.Where(std => std.Name.Contains(searchString));
        }
        switch(sortOrder)
        {
            case "id_desc":
                students = students.OrderByDescending(x=> x.Id);
                break;
            case "name_desc":
                students = students.OrderByDescending(x => x.Name);
                break;
            case "address_desc":
                students = students.OrderByDescending(x => x.Address);
                break;
            default:
                students = students.OrderBy(x => x.Id);
                break;
        }
        int total = students.Count();
        LastPage = (int)Math.Floor((total * 1.0 ) / dataPerPage);
        if(page > currentPage && page <= LastPage)
        {
            currentPage = page;
            students = students.Skip(dataPerPage * page);
            if(students.Count() >= dataPerPage)
                students = students.Take(dataPerPage);
        }
        else if(page < currentPage && page >= FirstPage)
        {
            currentPage = page;
            students = students.Skip((page-1) * dataPerPage);
            if(students.Count() >= dataPerPage)
                students = students.Take(dataPerPage);
        }
        else if(page > LastPage)
        {
            page = 0;
        }
        if(page == 0) page = currentPage;
        ViewData["page"] = page;
        currentPage = page;

        if(students.Count() > dataPerPage)
            students = students.Take(dataPerPage);
        return View(students);
    }


    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(StudentViewModel model)
    {

        if(ModelState.IsValid)
        {
            await studentRepo.AddStudentAsync(model);
            return RedirectToAction("GetStudents");
        }

        ModelState.AddModelError("","Failed to add");
        return View(model);
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
        if(ModelState.IsValid)
        {
            await studentRepo.EditStudentAsync(model);
            return RedirectToAction("GetStudents");
        }

        ModelState.AddModelError("","Failed to Edit");
        return View(model);
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
