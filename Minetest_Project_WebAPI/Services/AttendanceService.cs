using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Minetest_Project_WebAPI.Dtos;
using Minetest_Project_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minetest_Project_WebAPI.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly Minetest_DBContext _context;
        private readonly IMapper _mapper;

        public AttendanceService(Minetest_DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<AttendanceReadDto> GetAttendance()
        {
            var attendances_today = _context.Attendances
                .Where(d => d.Date == DateTime.Now.Date);

            var result =
                (from e in _context.Courses
                 join d in attendances_today
                 on e.CourseId equals d.CourseId into empDept
                 from ed in empDept.DefaultIfEmpty()
                 select new
                 {
                     courseId = e.CourseId + ' ' + e.CourseName,
                     studentName = ed == null ? "" : ed.StudentId + "-" + ed.Student.StudentName
                 })
                .ToList()
                .GroupBy(
                p => p.courseId,
                p => p.studentName,
                (key, g) => new AttendanceReadDto { CourseId = key, StudentName = String.Join(",", g.ToArray()) });

            return _mapper.Map<IEnumerable<AttendanceReadDto>>(result);
        }

        public AttendanceReadDto GetAttendanceByCourseId(string courseId)
        {
            var result = _context.Attendances
                .Include(s => s.Student)
                .Where(c => c.CourseId == courseId)
                .Where(d=> d.Date == DateTime.UtcNow.Date)
                .Select(g =>
                new
                {
                    courseId = g.CourseId + " " + g.Course.CourseName,
                    studentName = g.StudentId + "-" + g.Student.StudentName
                })
                .ToList()
                .GroupBy(
                p => p.courseId,
                p => p.studentName,
                (key, g) => new AttendanceReadDto { CourseId = key, StudentName = String.Join(",", g.ToArray()) })
                .SingleOrDefault();

            return _mapper.Map<AttendanceReadDto>(result);
        }

        public int PostAttendance(Attendance value)
        {
            if (string.IsNullOrEmpty(value.CourseId) || string.IsNullOrEmpty(value.Date.ToString()))
            {
                return 0;
            }
            _context.Attendances.Add(value);
            return _context.SaveChanges();
        }

        public int DeleteAttendance(Attendance value)
        {
            int del_attendanceId = _context.Attendances
                .Where(a => a.CourseId == value.CourseId & a.StudentId == value.StudentId & a.Date == value.Date)
                .Select(s => s.AttendanceId)
                .FirstOrDefault();

            var result = _context.Attendances.Find(del_attendanceId);

            if (result != null)
            {
                _context.Attendances.Remove(result);
            }
            return _context.SaveChanges();
        }
    }
}
