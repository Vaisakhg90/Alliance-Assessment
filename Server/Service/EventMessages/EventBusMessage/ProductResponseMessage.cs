using System;
using System.Collections.Generic;
using System.Text;
using EasyNetQ;

namespace EventMessages.EventBusMessage
{
    [Queue(queueName: "payment.validity.check.queue")]
    public sealed class ProductResponseMessage
    {
        public Products Product { get; set; }
        public List<Products> ProductList { get; set; }
        //public ProductsResponse();

    }
}
