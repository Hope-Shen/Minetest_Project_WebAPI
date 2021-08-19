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
    public class TeacherService : ITeacherService
    {
        private readonly Minetest_DBContext _context;
        private readonly IMapper _mapper;

        public TeacherService(Minetest_DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<TeacherReadDto> GetTeacher()
        {
            var result = _context.Teachers;
            return _mapper.Map<IEnumerable<TeacherReadDto>>(result);
        }

        public int PostTeacher(Teacher value)
        {
            if (string.IsNullOrEmpty(value.TeacherName))
            {
                return 0;
            }

            _context.Teachers.Add(value);
            return _context.SaveChanges();
        }

        public int DeleteTeacher(int teacherId)
        {
            var result = _context.Teachers.Find(teacherId);
            if (result != null)
            {
                _context.Teachers.Remove(result);
            }
            return _context.SaveChanges();
        }
    }
}
