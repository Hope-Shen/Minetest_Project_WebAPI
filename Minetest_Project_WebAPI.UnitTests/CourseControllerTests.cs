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
using System.Linq;
using FluentAssertions;

namespace Minetest_Project_WebAPI.UnitTests
{
    public class CourseControllerTests
    {
        private readonly Mock<ICourseService> _courseService = new();
        private readonly Mock<IMapper> _mapper = new();

        private IEnumerable<CourseReadDto> GetMockCourse()
        {
            var mockCourses = new List<CourseReadDto>()
            {
                new CourseReadDto() { CourseId="COMP0097", CourseName = "Operating Systems",TeacherId=1, TeacherName = "Teacher_1" },
                new CourseReadDto() { CourseId="COMP0098", CourseName = "Algorithmics",TeacherId=2, TeacherName = "Teacher_2" },
                new CourseReadDto() { CourseId="COMP0099", CourseName = "Interaction Design",TeacherId=2, TeacherName = "Teacher_3" }
            };
            return mockCourses;
        }

        [Fact]
        public void GetCourse_WithExistingCourses_ReturnOk()
        {
            // Arrange
            var existingCourse = GetMockCourse();
            _courseService.Setup(repo => repo.GetCourse()).Returns(existingCourse);
            var controller = new CourseController(_courseService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.GetCourse();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetCourse_WithOutExistingCourses_ReturnNotFound()
        {
            // Arrange
            _courseService.Setup(repo => repo.GetCourse()).Returns(new List<CourseReadDto>());
            var controller = new CourseController(_courseService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.GetCourse();

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetCourseById_WithOutExistingCourses_ReturnOk()
        {
            // Arrange
            var courseById = new CourseReadDto()
                { CourseId = "COMP0099", CourseName = "Interaction Design", TeacherId = 2, TeacherName = "Teacher_3" };
            _courseService.Setup(repo => repo.GetCourseById("COMP0099")).Returns(courseById);
            var controller = new CourseController(_courseService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.GetCourseById("COMP0099");

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetCourseById_WithOutExistingCourses_ReturnNotFound()
        {
            // Arrange
            _courseService.Setup(repo => repo.GetCourseById(It.IsAny<string>())).Returns((CourseReadDto)null);
            var controller = new CourseController(_courseService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.GetCourseById("COMP0000");

            //Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public void PostCourse_WithCourseCreate_ReturnOk()
        {
            // Arrange
            var createCourse = new Course()
            { CourseId = "COMP0001", CourseName = "Interaction Design", TeacherId = 2};
            _courseService.Setup(repo => repo.PostCourse(It.IsAny<Course>())).Returns(1);
            var controller = new CourseController(_courseService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.PostCourse(createCourse);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void PostCourse_WithOutCourseCreate_ReturnBadRequest()
        {
            // Arrange
            _courseService.Setup(repo => repo.PostCourse(It.IsAny<Course>())).Returns(0);
            var controller = new CourseController(_courseService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.PostCourse(It.IsAny<Course>());

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void PutCourse_WithExistingCourse_ReturnNoContent()
        {
            // Arrange
            var existingCourse = new Course()
            { CourseId = "COMP0001", CourseName = "Interaction Design", TeacherId = 2 };
            _courseService.Setup(repo => repo.PutCourse(existingCourse.CourseId, It.IsAny<Course>())).Returns(1);
            var controller = new CourseController(_courseService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.PutCourse("COMP0001", existingCourse);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void PutCourse_WithOutExistingCourse_ReturnNotFoundResult()
        {
            // Arrange
            var existingCourse = new Course()
            { CourseId = "COMP0001", CourseName = "Interaction Design", TeacherId = 2 };
            _courseService.Setup(repo => repo.PutCourse(existingCourse.CourseId, It.IsAny<Course>())).Returns(0);
            var controller = new CourseController(_courseService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.PutCourse("COMP0001", existingCourse);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void PutCourse_WithOutExistingCourse_ReturBadRequestResult()
        {
            // Arrange
            var existingCourse = new Course()
            { CourseId = "COMP0001", CourseName = "Interaction Design", TeacherId = 2 };
            _courseService.Setup(repo => repo.PutCourse(existingCourse.CourseId, It.IsAny<Course>())).Returns(1);
            var controller = new CourseController(_courseService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.PutCourse("COMP0002", existingCourse);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void DeleteCourse_WithExistingCourse_ReturnNoContent()
        {
            // Arrange
            var existingCourse = new Course()
            { CourseId = "COMP0001", CourseName = "Interaction Design", TeacherId = 2 };
            _courseService.Setup(repo => repo.DeleteCourse(existingCourse.CourseId)).Returns(1);
            var controller = new CourseController(_courseService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.DeleteCourse(existingCourse.CourseId);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void DeleteCourse_WithOutExistingCourse_ReturnNotFound()
        {
            // Arrange
            var existingCourse = new Course()
            { CourseId = "COMP0001", CourseName = "Interaction Design", TeacherId = 2 };
            _courseService.Setup(repo => repo.DeleteCourse(existingCourse.CourseId)).Returns(0);
            var controller = new CourseController(_courseService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.DeleteCourse("COMP0099");

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
