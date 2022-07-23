using System.ComponentModel.DataAnnotations;

namespace Dotnet.Models;

public class Doctor 
{
    [Required]
    public int Id {get;set;}
    [Required]

    public string Name {get;set;}
    [Required]

    public string Department {get;set;}
    [Required]

    public string Number {get;set;}
    [Required]

    public string Specialist {get;set;}

}