using MongoDB_NetCore_API.Configuration;
using MongoDB_NetCore_API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace MongoDB_NetCore_API.Services
{
    public class ProductService: IProductService
    {
        private readonly IMongoCollection<Products> _product;
        private readonly ProductsConfiguration _settings;
        public ProductService(IOptions<ProductsConfiguration> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            var database = client.GetDatabase(_settings.DatabaseName);
            _product = database.GetCollection<Products>(_settings.ProductsCollectionName);
        }
        public async Task<List<Products>> GetAllAsync()
        {
            return await _product.Find(c => true).ToListAsync();
        }
        public async Task<Products> GetByIdAsync(string id)
        {
            return await _product.Find<Products>(c => c.id == id).FirstOrDefaultAsync();
        }
        public async Task<Products> CreateAsync(Products product)
        {
            await _product.InsertOneAsync(product);
            return product;
        }
        public async Task UpdateAsync(string id, Products product)
        {
            await _product.ReplaceOneAsync(c => c.id == id, product);
        }
        public async Task DeleteAsync(string id)
        {
            await _product.DeleteOneAsync(c => c.id == id);
        }
    }
}
