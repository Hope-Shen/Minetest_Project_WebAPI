using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.Net;
using Newtonsoft.Json;

namespace Minetest_Project_WebAPI.IntegrationTests
{
    public class TestClientProvider
    {
        protected readonly HttpClient _client;

        public TestClientProvider()
        {
            var factory = new WebApplicationFactory<Startup>();
            _client = factory.CreateClient();
        }
        protected static class ContentHelper
        {
            public static StringContent GetStringContent(object obj)
                => new StringContent(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
        }

        [Theory]
        [InlineData("/api/student")]
        [InlineData("/api/teacher")]
        [InlineData("/api/course")]
        [InlineData("/api/enrollment")]
        [InlineData("/api/attendance")]
        public async Task GetAll_WithExistingItem_ReturnOK(string url)
        {
            // Arrange
            // Act
            var httpResponse = await _client.GetAsync(url);
            httpResponse.EnsureSuccessStatusCode();
            var response = httpResponse.Content.ReadAsStringAsync();
            // Assert
            httpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
