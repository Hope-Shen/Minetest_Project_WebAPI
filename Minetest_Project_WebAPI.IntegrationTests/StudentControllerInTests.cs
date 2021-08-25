using FluentAssertions;
using Minetest_Project_WebAPI.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Minetest_Project_WebAPI.IntegrationTests
{
    public class StudentControllerInTests : TestClientProvider
    {
        [Fact]
        public async Task PostStudent_WithStudentCreate_ReturnOk()
        {
            // Arrange
            Student requestContent = new Student { StudentName = "studnet_99" };
            // Act
            var httpResponse = await _client.PostAsync("/api/student", ContentHelper.GetStringContent(requestContent));
            httpResponse.EnsureSuccessStatusCode();
            var response = httpResponse.Content.ReadAsStringAsync();
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task PostStudent_WithOutStudentCreate_ReturnBadRequest()
        {
            // Arrange
            string requestContent = "";
            // Act
            var httpResponse = await _client.PostAsync("/api/student", ContentHelper.GetStringContent(requestContent));
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteStudent_WithExistingStudent_ReturnNoContent()
        {
            // Arrange
            int requestContent = 28;
            // Act
            var httpResponse = await _client.DeleteAsync(string.Format("/api/student/{0}", requestContent));
            httpResponse.EnsureSuccessStatusCode();
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteStudent_WithOutExistingStudent_ReturnNotFound()
        {
            // Arrange
            int requestContent = 99;
            // Act
            var httpResponse = await _client.DeleteAsync(string.Format("/api/student/{0}", requestContent));
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
