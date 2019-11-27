using Geolocation.Infrastructure.DTO;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Services
{
    public class IPStackService : IService
    {
        private readonly IConfiguration _configuration;

        public IPStackService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetIPStackAPIKey()
        {
            string key = _configuration["IPStackAPIKey"];           
            return key;
        }

        public async Task<GeolocationResponseDTO> GetDataFromRemoteAPI(string paramter)
        {
            var apikey = GetIPStackAPIKey();

            var client = new RestClient("http://api.ipstack.com");

            var request = new RestRequest($"{paramter}?access_key={apikey}");

            var response = await client.ExecuteTaskAsync<GeolocationResponseDTO>(request);           

            return response.Data;
        }
    }
}
