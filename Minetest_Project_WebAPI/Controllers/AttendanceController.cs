﻿using AutoMapper;
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

        // GET: api/<TodoController>
        public AttendanceController(Minetest_DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<ValuesController>
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

        // GET api/<CourseController>/5
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

        // POST api/<CourseController>
        [HttpPost]
        public ActionResult<AttendanceReadDto> PostAttendance([FromBody] Attendance value)
        {
            _context.Attendances.Add(value);
            _context.SaveChanges();

            return Ok();
        }
    }
}
