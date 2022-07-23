using Dotnet.Models;
using Dotnet.Repository;
using Dotnet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Dotnet.Controllers;


[AllowAnonymous]
public class StudentController : Controller
{
    private readonly IStudentRepository studentRepo;
    private static int currentPage = 1,LastPage;
    private const int dataPerPage = 10, FirstPage = 1;
    public StudentController(IStudentRepository studentRepo)
    {
        this.studentRepo = studentRepo;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetStudents(string sortOrder,string searchString,int page = 1)
    {
        var list = await studentRepo.GetStudentsAsync();
        var students = from student in list 
                       select student;

        
        
        ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_asc" : "name_desc";
        ViewData["AddressSortParm"] = string.IsNullOrEmpty(sortOrder) ? "address_asc" : "address_desc";
        

        switch(sortOrder)
        {
            case "name_desc":
                students = students.OrderByDescending(x => x.Name);
                ViewData["NameSortParm"] = "name_asc";
                break;
            case "name_asc":
                students = students.OrderBy(x => x.Name);
                ViewData["NameSortParm"] = "name_desc";
                break;
            case "address_desc":
                students = students.OrderByDescending(x => x.Address);
                ViewData["AddressSortParm"] = "address_asc";
                break;
            case "address_asc":
                students = students.OrderBy(x => x.Address);
                ViewData["AddressSortParm"] = "address_desc";
                break;
            default:
                students = students.OrderBy(x => x.Id);
                break;
        }
        
        LastPage = (int)Math.Ceiling((list.Count() * 1.0) / dataPerPage);
        




        if(page <= FirstPage)
        {
            students = students.Take(dataPerPage);
            page = currentPage = 1;
            ViewBag.Page = page;
        }
        else if(page >= LastPage)
        {

            students = students.TakeLast(dataPerPage);
            page = currentPage = LastPage;
            ViewBag.Page = page;
        }
        else 
        {
            if(page < currentPage)
            {
                students = students.Skip(dataPerPage * (page - 1)).Take(dataPerPage);
                ViewBag.Page = currentPage = page;
            }
            else 
            {
                students = students.Skip(dataPerPage * currentPage).Take(dataPerPage);
                currentPage = page;
                ViewBag.Page = currentPage;
            }
        }
       
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
