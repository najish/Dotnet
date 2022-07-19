using Dotnet.ViewModels;
using Dotnet.Models;
using System.Collections.Generic;
namespace Dotnet.Repository;

public interface IStudentRepository
{
    Task AddStudentAsync(StudentViewModel student);
    Task<List<StudentViewModel>> GetStudentsAsync();
    Task<StudentViewModel> GetStudentAsync(int id);
    Task EditStudentAsync(StudentViewModel model);

}