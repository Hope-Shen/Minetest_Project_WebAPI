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
    public class AttendanceController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService, IMapper mapper)
        {
            _mapper = mapper;
            _attendanceService = attendanceService;
        }

        // GET: api/<AttendanceController>
        [HttpGet]
        public ActionResult GetAttendance()
        {
            var result = _attendanceService.GetAttendance();
            if (result == null || result.Count() == 0) return NotFound();
            return Ok(result);
        }

        // GET api/<AttendanceController>/5
        [HttpGet("{courseId}")]
        public ActionResult GetAttendanceByCourseId(string courseId)
        {
            var result = _attendanceService.GetAttendanceByCourseId(courseId);

            if (result == null)
            {
                return NotFound("CourseId: " + courseId + " not found.");
            }

            return Ok(result);
        }

        // POST api/<AttendanceController>
        [HttpPost]
        public ActionResult<AttendanceReadDto> PostAttendance([FromBody] Attendance value)
        {
            try
            {
                _attendanceService.PostAttendance(value);
            }
            catch
            {
                return StatusCode(500, "Insert attendance error");
            }
            return Ok();
        }

        // DELETE api/<AttendanceController>/5
        [HttpDelete]
        public ActionResult DeleteAttendance([FromBody] Attendance value)
        {
            if (_attendanceService.DeleteAttendance(value) == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
