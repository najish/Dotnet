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
            int id = int.Parse(data[0]);
            string name = data[1];
            string address = data[2];

            var student = new Student {Name = name, Address = address, Id = id};
            list.Add(student);
        }
        return list;
    }
}