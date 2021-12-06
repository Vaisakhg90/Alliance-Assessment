using EasyNetQ;
using EventMessages;
using EventMessages.EventBusMessage;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly static IBus bus = RabbitHutch.CreateBus(
            connectionString: "host=localhost;timeout=60;virtualHost=/;username=guest;password=guest",
            registerServices: s =>
            {
                s.Register<ITypeNameSerializer, EventBusTypeNameSerializer>();
                s.Register<IConventions, EventBusConventions>();
            });
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<ActionResult<ProductResponseMessage>> Get()
        {
            return await _productService.Get();                        
        }

        [HttpGet("GetProductDetail")]
        public async Task<ActionResult<ProductResponseMessage>> Get(string id)
        {
            return await _productService.Get(id);
        }
    }
}
