using Geolocation.Infrastructure.Persistence;
using GeolocationAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;

namespace Geolocation.IntegrationTests
{
    public class IntegrationTest
    {
        protected readonly HttpClient TestClient;

        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();
                //.WithWebHostBuilder(builder =>
                //{
                //    builder.ConfigureServices(services =>
                //    {                        
                //        services.AddDbContext<GeolocationDbContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                //    });
                //}); ;
            TestClient = appFactory.CreateClient();
        }
    }
}