using PcPartPicker.Models;

namespace PcPartPicker.IService
{
    public interface IStorageService
    {
        Task<StorageModel> GetStorage(string storageId);
        Task<IEnumerable<StorageModel>> GetStorages();
        Task UpdateStorage(StorageModel Storage);
        Task CreateStorage(StorageModel Storage);
        Task DeleteStorage(string storageId);
        Task<IEnumerable<StorageModel>> GetStoragesByValue();
    }
}
