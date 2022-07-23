using System.ComponentModel.DataAnnotations;

namespace Dotnet.ViewModels;

public class StudentViewModel
{
    public int Id {get;set;}
    [Required]
    public string? Name {get;set;}
    [Required]
    public string? Address {get;set;}
    [Required]
    public DateTime Enrollment {get;set;}
}