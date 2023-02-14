using MongoDB.Bson;
using MongoDB.Driver;
using PcPartPicker.IService;
using PcPartPicker.Models;

namespace PcPartPicker.Service
{
    public class StorageService : IStorageService
    {
        private MongoClient? _mongoClient = null;
        private IMongoDatabase? _database = null;
        private IMongoCollection<StorageModel> _table;

        public StorageService()
        {
            _mongoClient = new MongoClient("mongodb+srv://admin_3:admin_3@proyecto1.b0ylzal.mongodb.net/?retryWrites=true&w=majority");
            _database = _mongoClient.GetDatabase("PcPartPicker");
            _table = _database.GetCollection<StorageModel>("Storage");
        }

        public async Task DeleteStorage(string storageId)
        {
            await _table.DeleteOneAsync(x => x.Id == storageId);
        }

        public async Task<StorageModel> GetStorage(string storageId)
        {
            return await Task.Run(() => _table.Find(x => x.Id == storageId).FirstOrDefault());
        }

        public async Task<IEnumerable<StorageModel>> GetStorages()
        {
            return await Task.FromResult(_table.Find(FilterDefinition<StorageModel>.Empty).ToList());
        }

        public async Task UpdateStorage(StorageModel Storage)
        {
            var Obj = await _table.Find(x => x.Id == Storage.Id).FirstOrDefaultAsync();
            if (Obj != null) await _table.ReplaceOneAsync(x => x.Id == Storage.Id, Storage);
        }

        public async Task CreateStorage(StorageModel Storage)
        {
            var Obj = await _table.Find(x => x.Id == Storage.Id).FirstOrDefaultAsync();
            if (Obj == null) await _table.InsertOneAsync(Storage);
        }

        public async Task<IEnumerable<StorageModel>> GetStoragesByValue()
        {
            var sortDefinition = Builders<StorageModel>.Sort.Descending(x => x.Price);
            var result = await _table.Find(new BsonDocument()).Sort(sortDefinition).ToListAsync();
            return result;
        }
    }
}
