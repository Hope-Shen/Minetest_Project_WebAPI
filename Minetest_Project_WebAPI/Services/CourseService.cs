using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Minetest_Project_WebAPI.Dtos;
using Minetest_Project_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minetest_Project_WebAPI.Services
{
    public class CourseService : ICourseService
    {
        private readonly Minetest_DBContext _context;
        private readonly IMapper _mapper;

        public CourseService(Minetest_DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<CourseReadDto> GetCourse()
        {
            var result = _context.Courses
                .Include(t => t.Teacher);

            return _mapper.Map<IEnumerable<CourseReadDto>>(result);
        }

        public CourseReadDto GetCourseById(string courseId)
        {
            var result = _context.Courses
                .Include(t => t.Teacher)
                .Where(c => c.CourseId == courseId)
                .SingleOrDefault();

            return _mapper.Map<CourseReadDto>(result);
        }

        public void PostCourse(Course value)
        {
            _context.Courses.Add(value);
            _context.SaveChanges();
        }

        public int PutCourse(string courseId, Course value)
        {
            _context.Entry(value).State = EntityState.Modified;
            return _context.SaveChanges();
        }

        public int DeleteCourse(string courseId)
        {
            var result = _context.Courses.Find(courseId);
            if (result != null)
            {
                _context.Courses.Remove(result);
            }
            
            return _context.SaveChanges();
        }
    }
}
