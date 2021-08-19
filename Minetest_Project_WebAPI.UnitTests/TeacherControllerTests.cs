using System;
using Xunit;
using Moq;
using AutoMapper;
using Minetest_Project_WebAPI.Controllers;
using Minetest_Project_WebAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Minetest_Project_WebAPI.Services;
using Minetest_Project_WebAPI.Models;
using FluentAssertions;

namespace Minetest_Project_WebAPI.UnitTests
{
    public class TeacherControllerTests
    {
        private readonly Mock<ITeacherService> _teacherService = new();
        private readonly Mock<IMapper> _mapper = new();

        private IEnumerable<TeacherReadDto> GetMockTeacher()
        {
            var mockteachers = new List<TeacherReadDto>()
            {
                new TeacherReadDto() { TeacherId=1, TeacherName = "Teacher_1" },
                new TeacherReadDto() { TeacherId=2, TeacherName = "Teacher_2" },
                new TeacherReadDto() { TeacherId=3, TeacherName = "Teacher_3" }
            };
            return mockteachers;
        }
        
        [Fact]
        public void GetTeacher_WithExistingTeachers_ReturnOk()
        {
            // Arrange
            _teacherService.Setup(repo => repo.GetTeacher()).Returns(GetMockTeacher());
            var controller = new TeacherController(_teacherService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.GetTeacher();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetTeacher_WithOutExistingTeachers_ReturnNotFound()
        {
            // Arrange
            _teacherService.Setup(repo => repo.GetTeacher()).Returns(new List<TeacherReadDto>());
            var controller = new TeacherController(_teacherService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.GetTeacher();

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void PostTeacher_WithTeacherCreate_ReturnOk()
        {
            // Arrange
            var createTeacher = new Teacher()
            { TeacherId = 1, TeacherName = "Teacher_1" };
            _teacherService.Setup(repo => repo.PostTeacher(It.IsAny<Teacher>())).Returns(1);
            var controller = new TeacherController(_teacherService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.PostTeacher(createTeacher);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void PostTeacher_WithOutTeacherCreate_ReturnBadRequest()
        {
            // Arrange
            _teacherService.Setup(repo => repo.PostTeacher(It.IsAny<Teacher>())).Returns(0);
            var controller = new TeacherController(_teacherService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.PostTeacher(It.IsAny<Teacher>());

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void DeleteTeacher_WithExistingTeacher_ReturnNoContent()
        {
            // Arrange
            var existingTeacher = new Teacher()
            { TeacherId = 1, TeacherName = "Teacher_1" };
            _teacherService.Setup(repo => repo.DeleteTeacher(existingTeacher.TeacherId)).Returns(1);
            var controller = new TeacherController(_teacherService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.DeleteTeacher(existingTeacher.TeacherId);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void DeleteTeacher_WithOutExistingTeacher_ReturnNotFound()
        {
            // Arrange
            var existingTeacher = new Teacher()
            { TeacherId = 1, TeacherName = "Teacher_1" };
            _teacherService.Setup(repo => repo.DeleteTeacher(existingTeacher.TeacherId)).Returns(0);
            var controller = new TeacherController(_teacherService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.DeleteTeacher(2);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
