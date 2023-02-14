using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using PcPartPicker.IService;
using PcPartPicker.Models;
using Syncfusion.Blazor.Data;

namespace PcPartPicker.Service
{
    public class ImageService : IImageService
    {
        private MongoClient? _mongoClient = null;
        private IMongoDatabase? _database = null;
        private IMongoCollection<ImageModel> _table;
        internal IGridFSBucket bucket;

        public ImageService()
        {
            _mongoClient = new MongoClient("mongodb+srv://admin_3:admin_3@proyecto1.b0ylzal.mongodb.net/?retryWrites=true&w=majority");
            _database = _mongoClient.GetDatabase("PcPartPicker");
            _table = _database.GetCollection<ImageModel>("fs.files");
            bucket = new GridFSBucket(_database);
        }

        public async Task<List<ImageModel>> GetAllImages()
        {
            var images = await _table.Find(new BsonDocument()).ToListAsync();
            var format = "image/png";
            foreach (var image in images)
            {
                image.Content = await GetImageContent(image.Id);
                image.Url = $"data:{format};base64,{Convert.ToBase64String(image.Content)}";
            }
            return images;
        }

        public async Task<bool> SaveImage(ImageModel image)
        {
            await bucket.UploadFromBytesAsync(image.filename, image.Content);

            return true;
        }

        public async Task<byte[]> GetImageContent(ObjectId id)
        {
            return await bucket.DownloadAsBytesAsync(id);
        }

        public async Task DeleteImage(ObjectId id)
        {
            await bucket.DeleteAsync(id);
        }
    }
}
