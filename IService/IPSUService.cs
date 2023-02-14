using PcPartPicker.Models;

namespace PcPartPicker.IService
{
    public interface IPSUService
    {
        Task<PSUModel> GetPSU(string PSUId);
        Task<IEnumerable<PSUModel>> GetPSUs();
        Task UpdatePSU(PSUModel PSU);
        Task CreatePSU(PSUModel PSU);
        Task DeletePSU(string PSUId);
        Task<IEnumerable<PSUModel>> GetPsusByValue();
    }
}
