using MongoDB.Bson;
using MongoDB.Driver;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DBExtention
{
    public class ContextSeeding
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }
        public static void SeedData(IMongoCollection<PriceReduction> priceCollection)
        {
            bool existPrice = priceCollection.Find(p => true).Any();
            if (!existPrice)
            {
                priceCollection.InsertManyAsync(GetPreconfiguredPriceReduction());
            }
        }
        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product(){
                    Id= ObjectId.GenerateNewId(),
                    ProductName = "Uncle Ben's Rice",
                    EntryDate =DateTime.Now,
                    Price =100
                },
                new Product(){
                    Id= ObjectId.GenerateNewId(),
                    ProductName = "A pile of potatoes",
                    EntryDate =DateTime.Now,
                    Price =50
                }
            };
        }

        private static IEnumerable<PriceReduction> GetPreconfiguredPriceReduction()
        {
            return new List<PriceReduction>()
            {
                new PriceReduction(){
                    DayOfWeek =1,
                    Reduction =0,
                },
                new PriceReduction(){
                    DayOfWeek =2,
                    Reduction =0,
                },
                new PriceReduction(){
                    DayOfWeek =3,
                    Reduction =0,
                },
                new PriceReduction(){
                    DayOfWeek =4,
                    Reduction =0,
                },
                new PriceReduction(){
                    DayOfWeek =5,
                    Reduction =0,
                },new PriceReduction(){
                    DayOfWeek =6,
                    Reduction =0.2,
                },
                new PriceReduction(){
                    DayOfWeek =7,
                    Reduction =0.5,
                }
            };
        }
    }
}
