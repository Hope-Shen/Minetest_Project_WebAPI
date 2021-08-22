using Minetest_Project_WebAPI.Dtos;
using Minetest_Project_WebAPI.Models;
using System;
using System.Collections.Generic;

namespace Minetest_Project_WebAPI.Services
{
    public interface IEnrollmentService
    {
        IEnumerable<EnrollmentReadDto> GetEnrollment();
        int PostEnrollment(Enrollment value);
        int DeleteEnrollment(int enrollmentId);
    }
}
