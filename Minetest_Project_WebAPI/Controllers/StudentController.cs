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
    public class StudentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _mapper = mapper;
            _studentService = studentService;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public ActionResult<StudentReadDto> GetStudent()
        {
            var result = _studentService.GetStudent();
            if (result == null || result.Count() == 0) return NotFound();
            return Ok(result);
        }
    }
}
