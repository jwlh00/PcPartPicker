using MongoDB.Bson;
using MongoDB.Driver;
using PcPartPicker.IService;
using PcPartPicker.Models;


namespace PcPartPicker.Service
{
    public class CPUCoolerService : ICPUCoolerService
    {
        private MongoClient? _mongoClient = null;
        private IMongoDatabase? _database = null;
        private IMongoCollection<CPUCoolerModel> _table;

        public CPUCoolerService()
        {
            _mongoClient = new MongoClient("mongodb+srv://admin_3:admin_3@proyecto1.b0ylzal.mongodb.net/?retryWrites=true&w=majority");
            _database = _mongoClient.GetDatabase("PcPartPicker");
            _table = _database.GetCollection<CPUCoolerModel>("CPUCooler");
        }

        public async Task DeleteCPUCooler(string CPUcoolerId)
        {
            await _table.DeleteOneAsync(x => x.Id == CPUcoolerId);
        }

        public async Task<CPUCoolerModel> GetCPUCooler(string CPUcoolerId)
        {
            return await Task.Run(() => _table.Find(x => x.Id == CPUcoolerId).FirstOrDefault());
        }

        public async Task<IEnumerable<CPUCoolerModel>> GetCPUCoolers()
        {
            return await Task.FromResult(_table.Find(FilterDefinition<CPUCoolerModel>.Empty).ToList());
        }

        public async Task UpdateCPUCooler(CPUCoolerModel CPUCooler)
        {
            var Obj = await _table.Find(x => x.Id == CPUCooler.Id).FirstOrDefaultAsync();
            if (Obj != null) await _table.ReplaceOneAsync(x => x.Id == CPUCooler.Id, CPUCooler);
        }

        public async Task CreateCPUCooler(CPUCoolerModel CPUCooler)
        {
            var Obj = await _table.Find(x => x.Id == CPUCooler.Id).FirstOrDefaultAsync();
            if (Obj == null) await _table.InsertOneAsync(CPUCooler);
        }

        public async Task<IEnumerable<CPUCoolerModel>> GetCpuCoolersByValue()
        {
            var sortDefinition = Builders<CPUCoolerModel>.Sort.Descending(x => x.Price);
            var result = await _table.Find(new BsonDocument()).Sort(sortDefinition).ToListAsync();
            return result;
        }
    }
}
