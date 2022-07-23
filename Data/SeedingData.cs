using Dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Data;

public static class SeedingData
{
    public static void StudentData(this ModelBuilder modelBuilder)
    {
        var students = ReadData.StudentData();
        modelBuilder.Entity<Student>().HasData(students.ToArray());
    }
}