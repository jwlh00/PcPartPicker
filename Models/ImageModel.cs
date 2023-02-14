using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace PcPartPicker.Models
{
    public class ImageModel
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public byte[] Content { get; set; }
        [BsonElement("filename")]
        public string filename { get; set; }

        [BsonElement("length")]
        public int Length { get; set; }

        [BsonElement("chunkSize")]
        public int ChunkSize { get; set; }

        [BsonElement("uploadDate")]
        public DateTime UploadDate { get; set; }

        [BsonElement("md5")]
        public string Md5 { get; set; }

        public string Url { get; set; }
    }
}
