using PcPartPicker.Models;

namespace PcPartPicker.IService
{
    public interface ICPUCoolerService
    {
        Task<CPUCoolerModel> GetCPUCooler(string CPUcoolerId);
        Task<IEnumerable<CPUCoolerModel>> GetCPUCoolers();
        Task UpdateCPUCooler(CPUCoolerModel CPUCooler);
        Task CreateCPUCooler(CPUCoolerModel CPUCooler);
        Task DeleteCPUCooler(string CPUcoolerId);
        Task<IEnumerable<CPUCoolerModel>> GetCpuCoolersByValue();
    }
}
