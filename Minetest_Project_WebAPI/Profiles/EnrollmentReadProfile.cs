using AutoMapper;
using Minetest_Project_WebAPI.Dtos;
using Minetest_Project_WebAPI.Models;

namespace Minetest_Project_WebAPI.Profiles
{
    public class EnrollmentReadProfile : Profile
    {
        public EnrollmentReadProfile()
        {
            CreateMap<Enrollment, EnrollmentReadDto>()
                .ForMember(
                    s => s.StudentName,
                    s => s.MapFrom(c => c.Student.StudentName)
                );
        }
    }
}
