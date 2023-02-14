using MongoDB.Bson;
using MongoDB.Driver;
using PcPartPicker.IService;
using PcPartPicker.Models;

namespace PcPartPicker.Service
{
    public class PSUService : IPSUService
    {
        private MongoClient? _mongoClient = null;
        private IMongoDatabase? _database = null;
        private IMongoCollection<PSUModel> _table;

        public PSUService()
        {
            _mongoClient = new MongoClient("mongodb+srv://admin_3:admin_3@proyecto1.b0ylzal.mongodb.net/?retryWrites=true&w=majority");
            _database = _mongoClient.GetDatabase("PcPartPicker");
            _table = _database.GetCollection<PSUModel>("PSU");
        }

        public async Task DeletePSU(string PSUId)
        {
            await _table.DeleteOneAsync(x => x.Id == PSUId);
        }

        public async Task<PSUModel> GetPSU(string PSUId)
        {
            return await Task.Run(() => _table.Find(x => x.Id == PSUId).FirstOrDefault());
        }

        public async Task<IEnumerable<PSUModel>> GetPSUs()
        {
            return await Task.FromResult(_table.Find(FilterDefinition<PSUModel>.Empty).ToList());
        }

        public async Task UpdatePSU(PSUModel PSU)
        {
            var Obj = await _table.Find(x => x.Id == PSU.Id).FirstOrDefaultAsync();
            if (Obj != null) await _table.ReplaceOneAsync(x => x.Id == PSU.Id, PSU);
        }

        public async Task CreatePSU(PSUModel PSU)
        {
            var Obj = await _table.Find(x => x.Id == PSU.Id).FirstOrDefaultAsync();
            if (Obj == null) await _table.InsertOneAsync(PSU);
        }

        public async Task<IEnumerable<PSUModel>> GetPsusByValue()
        {
            var sortDefinition = Builders<PSUModel>.Sort.Descending(x => x.Price);
            var result = await _table.Find(new BsonDocument()).Sort(sortDefinition).ToListAsync();
            return result;
        }
    }
}
