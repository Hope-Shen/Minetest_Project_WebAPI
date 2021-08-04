using Minetest_Project_WebAPI.Dtos;
using Minetest_Project_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minetest_Project_WebAPI.Services
{
    public interface ICourseService
    {
        IEnumerable<CourseReadDto> GetCourse();
        CourseReadDto GetCourseById(string courseId);
        void PostCourse(Course value);int PutCourse(string courseId, Course value);
        int DeleteCourse(string courseId);
    }
}
