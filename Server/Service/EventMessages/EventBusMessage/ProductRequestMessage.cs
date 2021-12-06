using System;
using System.Collections.Generic;
using System.Text;
using EasyNetQ;

namespace EventMessages.EventBusMessage
{
    [Queue(queueName: "payment.validity.check.queue")]
    public sealed class ProductRequestMessage
    {
        public string productRequest;
        public string Id;
    }
}
