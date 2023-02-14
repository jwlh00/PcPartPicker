using PcPartPicker.Models;

namespace PcPartPicker.IService
{
    public interface IMemoryService
    {
        Task<MemoryModel> GetMemory(string memoryId);
        Task<IEnumerable<MemoryModel>> GetMemories();
        Task UpdateMemory(MemoryModel Memory);
        Task CreateMemory(MemoryModel Memory);
        Task DeleteMemory(string memoryId);
        Task<IEnumerable<MemoryModel>> GetMemoriesByValue();
    }
}
