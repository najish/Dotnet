using Dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Data;

public static class SeedingData
{
    public static void StudentData(this ModelBuilder modelBuilder)
    {
        var students = ReadData.StudentData();
        var moredata = new Student[] 
        {
            new Student
            {
                Id = 1,
                Name = "Najish Eqbal",
                Address = "Rajabagicha"
            },
            new Student
            {
                Id = 2,
                Name = "Danish Eqbal",
                Address = "Daudnagar"
            },
            new Student
            {
                Id = 3,
                Name = "Yasin Ekbal",
                Address = "Ranchi"
            },
            new Student
            {
                Id = 4,
                Name = "Taukir Khan",
                Address = "MaharajGanj"
            },
            new Student
            {
                Id = 5, 
                Name = "Bilal Shaeed",
                Address = "Pakistan"
            }
        };

        students.AddRange(moredata);
        modelBuilder.Entity<Student>().HasData(students.ToArray());
    }
}