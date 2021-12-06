using Microsoft.Extensions.Configuration;
using ProductAPI.Controllers;
using ProductAPI.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductAPI.Test
{
    public class ProductTest
    {
        public readonly ProductService productService;
        public ProductTest()
        {
            var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();
            productService = new ProductService();
        }
        [Fact]
        public async Task Case1()
        {

            ProductController product = new ProductController(productService);
            Assert.NotNull(await product.Get());
        }
        [Fact]
        public async Task Case2()
        {
            ProductController product = new ProductController(productService);
            Assert.NotNull(await product.Get("61aa0ca79601acfcb2f311e0"));
        }
        [Fact]
        public async Task Case3()
        {
            ProductController product = new ProductController(productService);
            var result = await product.Get("61aade91ca9b53e0df441abb");
            var id = result.Value.Product.Id;
            Assert.Equal("61aade91ca9b53e0df441abb", id);
        }
        [Fact]
        public async Task Case4()
        {
            ProductController product = new ProductController(productService);
            var result = await product.Get("123456");
            Assert.Null(result);
        }
    }
}
