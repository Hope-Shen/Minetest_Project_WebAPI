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
            var result = _context.Enrollments
                .Include(s => s.Student)
                .Select(g =>
                new
                {
                    courseId = g.CourseId + " " + g.Course.CourseName,
                    studentName = g.StudentId + "-" + g.Student.StudentName
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

        public int DeleteEnrollment(Enrollment value)
        {
            int del_enrollmentId = _context.Enrollments
                .Where(a => a.CourseId == value.CourseId & a.StudentId == value.StudentId)
                .Select(s => s.EnrollmentId)
                .FirstOrDefault();

            var result = _context.Enrollments.Find(del_enrollmentId);

            if (result != null)
            {
                _context.Enrollments.Remove(result);
            }
            return _context.SaveChanges();
        }
    }
}
