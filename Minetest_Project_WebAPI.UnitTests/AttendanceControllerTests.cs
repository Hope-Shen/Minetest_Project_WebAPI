using System;
using Xunit;
using Moq;
using AutoMapper;
using Minetest_Project_WebAPI.Controllers;
using Minetest_Project_WebAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Minetest_Project_WebAPI.Services;
using FluentAssertions;
using Minetest_Project_WebAPI.Models;

namespace Minetest_Project_WebAPI.UnitTests
{
    public class AttendanceControllerTests
    {
        private readonly Mock<IAttendanceService> _attendanceService = new();
        private readonly Mock<IMapper> _mapper = new();

        private IEnumerable<AttendanceReadDto> GetMockAttendance()
        {
            var mockAttendances = new List<AttendanceReadDto>()
            {
                new AttendanceReadDto() { CourseId="COMP0097 Minetest Mods", StudentName="student_1,student_2" },
                new AttendanceReadDto() { CourseId="COMP0098 Microbit", StudentName="student_3,student_4,student_5" },
                new AttendanceReadDto() { CourseId="COMP0099 Photoshop", StudentName="student_6" }
            };
            return mockAttendances;
        }
        
        [Fact]
        public void GetAttendance_WithExistingAttendances_ReturnOk()
        {
            // Arrange
            _attendanceService.Setup(repo => repo.GetAttendance()).Returns(GetMockAttendance());
            var controller = new AttendanceController(_attendanceService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.GetAttendance();
            var actual = result as OkObjectResult;

            //Assert
            var expected = new OkObjectResult(GetMockAttendance());
            Assert.Equal(expected.StatusCode, actual.StatusCode);
        }

        [Fact]
        public void GetAttendance_WithOutExistingAttendances_ReturnNotFound()
        {
            // Arrange
            _attendanceService.Setup(repo => repo.GetAttendance()).Returns(new List<AttendanceReadDto>());
            var controller = new AttendanceController(_attendanceService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.GetAttendance();
            var actual = result as NotFoundResult;

            //Assert
            var expected = new NotFoundObjectResult(new List<AttendanceReadDto>());
            Assert.Equal(expected.StatusCode, actual.StatusCode);
        }

        [Fact]
        public void PostAttendance_WithAttendanceCreate_ReturnOk()
        {
            // Arrange
            var createAttendance = new Attendance()
            { CourseId = "COMP0097", StudentId = 1, Date = DateTime.Today };
            _attendanceService.Setup(repo => repo.PostAttendance(It.IsAny<Attendance>())).Returns(1);
            var controller = new AttendanceController(_attendanceService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.PostAttendance(createAttendance);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void PostAttendance_WithOutAttendanceCreate_ReturnBadRequest()
        {
            // Arrange
            _attendanceService.Setup(repo => repo.PostAttendance(It.IsAny<Attendance>())).Returns(0);
            var controller = new AttendanceController(_attendanceService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.PostAttendance(It.IsAny<Attendance>());

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void DeleteAttendance_WithExistingAttendance_ReturnNoContent()
        {
            // Arrange
            var existingEnrollment = new Attendance()
            { CourseId = "COMP0097", StudentId = 1, Date = DateTime.Today };
            _attendanceService.Setup(repo => repo.DeleteAttendance(existingEnrollment)).Returns(1);
            var controller = new AttendanceController(_attendanceService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.DeleteAttendance(existingEnrollment);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void DeleteAttendance_WithOutExistingAttendance_ReturnNotFound()
        {
            // Arrange
            var existingEnrollment = new Attendance()
            { CourseId = "COMP0097", StudentId = 1, Date = DateTime.Today };
            _attendanceService.Setup(repo => repo.DeleteAttendance(existingEnrollment)).Returns(0);
            var controller = new AttendanceController(_attendanceService.Object, _mapper.Object);

            // Act
            ActionResult result = controller.DeleteAttendance(It.IsAny<Attendance>());

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
