using Minetest_Project_WebAPI.Dtos;
using Minetest_Project_WebAPI.Models;
using System;
using System.Collections.Generic;

namespace Minetest_Project_WebAPI.Services
{
    public interface ITeacherService
    {
        IEnumerable<TeacherReadDto> GetTeacher();
        int PostTeacher(Teacher value);
        int DeleteTeacher(int teacherId);
    }
}
