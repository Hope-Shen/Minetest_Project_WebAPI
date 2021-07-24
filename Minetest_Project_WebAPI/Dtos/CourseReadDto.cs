using System;
using System.Collections.Generic;

#nullable disable

namespace Minetest_Project_WebAPI.Dtos
{
    public partial class CourseReadDto
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
    }
}
