using System;
using System.Collections.Generic;

#nullable disable

namespace Minetest_Project_WebAPI.Models
{
    public partial class Attendance
    {
        public int AttendanceId { get; set; }
        public string CourseId { get; set; }
        public int StudentId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
