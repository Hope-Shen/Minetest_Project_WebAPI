using FluentAssertions;
using Minetest_Project_WebAPI.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Minetest_Project_WebAPI.IntegrationTests
{
    public class TeacherControllerInTests : TestClientProvider
    {
        string api = "/api/teacher";
        [Fact]
        public async Task PostTeacher_WithStudentCreate_ReturnOk()
        {
            // Arrange
            Teacher requestContent = new Teacher { TeacherName = "Teacher_99" };
            // Act
            var httpResponse = await _client.PostAsync(api, ContentHelper.GetStringContent(requestContent));
            httpResponse.EnsureSuccessStatusCode();
            var response = httpResponse.Content.ReadAsStringAsync();
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task PostTeacher_WithOutStudentCreate_ReturnBadRequest()
        {
            // Arrange
            string requestContent = "";
            // Act
            var httpResponse = await _client.PostAsync(api, ContentHelper.GetStringContent(requestContent));
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteTeacher_WithExistingStudent_ReturnNoContent()
        {
            // Arrange
            int requestContent = 16;
            // Act
            var httpResponse = await _client.DeleteAsync(string.Format("{0}/{1}", api, requestContent));
            httpResponse.EnsureSuccessStatusCode();
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteTeacher_WithOutExistingStudent_ReturnNotFound()
        {
            // Arrange
            int requestContent = 99;
            // Act
            var httpResponse = await _client.DeleteAsync(string.Format("{0}/{1}", api, requestContent));
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
