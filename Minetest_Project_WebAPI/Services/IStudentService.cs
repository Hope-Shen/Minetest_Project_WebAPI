using Minetest_Project_WebAPI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minetest_Project_WebAPI.Services
{
    public interface IStudentService
    {
        IEnumerable<StudentReadDto> GetStudent();
    }
}
