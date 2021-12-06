using EventMessages.EventBusMessage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Controllers
{
    public class ProductController
    {
        ProductService prd = new ProductService();
        public ProductResponseMessage GetProductDetails(string Id)
        {
            
            var response = new ProductResponseMessage
            {
                Product = new Products()
            };
            var reductionRate = prd.GetReducionRate(((int)DateTime.Now.DayOfWeek + 6) % 7 + 1);
            var prdDetail = prd.Get(new MongoDB.Bson.ObjectId(Id));
            if (prdDetail != null)
            {
                response.Product = new Products
                {
                    Id = prdDetail.Id.ToString(),
                    ProductName = prdDetail.ProductName,
                    EntryDate = prdDetail.EntryDate,
                    PriceWithReduction = reductionRate != 0 ? prdDetail.Price * reductionRate : prdDetail.Price
                };
            }
            return response;
        }
        public ProductResponseMessage GetProduct()
        {
            var response = new ProductResponseMessage();
            response.ProductList = new List<Products>();
            var reductionRate = prd.GetReducionRate(((int)DateTime.Now.DayOfWeek + 6) % 7 + 1);
            var products = prd.Get();
            foreach (var i in products)
            {
                var p = new Products
                {
                    Id = i.Id.ToString(),
                    ProductName = i.ProductName,
                    EntryDate = i.EntryDate,
                    PriceWithReduction = reductionRate != 0 ? i.Price * reductionRate : i.Price

                };
                response.ProductList.Add(p);
            }
            return response;
        }
    }
}
