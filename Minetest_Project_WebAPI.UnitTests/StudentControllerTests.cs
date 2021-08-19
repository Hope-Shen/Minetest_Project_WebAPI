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
    public class StudentControllerTests
    {
        private readonly Mock<IStudentService> _studentService = new();
        private readonly Mock<IMapper> _mapper = new();

        private IEnumerable<StudentReadDto> GetMockStudent()
        {
            var mockstudents = new List<StudentReadDto>()
            {
                new StudentReadDto() { StudentId=1, StudentName = "Student_1" },
                new StudentReadDto() { StudentId=2, StudentName = "Student_2" },
                new StudentReadDto() { StudentId=3, StudentName = "Student_3" }
            };
            return mockstudents;
        }
        
        [Fact]
        public void GetStudent_WithExistingStudents_ReturnOk()
        {
            // Arrange
            _studentService.Setup(repo => repo.GetStudent()).Returns(GetMockStudent());
            var controller = new StudentController(_studentService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.GetStudent();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetStudent_WithOutExistingStudents_ReturnNotFound()
        {
            // Arrange
            _studentService.Setup(repo => repo.GetStudent()).Returns(new List<StudentReadDto>());
            var controller = new StudentController(_studentService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.GetStudent();

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void PostStudent_WithStudentCreate_ReturnOk()
        {
            // Arrange
            var createStudent = new Student()
            { StudentId = 1, StudentName = "Student_1"};
            _studentService.Setup(repo => repo.PostStudent(It.IsAny<Student>())).Returns(1);
            var controller = new StudentController(_studentService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.PostStudent(createStudent);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void PostStudent_WithOutStudentCreate_ReturnBadRequest()
        {
            // Arrange
            _studentService.Setup(repo => repo.PostStudent(It.IsAny<Student>())).Returns(0);
            var controller = new StudentController(_studentService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.PostStudent(It.IsAny<Student>());

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void DeleteStudent_WithExistingStudent_ReturnNoContent()
        {
            // Arrange
            var existingStudent = new Student()
            { StudentId = 1, StudentName = "Student_1" };
            _studentService.Setup(repo => repo.DeleteStudent(existingStudent.StudentId)).Returns(1);
            var controller = new StudentController(_studentService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.DeleteStudent(existingStudent.StudentId);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void DeleteStudent_WithOutExistingStudent_ReturnNotFound()
        {
            // Arrange
            var existingStudent = new Student()
            { StudentId = 1, StudentName = "Student_1" };
            _studentService.Setup(repo => repo.DeleteStudent(existingStudent.StudentId)).Returns(0);
            var controller = new StudentController(_studentService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.DeleteStudent(2);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
