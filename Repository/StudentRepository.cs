using Dotnet.Data;
using Dotnet.Models;
using Dotnet.ViewModels;
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
    public async Task AddStudent(StudentViewModel model)
    {
        var student = new Student
        {
            Name = model.Name,
            Address = model.Address
        };
        context.Students.Add(student);
        await context.SaveChangesAsync();
    }
}