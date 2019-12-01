using Geolocation.Domain.Domain;
using Geolocation.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Repositories
{
    public sealed class RootObjectRepository : IRepository<RootObject>
    {
        private readonly ILogger<RootObjectRepository> _logger;
        private readonly GeolocationDbContext _context;

        public RootObjectRepository(ILogger<RootObjectRepository> logger, GeolocationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task Add(RootObject item)
        {
            _logger.LogInformation("Adding new entry to database");
            try
            {
                await _context.AddAsync(item);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Added new entry to database with ID: {item.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError("Adding new entry to database with ID: {0} have failed | Exception: {1}", item.Id, ex.Message);
            }
        }

        public async Task<RootObject> GetById(Guid id)
        {
            _logger.LogInformation($"Retrieving entry from database with ID: {id}");

            return await _context.Geolocations.SingleOrDefaultAsync(g => g.Id == id);
        }

        public async Task<RootObject> GetByIP(string ip)
        {
            _logger.LogInformation("Retrieving entry from database based on IP parameter");

            return await _context.Geolocations.SingleOrDefaultAsync(g => g.ip == ip);
        }

        public async Task<RootObject> GetByURL(string URL)
        {
            _logger.LogInformation("Retrieving entry from database based on IP parameter");

            return await _context.Geolocations.SingleOrDefaultAsync(g => g.URLValue == URL);
        }

        public async Task RemoveByIP(string ip)
        {
            _logger.LogInformation($"Removing entry from database with IP: {ip}");
            var item = await GetByIP(ip);
            _context.Geolocations.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveByURL(string url)
        {
            _logger.LogInformation($"Removing entry from database with URL: {url}");
            var item = await GetByURL(url);
            _context.Geolocations.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(RootObject item)
        {
            _context.Geolocations.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
