using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Minetest_Project_WebAPI.Dtos;
using Minetest_Project_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minetest_Project_WebAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly Minetest_DBContext _context;
        private readonly IMapper _mapper;

        public StudentService(Minetest_DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<StudentReadDto> GetStudent()
        {
            var result = _context.Students;
            return _mapper.Map<IEnumerable<StudentReadDto>>(result);
        }
    }
}
