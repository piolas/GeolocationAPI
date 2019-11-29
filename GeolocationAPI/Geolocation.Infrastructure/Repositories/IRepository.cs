using System;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetById(Guid id);
        Task<T> GetByIP(string ip);
        Task Add(T item);
        Task Remove(string ip);
        Task Update(T item);
    }
}
