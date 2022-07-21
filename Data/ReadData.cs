using System;
using System.IO;
using Dotnet.Models;

namespace Dotnet.Data;

public class ReadData
{
    public static List<Student> StudentData()
    {
        string[] lines = File.ReadAllLines(@"F:\Dotnet\TextData\StudentData.txt");

        var list = new List<Student>();
        for(int i = 1; i < lines.Length; i++)
        {
            string[] data = lines[i].Split(',');
            string name = data[0];
            string address = data[1];

            var student = new Student {Name = name, Address = address};
            list.Add(student);
        }
        return list;
    }
}