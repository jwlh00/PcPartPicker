using PcPartPicker.Models;

namespace PcPartPicker.IService
{
    public interface ICPUService
    {
        Task<CPUModel> GetCPU(string CPUId);
        Task<IEnumerable<CPUModel>> GetCPUs();
        Task UpdateCPU(CPUModel CPU);
        Task CreateCPU(CPUModel CPU);
        Task DeleteCPU(string CPUId);
        Task<IEnumerable<CPUModel>> GetCpusByValue();
    }
}
