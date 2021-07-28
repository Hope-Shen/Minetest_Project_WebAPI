using AutoMapper;
using Minetest_Project_WebAPI.Dtos;
using Minetest_Project_WebAPI.Models;

namespace Minetest_Project_WebAPI.Profiles
{
    public class StudentReadProfile : Profile
    {
        public StudentReadProfile()
        {
            CreateMap<Student, StudentReadDto>();
        }
        
    }
}
