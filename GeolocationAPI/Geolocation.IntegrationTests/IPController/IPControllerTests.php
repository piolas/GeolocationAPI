using FluentAssertions;
using GeolocationAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Geolocation.IntegrationTests.IPController
{
    public class IPControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public IPControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetGeolocationDataByIP_WithIPParameter_ReturnsOkObjectResult()
        {
            var ip = "89.64.27.223";

            var response = await _client.GetAsync($"/IP/GetGeolocationDataByIP/{ip}");

            response.EnsureSuccessStatusCode();

            var responseResult = await response.Content.ReadAsStringAsync();

            responseResult.Should().Contain(ip);
        }

        [Fact]
        public async Task GetGeolocationDataByIP_WithURLParameter_ReturnsOkObjectResult()
        {
            var url = "www.onet.pl";

            var response = await _client.GetAsync($"/IP/GetGeolocationDataByIP/{url}");

            response.EnsureSuccessStatusCode();

            var responseResult = await response.Content.ReadAsStringAsync();

            responseResult.Should().Contain(url);
        }
    }
}
