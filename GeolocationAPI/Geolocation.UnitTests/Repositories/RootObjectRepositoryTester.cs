using Geolocation.Domain.Domain;
using Geolocation.Infrastructure.Persistence;
using Geolocation.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Geolocation.UnitTests.Repositories
{
    [TestFixture]
    public class RootObjectRepositoryTester
    {
        [Test]
        public async Task Add_RootObject_to_database()
        {
            var options = new DbContextOptionsBuilder<GeolocationDbContext>()
                                .UseInMemoryDatabase(databaseName: "GeolocationApi")
                                .Options;

            var mockLogger = Mock.Of<ILogger<RootObjectRepository>>();

            var id = Guid.NewGuid();

            using (var context = new GeolocationDbContext(options))
            {
                var repository = new RootObjectRepository(mockLogger, context);

                await repository.Add(new RootObject {Id = id });
            }

            using (var context = new GeolocationDbContext(options))
            {
                Assert.AreEqual(1, context.Geolocations.Count());
                Assert.AreEqual(id, context.Geolocations.Single().Id);
            }
        }
    }
}
