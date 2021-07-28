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
    public class CourseController : ControllerBase
    {
        private readonly Minetest_DBContext _context;
        private readonly IMapper _mapper;

        public CourseController(Minetest_DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<CourseController>
        [HttpGet]
        public ActionResult<CourseReadDto> GetCourse()
        {
            var result = _context.Courses
                .Include(t => t.Teacher);
            //.Select(a => new CourseReadDto
            //{
            //    CourseId = a.CourseId,
            //    CourseName = a.CourseName,
            //    TeacherId = a.TeacherId,
            //    TeacherName = a.Teacher.TeacherName
            //});
            if (result == null || result.Count() == 0) return NotFound();

            return Ok(_mapper.Map<IEnumerable<CourseReadDto>>(result));
        }

        // GET api/<CourseController>/5
        [HttpGet("{courseId}")]
        public ActionResult<CourseReadDto> GetCourseById(string courseId)
        {
            var result = _context.Courses
                .Include(t => t.Teacher)
                .Where(c => c.CourseId == courseId)
                .SingleOrDefault();

            if (result == null)
            {
                return NotFound("CourseId: " + courseId + " not found.");
            }

            return Ok(_mapper.Map<CourseReadDto>(result));
        }

        // POST api/<CourseController>
        [HttpPost]
        public ActionResult<Course> PostCourse([FromBody] Course value)
        {
            _context.Courses.Add(value);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCourseById), new { id = value.CourseId }, value);
        }

        // PUT api/<CourseController>/5
        [HttpPut("{courseId}")]
        public ActionResult PutCourse(string courseId, [FromBody] Course value)
        {
            if (courseId != value.CourseId)
            {
                return BadRequest();
            }

            _context.Entry(value).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (!_context.Courses.Any(e => e.CourseId == courseId))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, "Update error");
                }
            }
            return NoContent();
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{courseId}")]
        public ActionResult DeleteCourse(string courseId)
        {
            var result = _context.Courses.Find(courseId);
            if (result == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(result);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
