using AutoMapper;
using Minetest_Project_WebAPI.Dtos;
using Minetest_Project_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minetest_Project_WebAPI.Profiles
{
    public class CourseReadProfile : Profile
    {
        public CourseReadProfile()
        {
            CreateMap<Course, CourseReadDto>()
                .ForMember(
                t => t.TeacherName,
                s => s.MapFrom(c => c.Teacher.TeacherName)
                );
        }
        
    }
}
