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
    public class EnrollmentControllerTests
    {
        private readonly Mock<IEnrollmentService> _enrollmentService = new();
        private readonly Mock<IMapper> _mapper = new();

        private IEnumerable<EnrollmentReadDto> GetMockEnrollment()
        {
            var mockEnrollments = new List<EnrollmentReadDto>()
            {
                new EnrollmentReadDto() { CourseId="COMP0097 Minetest Mods", StudentName="student_1,student_2" },
                new EnrollmentReadDto() { CourseId="COMP0098 Microbit", StudentName="student_3,student_4,student_5" },
                new EnrollmentReadDto() { CourseId="COMP0099 Photoshop", StudentName="student_6" }
            };
            return mockEnrollments;
        }
        
        [Fact]
        public void GetEnrollment_WithExistingEnrollments_ReturnOk()
        {
            // Arrange
            _enrollmentService.Setup(repo => repo.GetEnrollment()).Returns(GetMockEnrollment());
            var controller = new EnrollmentController(_enrollmentService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.GetEnrollment();
            var actual = result as OkObjectResult;

            //Assert
            var expected = new OkObjectResult(GetMockEnrollment());
            Assert.Equal(expected.StatusCode, actual.StatusCode);
        }

        [Fact]
        public void GetEnrollment_WithOutExistingEnrollments_ReturnNotFound()
        {
            // Arrange
            _enrollmentService.Setup(repo => repo.GetEnrollment()).Returns(new List<EnrollmentReadDto>());
            var controller = new EnrollmentController(_enrollmentService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.GetEnrollment();
            var actual = result as NotFoundResult;

            //Assert
            var expected = new NotFoundObjectResult(new List<EnrollmentReadDto>());
            Assert.Equal(expected.StatusCode, actual.StatusCode);
        }
    }
}
