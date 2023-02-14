using MongoDB.Bson;
using MongoDB.Driver;
using PcPartPicker.IService;
using PcPartPicker.Models;

namespace PcPartPicker.Service
{
    public class CPUService : ICPUService
    {
        private MongoClient? _mongoClient = null;
        private IMongoDatabase? _database = null;
        private IMongoCollection<CPUModel> _table;

        public CPUService()
        {
            _mongoClient = new MongoClient("mongodb+srv://admin_3:admin_3@proyecto1.b0ylzal.mongodb.net/?retryWrites=true&w=majority");
            _database = _mongoClient.GetDatabase("PcPartPicker");
            _table = _database.GetCollection<CPUModel>("CPU");
        }

        public async Task DeleteCPU(string CPUId)
        {
            await _table.DeleteOneAsync(x => x.Id == CPUId);
        }

        public async Task<CPUModel> GetCPU(string CPUId)
        {
            return await Task.Run(() => _table.Find(x => x.Id == CPUId).FirstOrDefault());
        }

        public async Task<IEnumerable<CPUModel>> GetCPUs()
        {
            return await Task.FromResult(_table.Find(FilterDefinition<CPUModel>.Empty).ToList());
        }

        public async Task UpdateCPU(CPUModel CPU)
        {
            var Obj = await _table.Find(x => x.Id == CPU.Id).FirstOrDefaultAsync();
            if (Obj != null) await _table.ReplaceOneAsync(x => x.Id == CPU.Id, CPU);
        }

        public async Task CreateCPU(CPUModel CPU)
        {
            var Obj = await _table.Find(x => x.Id == CPU.Id).FirstOrDefaultAsync();
            if (Obj == null) await _table.InsertOneAsync(CPU);
        }


        public async Task<IEnumerable<CPUModel>> GetCpusByValue()
        {
            var sortDefinition = Builders<CPUModel>.Sort.Descending(x => x.Price);
            var result = await _table.Find(new BsonDocument()).Sort(sortDefinition).ToListAsync();
            return result;
        }
    }
}
