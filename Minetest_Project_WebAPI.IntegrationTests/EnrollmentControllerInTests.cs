using FluentAssertions;
using Minetest_Project_WebAPI.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Minetest_Project_WebAPI.IntegrationTests
{
    public class EnrollmentControllerInTests : TestClientProvider
    {
        string api = "/api/enrollment";

        [Fact]
        public async Task PostEnrollment_WithEnrollmentCreate_ReturnOk()
        {
            // Arrange
            Enrollment requestContent = new Enrollment { CourseId = "COMP0001", StudentId = 1 };
            // Act
            var httpResponse = await _client.PostAsync(api, ContentHelper.GetStringContent(requestContent));
            httpResponse.EnsureSuccessStatusCode();
            var response = httpResponse.Content.ReadAsStringAsync();
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task PostEnrollment_WithOutEnrollmentCreate_ReturnBadRequest()
        {
            // Arrange
            string requestContent = "";
            // Act
            var httpResponse = await _client.PostAsync(api, ContentHelper.GetStringContent(requestContent));
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteEnrollment_WithExistingEnrollment_ReturnNoContent()
        {
            // Arrange
            int requestContent = 28;
            // Act
            var httpResponse = await _client.DeleteAsync(string.Format("{0}/{1}", api, requestContent));
            httpResponse.EnsureSuccessStatusCode();
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteEnrollment_WithOutExistingEnrollment_ReturnNotFound()
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
