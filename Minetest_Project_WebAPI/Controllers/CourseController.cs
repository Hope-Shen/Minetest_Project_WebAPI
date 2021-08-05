using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minetest_Project_WebAPI.Dtos;
using Minetest_Project_WebAPI.Models;
using Minetest_Project_WebAPI.Services;
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
        private readonly IMapper _mapper;
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService, IMapper mapper)
        {
            _mapper = mapper;
            _courseService = courseService;
        }

        // GET: api/<CourseController>
        [HttpGet]
        public ActionResult GetCourse()
        {
            var result = _courseService.GetCourse();
            if (result == null || result.Count() == 0) return NotFound();
            return Ok(result);
        }

        // GET api/<CourseController>/5
        [HttpGet("{courseId}")]
        public ActionResult GetCourseById(string courseId)
        {
            var result = _courseService.GetCourseById(courseId);
            if (result == null) return NotFound("CourseId: " + courseId + " not found.");
            return Ok(result);
        }

        // POST api/<CourseController>
        [HttpPost]
        public ActionResult<Course> PostCourse([FromBody] Course value)
        {
            try
            {
                _courseService.PostCourse(value);
            }
            catch
            {
                return StatusCode(500, "Insert course error");
            }

            return Ok();
        }

        // PUT api/<CourseController>/5
        [HttpPut("{courseId}")]
        public ActionResult PutCourse(string courseId, [FromBody] Course value)
        {
            if (courseId != value.CourseId)
            {
                return BadRequest();
            }

            try
            {
                if (_courseService.PutCourse(courseId, value) == 0)
                {
                    return NotFound();
                }
            }
            catch
            {
                return StatusCode(500, "Update course error");
            }
            return NoContent();
        }

        // DELETE api/<CourseController>/5
        [HttpDelete("{courseId}")]
        public ActionResult DeleteCourse(string courseId)
        {
            if (_courseService.DeleteCourse(courseId) == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
