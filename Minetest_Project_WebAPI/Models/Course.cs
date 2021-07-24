using System;
using System.Collections.Generic;

#nullable disable

namespace Minetest_Project_WebAPI.Models
{
    public partial class Course
    {
        public Course()
        {
            Attendances = new HashSet<Attendance>();
            Enrollments = new HashSet<Enrollment>();
        }

        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
