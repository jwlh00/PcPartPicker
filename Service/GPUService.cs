using MongoDB.Bson;
using MongoDB.Driver;
using PcPartPicker.IService;
using PcPartPicker.Models;

namespace PcPartPicker.Service
{
    public class GPUService : IGPUService
    {
        private MongoClient? _mongoClient = null;
        private IMongoDatabase? _database = null;
        private IMongoCollection<GPUModel> _table;

        public GPUService()
        {
            _mongoClient = new MongoClient("mongodb+srv://admin_3:admin_3@proyecto1.b0ylzal.mongodb.net/?retryWrites=true&w=majority");
            _database = _mongoClient.GetDatabase("PcPartPicker");
            _table = _database.GetCollection<GPUModel>("GPU");
        }

        public async Task DeleteGPU(string GPUId)
        {
            await _table.DeleteOneAsync(x => x.Id == GPUId);
        }

        public async Task<GPUModel> GetGPU(string GPUId)
        {
            return await Task.Run(() => _table.Find(x => x.Id == GPUId).FirstOrDefault());
        }

        public async Task<IEnumerable<GPUModel>> GetGPUs()
        {
            return await Task.FromResult(_table.Find(FilterDefinition<GPUModel>.Empty).ToList());
        }

        public async Task UpdateGPU(GPUModel GPU)
        {
            var Obj = await _table.Find(x => x.Id == GPU.Id).FirstOrDefaultAsync();
            if (Obj != null) await _table.ReplaceOneAsync(x => x.Id == GPU.Id, GPU);
        }

        public async Task CreateGPU(GPUModel GPU)
        {
            var Obj = await _table.Find(x => x.Id == GPU.Id).FirstOrDefaultAsync();
            if (Obj == null) await _table.InsertOneAsync(GPU);
        }

        public async Task<IEnumerable<GPUModel>> GetGpusByValue()
        {
            var sortDefinition = Builders<GPUModel>.Sort.Descending(x => x.Price);
            var result = await _table.Find(new BsonDocument()).Sort(sortDefinition).ToListAsync();
            return result;
        }
    }
}
