using EasyNetQ;
using Newtonsoft.Json;
using EventMessages.EventBusMessage;
using System;
using System.Threading.Tasks;
using EventMessages;
using Service.Controllers;

namespace Service
{
    class Program
    {
        private readonly static IBus bus = RabbitHutch.CreateBus(
            connectionString: "host=localhost;timeout=60;virtualHost=/;username=guest;password=guest",
            registerServices: s =>
            {
                s.Register<ITypeNameSerializer, EventBusTypeNameSerializer>();
                s.Register<IConventions, EventBusConventions>();
            });

        static async Task Main(string[] args)
        {
            ProductController prd = new ProductController();
            try
            {
                await bus.Rpc.RespondAsync<ProductRequestMessage, ProductResponseMessage>(request =>
                {
                    
                    Console.WriteLine($"Received request: {JsonConvert.SerializeObject(request)}");
                    
                    if(request.productRequest == "Product Request")
                    {
                        return prd.GetProduct();
                    }
                    else if(request.productRequest == "Product Detail Request")
                    {
                        return prd.GetProductDetails(request.Id);
                    }
                    else
                    {
                        return null;
                    }
            
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine(); // to keep running the console app
        }
    }
}

