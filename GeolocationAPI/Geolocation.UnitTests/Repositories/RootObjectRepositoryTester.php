﻿using Geolocation.Domain.Domain;
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
                                .UseInMemoryDatabase(databaseName: "Add_RootObject_to_database")
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

        [Test]
        public async Task Delete_RootObject_from_database_by_IP()
        {
            var options = new DbContextOptionsBuilder<GeolocationDbContext>()
                                .UseInMemoryDatabase(databaseName: "Delete_RootObject_from_database_by_IP")
                                .Options;

            var ipAddress = "89.64.27.223";

            var mockLogger = Mock.Of<ILogger<RootObjectRepository>>();

            using (var context = new GeolocationDbContext(options))
            {
                var repository = new RootObjectRepository(mockLogger, context);

                await repository.Add(new RootObject { ip = ipAddress });
            }

            using (var context = new GeolocationDbContext(options))
            {
                Assert.AreEqual(1, context.Geolocations.Count());
                Assert.AreEqual(ipAddress, context.Geolocations.Single().ip);
            }

            using (var context = new GeolocationDbContext(options))
            {
                var repository = new RootObjectRepository(mockLogger, context);
                await repository.RemoveByIP(ipAddress);
                Assert.AreEqual(0, context.Geolocations.Count());
            }
        }

        [Test]
        public async Task Delete_RootObject_from_database_by_URL()
        {
            var options = new DbContextOptionsBuilder<GeolocationDbContext>()
                                .UseInMemoryDatabase(databaseName: "Delete_RootObject_from_database_by_IP")
                                .Options;

            var urlAddress = "www.onet.pl";

            var mockLogger = Mock.Of<ILogger<RootObjectRepository>>();

            using (var context = new GeolocationDbContext(options))
            {
                var repository = new RootObjectRepository(mockLogger, context);

                await repository.Add(new RootObject { URLValue = urlAddress });
            }

            using (var context = new GeolocationDbContext(options))
            {
                Assert.AreEqual(1, context.Geolocations.Count());
                Assert.AreEqual(urlAddress, context.Geolocations.Single().URLValue);
            }

            using (var context = new GeolocationDbContext(options))
            {
                var repository = new RootObjectRepository(mockLogger, context);
                await repository.RemoveByURL(urlAddress);
                Assert.AreEqual(0, context.Geolocations.Count());
            }
        }

        [Test]
        public async Task Get_RootObject_from_database_by_IP()
        {
            var options = new DbContextOptionsBuilder<GeolocationDbContext>()
                                .UseInMemoryDatabase(databaseName: "Get_RootObject_from_database_by_IP")
                                .Options;

            var ipAddress = "89.64.27.223";

            var mockLogger = Mock.Of<ILogger<RootObjectRepository>>();

            using (var context = new GeolocationDbContext(options))
            {
                var repository = new RootObjectRepository(mockLogger, context);

                await repository.Add(new RootObject { ip = ipAddress });
            }

            using (var context = new GeolocationDbContext(options))
            {
                Assert.AreEqual(1, context.Geolocations.Count());
                Assert.AreEqual(ipAddress, context.Geolocations.Single().ip);
            }
        }

        [Test]
        public async Task Get_RootObject_from_database_by_URL()
        {
            var options = new DbContextOptionsBuilder<GeolocationDbContext>()
                                .UseInMemoryDatabase(databaseName: "Get_RootObject_from_database_by_URL")
                                .Options;

            var URLAddress = "www.onet.pl";

            var mockLogger = Mock.Of<ILogger<RootObjectRepository>>();

            using (var context = new GeolocationDbContext(options))
            {
                var repository = new RootObjectRepository(mockLogger, context);

                await repository.Add(new RootObject { URLValue = URLAddress });
            }

            using (var context = new GeolocationDbContext(options))
            {
                Assert.AreEqual(1, context.Geolocations.Count());
                Assert.AreEqual(URLAddress, context.Geolocations.Single().URLValue);
            }
        }
    }
}
