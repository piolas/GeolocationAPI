﻿using System;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        Task<T> GetById(Guid id);        
        Task Add(T item);
        Task Remove(T item);
        Task Update(T item);
    }
}