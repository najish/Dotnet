using Dotnet.Data;
using Dotnet.Models;
using Dotnet.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;

namespace Dotnet.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext context;
    public StudentRepository(AppDbContext context)
    {
        this.context = context;
    }
    public async Task AddStudentAsync(StudentViewModel model)
    {
        var student = new Student
        {
            Name = model.Name,
            Address = model.Address
        };
        context.Students.Add(student);
        await context.SaveChangesAsync();
    }

    public async Task<List<StudentViewModel>> GetStudentsAsync()
    {
        var model = await context.Students.ToListAsync();
        var list = new List<StudentViewModel>();
        foreach(var student in model)
        {
            list.Add(new StudentViewModel{ Id = student.Id, Name = student.Name, Address = student.Address});
        }
        return list;
    }

    public async Task<StudentViewModel> GetStudentAsync(int id)
    {
        var model = await context.Students.FindAsync(id);
        var student = new StudentViewModel{Id = model.Id, Name = model.Name, Address = model.Address};
        return student;
    }


    public async Task EditStudentAsync(StudentViewModel model)
    {
        var student = new Student
        {
            Id = model.Id,
            Name = model.Name,
            Address = model.Address
        };

        context.Students.Update(student);
        await context.SaveChangesAsync();
    }

    public async Task DeleteStudentAsync(StudentViewModel model)
    {
        var student = context.Students.Find(model.Id);
        if(student != null)
            context.Students.Remove(student);
        await context.SaveChangesAsync();
    }
}