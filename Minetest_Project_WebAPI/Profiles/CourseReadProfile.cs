using AutoMapper;
using Minetest_Project_WebAPI.Dtos;
using Minetest_Project_WebAPI.Models;

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
