using PcPartPicker.Models;

namespace PcPartPicker.IService
{
    public interface IMotherboardService
    {
        Task<MotherboardModel> GetMotherboard(string motherboardId);
        Task<IEnumerable<MotherboardModel>> GetMotherboards();
        Task UpdateMotherboard(MotherboardModel Motherboard);
        Task CreateMotherboard(MotherboardModel Motherboard);
        Task DeleteMotherboard(string motherboardId);
        Task<IEnumerable<MotherboardModel>> GetMotherboardsByValue();
    }
}
