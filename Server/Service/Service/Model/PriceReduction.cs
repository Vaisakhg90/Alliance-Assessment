using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Model
{
    public class PriceReduction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _Id { get; set; }
        public int DayOfWeek { get; set; }
        public double Reduction { get; set; }
    }
}
