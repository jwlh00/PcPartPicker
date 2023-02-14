using MongoDB.Bson;
using MongoDB.Driver;
using PcPartPicker.IService;
using PcPartPicker.Models;
using System.Xml.Linq;

namespace PcPartPicker.Service
{
    public class BuildService : IBuildService
    {
        private MongoClient? _mongoClient = null;
        private IMongoDatabase? _database = null;
        private IMongoCollection<BuildModel> _table;
        private IMongoCollection<BuildUserModel> _tableUser;
        private IMongoCollection<BuildImageModel> _tableImg;
            
        public BuildService()
        {
            _mongoClient = new MongoClient("mongodb+srv://admin_3:admin_3@proyecto1.b0ylzal.mongodb.net/?retryWrites=true&w=majority");
            _database = _mongoClient.GetDatabase("PcPartPicker");
            _table = _database.GetCollection<BuildModel>("Build");
            _tableUser = _database.GetCollection<BuildUserModel>("Build");
            _tableImg = _database.GetCollection<BuildImageModel>("Build");

        }
        public async Task DeleteBuild(string buildId)
        {
            await _table.DeleteOneAsync(x => x.Id == buildId);
        }

        public async Task<BuildModel> GetBuild(string buildId)
        {
            return await Task.Run(()=> _table.Find(x => x.Id == buildId).FirstOrDefault());
        }

        public async Task<IEnumerable<BuildModel>> GetBuildAsEnum(string buildId)
        {
            return await Task.Run(() => _table.Find(x => x.Id == buildId).ToList());
        }

        public async Task<IEnumerable<BuildModel>> GetBuilds()
        {
            return await Task.FromResult(_table.Find(FilterDefinition<BuildModel>.Empty).ToList());
        }

        public async Task UpdateBuild(BuildModel Build)
        {
            var Obj = await _table.Find(x => x.Id == Build.Id).FirstOrDefaultAsync();
            if (Obj != null) await _table.ReplaceOneAsync(x => x.Id == Build.Id, Build);
        }

        public async Task CreateBuild(BuildModel Build)
        {
            var Obj = await _table.Find(x => x.Id == Build.Id).FirstOrDefaultAsync();
            if (Obj == null) await _table.InsertOneAsync(Build);
        }


        //TODO: Referenced Document and Proyection here
        public async Task<BuildUserModel> GetBuildUser(string buildId)
        {
            var build = await _tableUser.Find(x => x.Id == buildId).FirstOrDefaultAsync();
            var referenceCollection = _database.GetCollection<UserModel>("User");

            if (build == null)
                return null;

            var user = await referenceCollection.Find(x => x.Id == build.UsernameId)
                .Project(x => new UserModel { UserName = x.UserName, Gender = x.Gender })
                .FirstOrDefaultAsync();
            build.User = user;

            return build;
        }

        public async Task<IEnumerable<BuildImageModel>> GetBuildImage()
        {
            var builds = await _tableImg.Find(x => true).ToListAsync();
            var referenceCollection = _database.GetCollection<ImageModel>("fs.file");

            foreach (var build in builds)
            {
                var img = await referenceCollection.Find(x => x.filename == build.ImgName).FirstOrDefaultAsync();
                build.Image = img;
            }

            return builds;
        }
    }
}
