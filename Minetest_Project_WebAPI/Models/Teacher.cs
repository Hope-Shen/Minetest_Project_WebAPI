using System;
using System.Collections.Generic;

#nullable disable

namespace Minetest_Project_WebAPI.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Courses = new HashSet<Course>();
        }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
