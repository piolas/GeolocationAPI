using FluentAssertions;
using Geolocation.Infrastructure.DTO;
using GeolocationAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Geolocation.Integration.IPController
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
        public async Task AddGeolocationDataByIP_WithIPParameter_ReturnsOkObjectResult()
        {
            var ip = "89.64.27.223";

            var ipDataDTO = new IPDataDTO {IPParameter = ip };

            var contents = new StringContent(JsonConvert.SerializeObject(ipDataDTO), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"/IP/AddGeolocationDataByIP", contents);

            response.EnsureSuccessStatusCode();

            var responseResult = await response.Content.ReadAsStringAsync();

            responseResult.Should().Contain("Adding new entry based on paramter was successfull");
        }

        [Fact]
        public async Task DeleteGeolocationDataByIP_WithIPParameter_ReturnsOkObjectResult()
        {
            var ip = "89.64.27.223";

            var ipDataDTO = new IPDataDTO { IPParameter = ip };

            var contents = new StringContent(JsonConvert.SerializeObject(ipDataDTO), Encoding.UTF8, "application/json");

            await _client.PostAsync($"/IP/AddGeolocationDataByIP", contents);

            HttpRequestMessage request = new HttpRequestMessage
            {
                Content = contents,
                Method = HttpMethod.Delete,
                RequestUri = new Uri("/IP/DeleteGeolocationDataByIP", UriKind.Relative)
            };

            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var responseResult = await response.Content.ReadAsStringAsync();

            responseResult.Should().Contain("Entry found in DB. Deletion completed");
        }
    }
}
