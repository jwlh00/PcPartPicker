using MongoDB.Bson;
using MongoDB.Driver;
using PcPartPicker.IService;
using PcPartPicker.Models;

namespace PcPartPicker.Service
{
    public class CaseService : ICaseService
    {
        private MongoClient? _mongoClient = null;
        private IMongoDatabase? _database = null;
        private IMongoCollection<CaseModel> _table;

        public CaseService()
        {
            _mongoClient = new MongoClient("mongodb+srv://admin_3:admin_3@proyecto1.b0ylzal.mongodb.net/?retryWrites=true&w=majority");
            _database = _mongoClient.GetDatabase("PcPartPicker");
            _table = _database.GetCollection<CaseModel>("Case");
        }

        public async Task DeleteCase(string caseId)
        {
            await _table.DeleteOneAsync(x => x.Id == caseId);
        }

        public async Task<CaseModel> GetCase(string caseId)
        {
            return await Task.Run(() => _table.Find(x => x.Id == caseId).FirstOrDefault());
        }

        public async Task<IEnumerable<CaseModel>> GetCases()
        {
            return await Task.FromResult(_table.Find(FilterDefinition<CaseModel>.Empty).ToList());
        }

        public async Task UpdateCase(CaseModel Case)
        {
            var Obj = await _table.Find(x => x.Id == Case.Id).FirstOrDefaultAsync();
            if (Obj != null) await _table.ReplaceOneAsync(x => x.Id == Case.Id, Case);
        }

        public async Task CreateCase(CaseModel Case)
        {
            var Obj = await _table.Find(x => x.Id == Case.Id).FirstOrDefaultAsync();
            if (Obj == null) await _table.InsertOneAsync(Case);
        }


        //TODO: Sort here
        public async Task<IEnumerable<CaseModel>> GetCasesByValue()
        {
            var sortDefinition = Builders<CaseModel>.Sort.Descending(x => x.Price);
            var result = await _table.Find(new BsonDocument()).Sort(sortDefinition).ToListAsync();
            return result;
        }
    }
}
