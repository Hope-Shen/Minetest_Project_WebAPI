using Minetest_Project_WebAPI.Dtos;
using Minetest_Project_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minetest_Project_WebAPI.Services
{
    public interface IAttendanceService
    {
        IEnumerable<AttendanceReadDto> GetAttendance();
        AttendanceReadDto GetAttendanceByCourseId(string courseId);
        void PostAttendance(Attendance value);
        int DeleteAttendance(Attendance value);
    }
}
