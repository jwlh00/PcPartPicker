using MongoDB.Driver;
using PcPartPicker.IService;
using PcPartPicker.Models;
using PcPartPicker.Pages.PcPartPickerPages;

namespace PcPartPicker.Service
{
    public class UserService : IUserService
    {
        private MongoClient? _mongoClient = null;
        private IMongoDatabase? _database = null;
        private IMongoCollection<UserModel> _table;
            
        public UserService()
        {
            _mongoClient = new MongoClient("mongodb+srv://admin_3:admin_3@proyecto1.b0ylzal.mongodb.net/?retryWrites=true&w=majority");
            _database = _mongoClient.GetDatabase("PcPartPicker");
            _table = _database.GetCollection<UserModel>("User");

        }
        public async Task DeleteUser(string UserId)
        {
            await _table.DeleteOneAsync(x => x.Id == UserId);
        }

        public async Task<UserModel> GetUser(string UserId)
        {
            return await Task.Run(()=> _table.Find(x => x.Id == UserId).FirstOrDefault());
        }

        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            return await Task.FromResult(_table.Find(FilterDefinition<UserModel>.Empty).ToList());
        }

        public async Task UpdateUser(UserModel User)
        {
            var Obj = await _table.Find(x => x.Id == User.Id).FirstOrDefaultAsync();
            if (Obj != null) await _table.ReplaceOneAsync(x => x.Id == User.Id, User);
        }

        public async Task CreateUser(UserModel User)
        {
            var Obj = await _table.Find(x => x.Id == User.Id).FirstOrDefaultAsync();
            if (Obj == null) await _table.InsertOneAsync(User);
        }

        //TODO: Simple Aggregation + Limit here
        public async Task<IEnumerable<UserModel>> GetActiveUsers()
        {
            var pipeline = new EmptyPipelineDefinition<UserModel>()
                .Match(x => x.IsUserActive == true)
                .Limit(5);

            var result = await _table.Aggregate<UserModel>(pipeline).ToListAsync();

            return result;
        }

        public async void BulkAdd()
        {
            Random random = new Random();
            IEnumerable<UserModel> users = Enumerable.Range(1, 10)
                .Select(i => new UserModel
                {
                    UserName = $"User{i}",
                    Gender = i % 2 == 0 ? "Male" : "Female",
                    Age = random.Next(18, 60),
                    IsUserActive = i % 2 == 0 ? true : false
                });
            await _table.InsertManyAsync(users);
        }

        public async void BulkChangeActivity(bool activity)
        {
            var users = _table.Find(FilterDefinition<UserModel>.Empty).ToList();
            var filter = Builders<UserModel>.Filter.In(x => x.Id, users.Select(x => x.Id));
            var update = Builders<UserModel>.Update.Set(x => x.IsUserActive, activity);
            _table.UpdateMany(filter, update);
        }

        public void BulkDeleteNotActive()
        {
            _table.DeleteMany(x => x.IsUserActive == false);
        }
    }
}
