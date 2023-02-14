using PcPartPicker.Models;

namespace PcPartPicker.IService
{
    public interface IUserService
    {
        Task<UserModel> GetUser(string UserId);
        Task<IEnumerable<UserModel>> GetUsers();
        Task UpdateUser(UserModel User);
        Task CreateUser(UserModel User);
        Task DeleteUser(string UserId);
        Task<IEnumerable<UserModel>> GetActiveUsers();



        void BulkDeleteNotActive();
        void BulkChangeActivity(bool activity);
        void BulkAdd();

    }
}
