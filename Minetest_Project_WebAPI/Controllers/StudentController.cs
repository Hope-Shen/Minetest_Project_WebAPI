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
    public class StudentController : ControllerBase
    {
        private readonly Minetest_DBContext _context;
        private readonly IMapper _mapper;

        // GET: api/<StudentController>
        public StudentController(Minetest_DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public ActionResult<StudentReadDto> GetStudent()
        {
            var result = _context.Students;
            if (result == null || result.Count() == 0) return NotFound();

            return Ok(_mapper.Map<IEnumerable<StudentReadDto>>(result));
        }
    }
}
