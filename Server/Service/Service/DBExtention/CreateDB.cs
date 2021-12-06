using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DBExtention
{
    public static class CreateDB
    {
        public static void CreateCollecitonifNotExist(this IMongoDatabase database)
        {
            try
            {
                database.CreateCollection("Product");
                database.CreateCollection("PriceReduction");
            }
            catch (Exception)
            {
                
            }
        }
    }
}
