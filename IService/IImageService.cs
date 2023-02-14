using MongoDB.Bson;
using PcPartPicker.Models;

namespace PcPartPicker.IService
{
    public interface IImageService
    {
        Task<bool> SaveImage(ImageModel image);
        Task<List<ImageModel>> GetAllImages();
        Task<byte[]> GetImageContent(ObjectId id);
        Task DeleteImage(ObjectId id);
    }
}
