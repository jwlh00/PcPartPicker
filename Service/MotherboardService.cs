using MongoDB.Bson;
using MongoDB.Driver;
using PcPartPicker.IService;
using PcPartPicker.Models;

namespace PcPartPicker.Service
{
    public class MotherboardService : IMotherboardService
    {
        private MongoClient? _mongoClient = null;
        private IMongoDatabase? _database = null;
        private IMongoCollection<MotherboardModel> _table;

        public MotherboardService()
        {
            _mongoClient = new MongoClient("mongodb+srv://admin_3:admin_3@proyecto1.b0ylzal.mongodb.net/?retryWrites=true&w=majority");
            _database = _mongoClient.GetDatabase("PcPartPicker");
            _table = _database.GetCollection<MotherboardModel>("Motherboard");
        }

        public async Task DeleteMotherboard(string motherboardId)
        {
            await _table.DeleteOneAsync(x => x.Id == motherboardId);
        }

        public async Task<MotherboardModel> GetMotherboard(string motherboardId)
        {
            return await Task.Run(() => _table.Find(x => x.Id == motherboardId).FirstOrDefault());
        }

        public async Task<IEnumerable<MotherboardModel>> GetMotherboards()
        {
            return await Task.FromResult(_table.Find(FilterDefinition<MotherboardModel>.Empty).ToList());
        }

        public async Task UpdateMotherboard(MotherboardModel Motherboard)
        {
            var Obj = await _table.Find(x => x.Id == Motherboard.Id).FirstOrDefaultAsync();
            if (Obj != null) await _table.ReplaceOneAsync(x => x.Id == Motherboard.Id, Motherboard);
        }

        public async Task CreateMotherboard(MotherboardModel Motherboard)
        {
            var Obj = await _table.Find(x => x.Id == Motherboard.Id).FirstOrDefaultAsync();
            if (Obj == null) await _table.InsertOneAsync(Motherboard);
        }

        public async Task<IEnumerable<MotherboardModel>> GetMotherboardsByValue()
        {
            var sortDefinition = Builders<MotherboardModel>.Sort.Descending(x => x.Price);
            var result = await _table.Find(new BsonDocument()).Sort(sortDefinition).ToListAsync();
            return result;
        }
    }
}
