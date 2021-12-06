using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventMessages
{
    public sealed class EventBusConventions : Conventions
    {
        public EventBusConventions(ITypeNameSerializer typeNameSerializer) : base(typeNameSerializer)
        {
            ExchangeNamingConvention = type =>
            {
                QueueAttribute MyAttribute = (QueueAttribute)Attribute.GetCustomAttribute(type, typeof(QueueAttribute));
                return MyAttribute.ExchangeName;
            };
            RpcRoutingKeyNamingConvention = type =>
            {
                QueueAttribute MyAttribute = (QueueAttribute)Attribute.GetCustomAttribute(type, typeof(QueueAttribute));
                return MyAttribute.QueueName;
            };
            //ErrorQueueNamingConvention = info => "ErrorQueue";
            //ErrorExchangeNamingConvention = info => "BusErrorExchange_" + info.RoutingKey + assemblyName;
        }
    }
}
