using System;
using System.Collections.Generic;

#nullable disable

namespace Minetest_Project_WebAPI.Dtos
{
    public class AttendanceReadDto
    {
        //public int AttendanceId { get; set; }
        public string CourseId { get; set; }
        //public string CourseName { get; set; }
        //public int StudentId { get; set; }
        public string StudentName { get; set; }
    }
}
