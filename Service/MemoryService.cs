using MongoDB.Bson;
using MongoDB.Driver;
using PcPartPicker.IService;
using PcPartPicker.Models;

namespace PcPartPicker.Service
{
    public class MemoryService : IMemoryService
    {
        private MongoClient? _mongoClient = null;
        private IMongoDatabase? _database = null;
        private IMongoCollection<MemoryModel> _table;

        public MemoryService()
        {
            _mongoClient = new MongoClient("mongodb+srv://admin_3:admin_3@proyecto1.b0ylzal.mongodb.net/?retryWrites=true&w=majority");
            _database = _mongoClient.GetDatabase("PcPartPicker");
            _table = _database.GetCollection<MemoryModel>("Memory");
        }

        public async Task DeleteMemory(string memoryId)
        {
            await _table.DeleteOneAsync(x => x.Id == memoryId);
        }

        public async Task<MemoryModel> GetMemory(string memoryId)
        {
            return await Task.Run(() => _table.Find(x => x.Id == memoryId).FirstOrDefault());
        }

        public async Task<IEnumerable<MemoryModel>> GetMemories()
        {
            return await Task.FromResult(_table.Find(FilterDefinition<MemoryModel>.Empty).ToList());
        }

        public async Task UpdateMemory(MemoryModel Memory)
        {
            var Obj = await _table.Find(x => x.Id == Memory.Id).FirstOrDefaultAsync();
            if (Obj != null) await _table.ReplaceOneAsync(x => x.Id == Memory.Id, Memory);
        }

        public async Task CreateMemory(MemoryModel Memory)
        {
            var Obj = await _table.Find(x => x.Id == Memory.Id).FirstOrDefaultAsync();
            if (Obj == null) await _table.InsertOneAsync(Memory);
        }

        public async Task<IEnumerable<MemoryModel>> GetMemoriesByValue()
        {
            var sortDefinition = Builders<MemoryModel>.Sort.Descending(x => x.Price);
            var result = await _table.Find(new BsonDocument()).Sort(sortDefinition).ToListAsync();
            return result;
        }
    }
}
