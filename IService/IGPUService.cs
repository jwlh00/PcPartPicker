using PcPartPicker.Models;

namespace PcPartPicker.IService
{
    public interface IGPUService
    {
        Task<GPUModel> GetGPU(string GPUId);
        Task<IEnumerable<GPUModel>> GetGPUs();
        Task UpdateGPU(GPUModel GPU);
        Task CreateGPU(GPUModel GPU);
        Task DeleteGPU(string GPUId);
        Task<IEnumerable<GPUModel>> GetGpusByValue();
    }
}
