using MongoDB.Bson;
using MongoDB.Driver;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver.Linq;
using Service.DBExtention;

namespace Service
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _product;
        private readonly IMongoCollection<PriceReduction> _priceReductions;
        
        private IMongoCollection<Product> Products { get; }
        private IMongoCollection<PriceReduction> PriceReductions { get; }
        public ProductService()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("ProductDB");
            database.CreateCollecitonifNotExist();
            _product = database.GetCollection<Product>("Product");
            _priceReductions = database.GetCollection<PriceReduction>("PriceReduction");
            ContextSeeding.SeedData(_product);
            ContextSeeding.SeedData(_priceReductions);
        }
        public List<Product> Get()
        {
            List<Product> products;
            products = _product.Find(p => true).ToList();
            return products;
        }

        public Product Get(ObjectId id) =>
            _product.Find<Product>(p => p.Id == id).FirstOrDefault();

        public double GetReducionRate(int DayOfWeek)
        {
            return _priceReductions.AsQueryable<PriceReduction>().Where(x => x.DayOfWeek == DayOfWeek).Select(x => x.Reduction).FirstOrDefault();
        }
    }
}
