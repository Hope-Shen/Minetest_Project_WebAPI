﻿using AutoMapper;
using Minetest_Project_WebAPI.Dtos;
using Minetest_Project_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minetest_Project_WebAPI.Profiles
{
    public class AttendanceReadProfile : Profile
    {
        public AttendanceReadProfile()
        {
            CreateMap<Attendance, AttendanceReadDto>()
                //.ForMember(
                //c => c.CourseName,
                //s => s.MapFrom(c => c.Course.CourseName)
                //)
                .ForMember(
                s => s.StudentName, 
                s => s.MapFrom(c => c.Student.StudentName)
                );
        }

    }
}