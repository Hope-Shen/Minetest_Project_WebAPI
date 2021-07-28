using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minetest_Project_WebAPI.Dtos;
using Minetest_Project_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minetest_Project_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly Minetest_DBContext _context;
        private readonly IMapper _mapper;

        public AttendanceController(Minetest_DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<AttendanceController>
        [HttpGet]
        public ActionResult<AttendanceReadDto> GetAttendance()
        {
            var result = _context.Attendances
                .Include(s => s.Student)
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
                (key, g) => new AttendanceReadDto { CourseId = key, StudentName = String.Join(",", g.ToArray()) });

            if (result == null || result.Count() == 0) return NotFound();

            return Ok(_mapper.Map<IEnumerable<AttendanceReadDto>>(result));
        }

        // GET api/<AttendanceController>/5
        [HttpGet("{courseId}")]
        public ActionResult<AttendanceReadDto> GetAttendanceByCourseId(string courseId)
        {
            var result = _context.Attendances
                .Include(s => s.Student)
                .Where(c => c.CourseId == courseId)
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

            if (result == null)
            {
                return NotFound("CourseId: " + courseId + " not found.");
            }

            return Ok(_mapper.Map<AttendanceReadDto>(result));
        }

        // POST api/<AttendanceController>
        [HttpPost]
        public ActionResult<AttendanceReadDto> PostAttendance([FromBody] Attendance value)
        {
            _context.Attendances.Add(value);
            _context.SaveChanges();

            return Ok();
        }

        // DELETE api/<AttendanceController>/5
        [HttpDelete]
        public ActionResult DeleteAttendance([FromBody] Attendance value)
        {
            int del_attendanceId = _context.Attendances
                .Where(a=> a.CourseId == value.CourseId & a.StudentId == value.StudentId & a.Date == value.Date)
                .Select(s => s.AttendanceId)
                .FirstOrDefault();

            var result = _context.Attendances.Find(del_attendanceId);
            if (result == null)
            {
                return NotFound();
            }

            _context.Attendances.Remove(result);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
