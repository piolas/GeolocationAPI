using Geolocation.Infrastructure.DTO;
using Microsoft.Extensions.Configuration;
using Polly;
using RestSharp;
using System;
using System.Net.Http;
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
            var maxRetryAttempts = 3;
            var pauseBetweenFailures = TimeSpan.FromSeconds(2);

            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(maxRetryAttempts, i => pauseBetweenFailures);


            var apikey = GetIPStackAPIKey();

            var client = new RestClient("http://api.ipstack.com");

            var request = new RestRequest($"{paramter}?access_key={apikey}");

            IRestResponse<GeolocationResponseDTO> response = null;

            await retryPolicy.ExecuteAsync(async () =>
            {
                response = await client.ExecuteTaskAsync<GeolocationResponseDTO>(request);                
            });

            return response.Data;
        }
    }
}
