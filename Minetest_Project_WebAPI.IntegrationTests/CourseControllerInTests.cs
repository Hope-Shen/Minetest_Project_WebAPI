using FluentAssertions;
using Minetest_Project_WebAPI.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Minetest_Project_WebAPI.IntegrationTests
{
    public class CourseControllerInTests : TestClientProvider
    {
        string api = "/api/course";
        [Fact]
        public async Task PostCourse_WithCourseCreate_ReturnOk()
        {
            // Arrange
            Course requestContent = new Course { CourseId = "INST0001", CourseName = "Course_INST0001", TeacherId = 1 };
            // Act
            var httpResponse = await _client.PostAsync(api, ContentHelper.GetStringContent(requestContent));
            httpResponse.EnsureSuccessStatusCode();
            var response = httpResponse.Content.ReadAsStringAsync();
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task PostCourse_WithOutCourseCreate_ReturnBadRequest()
        {
            // Arrange
            string requestContent = "";
            // Act
            var httpResponse = await _client.PostAsync(api, ContentHelper.GetStringContent(requestContent));
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteCourse_WithExistingCourse_ReturnNoContent()
        {
            // Arrange
            string requestContent = "INST0001";
            // Act
            var httpResponse = await _client.DeleteAsync(string.Format("{0}/{1}", api, requestContent));
            httpResponse.EnsureSuccessStatusCode();
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteCourse_WithOutExistingCourse_ReturnNotFound()
        {
            // Arrange
            string requestContent = "INST0099";
            // Act
            var httpResponse = await _client.DeleteAsync(string.Format("{0}/{1}", api, requestContent));
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
