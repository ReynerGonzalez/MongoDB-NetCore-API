using MongoDB_NetCore_API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB_NetCore_API.Services
{
    public interface IProductService
    {
        Task<List<Products>> GetAllAsync();
        Task<Products> GetByIdAsync(string id);
        Task<Products> CreateAsync(Products product);
        Task UpdateAsync(string id, Products product);
        Task DeleteAsync(string id);
    }
}
