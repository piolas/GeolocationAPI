using Geolocation.Infrastructure.DTO;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
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
            string vaultName = _configuration["IPStackAPIKey"];           
            return vaultName;
        }

        public async Task<GeolocationResponseDTO> GetDataFromRemoteAPI(string paramter)
        {
            var apikey = GetIPStackAPIKey();

            var client = new RestClient("http://api.ipstack.com");

            var request = new RestRequest($"{paramter}/?access_key={apikey}");

            var response = await client.ExecuteTaskAsync<GeolocationResponseDTO>(request);           

            return response.Data;
        }
    }
}
