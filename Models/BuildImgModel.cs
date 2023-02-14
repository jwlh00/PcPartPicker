using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson.Serialization.Attributes;

namespace PcPartPicker.Models
{
    public class BuildImageModel
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
        public string? Name { get; set; }
        public string? UsernameId { get; set; }
        public string? ImgName { get; set; }
        public bool IsActive { get; set; }
        public CPUModel? CPU { get; set; }
        public CPUCoolerModel? CPUCooler { get; set; }
        public CaseModel? Case { get; set; }
        public List<GPUModel>? GPU { get; set; }
        public List<MemoryModel>? Memory { get; set; }
        public MotherboardModel? Motherboard { get; set; }
        public PSUModel? PSU { get; set; }
        public List<StorageModel>? Storage { get; set; }

        public ImageModel Image { get; set; }

        

    }
}
