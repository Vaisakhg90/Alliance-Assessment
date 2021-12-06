using EventMessages.EventBusMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Interface
{
    public interface IProductService
    {
        Task<ProductResponseMessage> Get();
        Task<ProductResponseMessage> Get(string id);
    }
}
