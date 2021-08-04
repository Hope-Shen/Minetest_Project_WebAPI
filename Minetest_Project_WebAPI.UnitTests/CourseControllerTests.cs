using System;
using Xunit;
using Moq;
using AutoMapper;
using Minetest_Project_WebAPI.Controllers;
using Minetest_Project_WebAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Minetest_Project_WebAPI.Services;

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
            _courseService.Setup(repo => repo.GetCourse()).Returns(GetMockCourse());
            var controller = new CourseController(_courseService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.GetCourse();
            var actual = result as OkObjectResult;

            //Assert
            var expected = new OkObjectResult(GetMockCourse());
            Assert.Equal(expected.StatusCode, actual.StatusCode);
        }

        [Fact]
        public void GetCourse_WithOutExistingCourses_ReturnNotFound()
        {
            // Arrange
            _courseService.Setup(repo => repo.GetCourse()).Returns(new List<CourseReadDto>());
            var controller = new CourseController(_courseService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.GetCourse();
            var actual = result as NotFoundResult;

            //Assert
            var expected = new NotFoundObjectResult(new List<CourseReadDto>());
            Assert.Equal(expected.StatusCode, actual.StatusCode);
        }


    }
}
