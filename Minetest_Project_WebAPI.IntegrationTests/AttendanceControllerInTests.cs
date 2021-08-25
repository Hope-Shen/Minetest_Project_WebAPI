using FluentAssertions;
using Minetest_Project_WebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Minetest_Project_WebAPI.IntegrationTests
{
    public class AttendanceControllerInTests : TestClientProvider
    {
        string api = "/api/attendance";

        [Fact]
        public async Task PostAttendance_WithAttendanceCreate_ReturnOk()
        {
            // Arrange
            Attendance requestContent = new Attendance { CourseId = "COMP0001", StudentId = 1, Date = DateTime.Today };
            // Act
            var httpResponse = await _client.PostAsync(api, ContentHelper.GetStringContent(requestContent));
            // Act
            httpResponse.EnsureSuccessStatusCode();
            var response = httpResponse.Content.ReadAsStringAsync();
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task PostAttendance_WithOutAttendanceCreate_ReturnBadRequest()
        {
            // Arrange
            string requestContent = "";
            // Act
            var httpResponse = await _client.PostAsync(api, ContentHelper.GetStringContent(requestContent));
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task DeleteAttendance_WithExistingAttendance_ReturnNoContent()
        {
            // Arrange
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("https://localhost:44357/api/attendance"),
                Content = new StringContent(JsonConvert.SerializeObject(new Attendance { CourseId = "COMP0001", StudentId = 1, Date = DateTime.Today }), Encoding.UTF8, "application/json")
            };
            // Act
            var httpResponse = await _client.SendAsync(request);
            httpResponse.EnsureSuccessStatusCode();
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task DeleteAttendance_WithOutExistingAttendance_ReturnNotFound()
        {
            // Arrange
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("https://localhost:44357/api/attendance"),
                Content = new StringContent(JsonConvert.SerializeObject(new Attendance { CourseId = "INST0099", StudentId = 1, Date = DateTime.Today }), Encoding.UTF8, "application/json")
            };
            // Act
            var httpResponse = await _client.SendAsync(request);
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
