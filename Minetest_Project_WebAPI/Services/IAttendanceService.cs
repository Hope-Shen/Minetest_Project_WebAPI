using Minetest_Project_WebAPI.Dtos;
using Minetest_Project_WebAPI.Models;
using System;
using System.Collections.Generic;

namespace Minetest_Project_WebAPI.Services
{
    public interface IAttendanceService
    {
        IEnumerable<AttendanceReadDto> GetAttendance();
        AttendanceReadDto GetAttendanceByCourseId(string courseId);
        int PostAttendance(Attendance value);
        int DeleteAttendance(Attendance value);
    }
}
