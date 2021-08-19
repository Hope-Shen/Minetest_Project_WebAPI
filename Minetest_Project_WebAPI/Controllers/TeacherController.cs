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
    public class TeacherController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService, IMapper mapper)
        {
            _mapper = mapper;
            _teacherService = teacherService;
        }

        // GET: api/<TeacherController>
        [HttpGet]
        public ActionResult GetTeacher()
        {
            var result = _teacherService.GetTeacher();
            if (result == null || result.Count() == 0) return NotFound();
            return Ok(result);
        }

        // POST api/<TeacherController>
        [HttpPost]
        public ActionResult PostTeacher([FromBody] Teacher value)
        {
            try
            {
                if (_teacherService.PostTeacher(value) == 0)
                {
                    return BadRequest();
                }
            }
            catch
            {
                return StatusCode(500, "Insert teacher error");
            }

            return Ok(new { TeacherId = value.TeacherId, TeacherName = value.TeacherName });
        }


        // DELETE api/<TeacherController>/5
        [HttpDelete("{teacherId}")]
        public ActionResult DeleteTeacher(int teacherId)
        {
            if (_teacherService.DeleteTeacher(teacherId) == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
