using Dotnet.ViewModels;

namespace Dotnet.Repository;

public interface IStudentRepository
{
    void AddStudent(StudentViewModel student);
}