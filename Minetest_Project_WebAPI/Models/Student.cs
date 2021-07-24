using System;
using System.Collections.Generic;

#nullable disable

namespace Minetest_Project_WebAPI.Models
{
    public partial class Student
    {
        public Student()
        {
            Attendances = new HashSet<Attendance>();
            Enrollments = new HashSet<Enrollment>();
        }

        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
