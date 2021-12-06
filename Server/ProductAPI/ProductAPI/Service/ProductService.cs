using EasyNetQ;
using EventMessages;
using EventMessages.EventBusMessage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Service
{
    public class ProductService: IProductService
    {
        private readonly static IBus bus = RabbitHutch.CreateBus(
            connectionString: "host=localhost;timeout=60;virtualHost=/;username=guest;password=guest",
            registerServices: s =>
            {
                s.Register<ITypeNameSerializer, EventBusTypeNameSerializer>();
                s.Register<IConventions, EventBusConventions>();
            });      

        public async Task<ProductResponseMessage> Get()
        {
            var task = bus.Rpc.RequestAsync<ProductRequestMessage, ProductResponseMessage>(new ProductRequestMessage
            {
                productRequest = "Product Request"
            });
            await task; // wait for all tasks to complete
            var prod = task.Result;
            return prod;
        }

        public async Task<ProductResponseMessage> Get(string id)
        {
            var task = bus.Rpc.RequestAsync<ProductRequestMessage, ProductResponseMessage>(new ProductRequestMessage
            {
                productRequest = "Product Detail Request",
                Id = id
            });
            await task; // wait for all tasks to complete
            var prod = task.Result;
            return prod;
        }
            
    }
    
}
