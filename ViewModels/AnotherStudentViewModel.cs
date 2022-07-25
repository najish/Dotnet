namespace Dotnet.ViewModels;
using Dotnet.Models;
public class AnotherStudentViewModel
{
    public Student Student {get;set;} = new Student();

    public Doctor Doctor {get;set;} = new Doctor();
}