using Geolocation.Domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace Geolocation.Infrastructure.Persistence
{
    public sealed class GeolocationDbContext : DbContext
    {
        public GeolocationDbContext(DbContextOptions<GeolocationDbContext> options) : base(options)
        {
        }

        public DbSet<RootObject> Geolocations { get; set; }
    }
}
