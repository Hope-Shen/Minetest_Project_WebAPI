using Minetest_Project_WebAPI.Dtos;
using Minetest_Project_WebAPI.Models;
using System;
using System.Collections.Generic;

namespace Minetest_Project_WebAPI.Services
{
    public interface IStudentService
    {
        IEnumerable<StudentReadDto> GetStudent();
        int PostStudent(Student value);
        int DeleteStudent(int studentId);
    }
}
