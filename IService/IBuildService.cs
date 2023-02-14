using PcPartPicker.Models;

namespace PcPartPicker.IService
{
    public interface IBuildService
    {
        Task<BuildModel> GetBuild(string buildId);
        Task<IEnumerable<BuildModel>> GetBuildAsEnum(string buildId);
        Task<IEnumerable<BuildModel>> GetBuilds();
        Task UpdateBuild(BuildModel Build);
        Task CreateBuild(BuildModel Build);
        Task DeleteBuild(string buildId);
        
        Task<BuildUserModel> GetBuildUser(string buildId);
        Task<IEnumerable<BuildImageModel>> GetBuildImage();
    }
}
