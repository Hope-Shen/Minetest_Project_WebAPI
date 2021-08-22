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
    public class EnrollmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService, IMapper mapper)
        {
            _mapper = mapper;
            _enrollmentService = enrollmentService;
        }

        // GET: api/<EnrollmentController>
        [HttpGet]
        public ActionResult GetEnrollment()
        {
            var result = _enrollmentService.GetEnrollment();
            if (result == null || result.Count() == 0) return NotFound();
            return Ok(result);
        }

        // POST api/<EnrollmentController>
        [HttpPost]
        public ActionResult PostEnrollment([FromBody] Enrollment value)
        {
            try
            {
                if (_enrollmentService.PostEnrollment(value) == 0)
                {
                    return BadRequest();
                }
            }
            catch
            {
                return StatusCode(500, "Insert enrollment error");
            }

            return Ok(new { EnrollmentId = value.EnrollmentId, CourseId = value.CourseId, StudentId = value.StudentId });
        }

        // DELETE api/<EnrollmentController>/5
        [HttpDelete("{enrollmentId}")]
        public ActionResult DeleteEnrollment(int enrollmentId)
        {
            if (_enrollmentService.DeleteEnrollment(enrollmentId) == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
