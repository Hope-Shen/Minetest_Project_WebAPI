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
    public class EnrollmentService : IEnrollmentService
    {
        private readonly Minetest_DBContext _context;
        private readonly IMapper _mapper;

        public EnrollmentService(Minetest_DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<EnrollmentReadDto> GetEnrollment()
        {
            var result =
                (from c in _context.Courses
                 join d in _context.Enrollments
                 on c.CourseId equals d.CourseId into empDept
                 from ed in empDept.DefaultIfEmpty()
                 select new
                 {
                     courseId = c.CourseId + ' ' + c.CourseName,
                     studentName = ed == null ? "" : ed.StudentId + "-" + ed.Student.StudentName
                 })
                .ToList()
                .GroupBy(
                p => p.courseId,
                p => p.studentName,
                (key, g) => new EnrollmentReadDto { CourseId = key, StudentName = String.Join(",", g.ToArray()) });

            return _mapper.Map<IEnumerable<EnrollmentReadDto>>(result);
        }

        public int PostEnrollment(Enrollment value)
        {
            if (string.IsNullOrEmpty(value.CourseId) || string.IsNullOrEmpty(value.StudentId.ToString()))
            {
                return 0;
            }

            _context.Enrollments.Add(value);
            return _context.SaveChanges();
        }

        public int DeleteEnrollment(int enrollmentId)
        {
            var result = _context.Enrollments.Find(enrollmentId);
            if (result != null)
            {
                _context.Enrollments.Remove(result);
            }
            return _context.SaveChanges();
        }
    }
}
