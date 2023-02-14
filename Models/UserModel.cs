using MongoDB.Bson.Serialization.Attributes;

namespace PcPartPicker.Models
{
    public class UserModel
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        public string UserName { get; set; } = "";
        public int Age { get; set; }
        public string Gender { get; set; } = "";
        public bool IsUserActive { get;set; }
    }
}
