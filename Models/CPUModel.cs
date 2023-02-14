using MongoDB.Bson.Serialization.Attributes;

namespace PcPartPicker.Models
{
    public class CPUModel
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Brand { get; set; } = "";
        public int CoreCount { get; set; }
        public decimal PerformanceCoreClock { get; set; }
        public int TDP {get; set; }
    }
}
