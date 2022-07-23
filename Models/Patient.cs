using System.ComponentModel.DataAnnotations;

namespace Dotnet.models;

public enum Status
{
    Minor,
    Normal,
    Critical    
}

public class Patient
{
    [Required]
    public int Id {get;set;}
    [Required]
    public string Name {get;set;}
    [Display(Name = "DOB")]
    public DateTime DateOfBirth {get;set;}

    [Required]
    public DateTime Admit {get;set;}
    [Required]
    public Status Status {get;set;}
    [Required]
    [MinLength(10),MaxLength(500)]
    public string Description {get;set;}
}