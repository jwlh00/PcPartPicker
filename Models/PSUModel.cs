using MongoDB.Bson.Serialization.Attributes;

namespace PcPartPicker.Models
{
    public class PSUModel
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Brand { get; set; } = "";
        public bool Modular { get; set; }
        public string Rating { get; set; } = "";
        public int Wattage { get; set; }
    }
}
