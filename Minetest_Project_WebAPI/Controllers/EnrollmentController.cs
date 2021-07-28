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
    public class EnrollmentController : ControllerBase
    {
        private readonly Minetest_DBContext _context;
        private readonly IMapper _mapper;

        public EnrollmentController(Minetest_DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<EnrollmentController>
        [HttpGet]
        public ActionResult<EnrollmentReadDto> GetCourse()
        {
            var result = _context.Enrollments
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
                (key, g) => new EnrollmentReadDto { CourseId = key, StudentName = String.Join(",", g.ToArray()) });

            if (result == null || result.Count() == 0) return NotFound();

            return Ok(_mapper.Map<IEnumerable<EnrollmentReadDto>>(result));
        }
    }
}
