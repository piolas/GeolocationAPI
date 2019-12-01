using System;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetById(Guid id);
        Task<T> GetByIP(string ip);
        Task<T> GetByURL(string url);
        Task Add(T item);
        Task RemoveByIP(string ip);
        Task RemoveByURL(string url);
        Task Update(T item);
    }
}
