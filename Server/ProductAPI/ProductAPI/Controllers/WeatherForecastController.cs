using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventMessages.EventBusMessage;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly static IBus bus = RabbitHutch.CreateBus(
            connectionString: "host=localhost;timeout=60;virtualHost=/;username=guest;password=guest",
            registerServices: s =>
            {
                s.Register<ITypeNameSerializer, EventBusTypeNameSerializer>();
                s.Register<IConventions, EventBusConventions>();
            });
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("Test")]
        public IActionResult Test()
        {
             
            var task1 = bus.Rpc.RequestAsync<ProductRequestMessage, ProductResponseMessage>(new ProductRequestMessage
            {
                productRequest = "Product Request"
            });
            //var task2 = bus.Rpc.RequestAsync<PaymentValidityCheckRequestMessage, PaymentValidityCheckResponseMessage>(new PaymentValidityCheckRequestMessage
            //{
            //    Amount = -500,
            //    CustomerId = 1,
            //    PaymentType = "CREDIT"
            //});
            Task.WaitAll(task1); // wait for all tasks to complete

            Console.WriteLine("Received response: " + task1.Result);
            //Console.WriteLine("Received response: " + task2.Result.Message);
            return Ok(task1.Result);
        }
    }
}
