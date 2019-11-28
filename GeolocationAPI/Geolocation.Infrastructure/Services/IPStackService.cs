using Geolocation.Infrastructure.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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

        public async Task<GeolocationResponseDTO> GetDataFromRemoteAPI(string parameter)
        {
            var maxRetryAttempts = 3;
            var pauseBetweenFailures = TimeSpan.FromSeconds(2);

            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(maxRetryAttempts, i => pauseBetweenFailures);


            var apikey = GetIPStackAPIKey();

            //var client = new RestClient("http://api.ipstack.com");            

            //var request = new RestRequest("{parameter}?access_key={apikey}");

            //request.AddParameter("parameter", parameter, ParameterType.GetOrPost);
            //request.AddParameter("apikey", apikey);

            //IRestResponse<GeolocationResponseDTO> response = null;

            //$"http://api.ipstack.com/{parameter}?access_key={apikey}"

            GeolocationResponseDTO dto = null;

            var client = new HttpClient();
            {
                string product = null;
                var response = await client.GetAsync($"http://api.ipstack.com/{parameter}?access_key={apikey}");
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsStringAsync();
                    dto = JsonConvert.DeserializeObject<GeolocationResponseDTO>(product);
                }
                return dto;
            }

                //await retryPolicy.ExecuteAsync(async () =>
                //{
                //    response = await client.ExecuteTaskAsync<GeolocationResponseDTO>(request);
                //});

            //return response.Data;
        }
    }
}
