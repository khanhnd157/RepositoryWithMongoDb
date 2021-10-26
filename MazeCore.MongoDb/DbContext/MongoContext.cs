using MazeCore.MongoDb.Settings;

using MongoDB.Driver;

namespace MazeCore.MongoDb.Context
{
    public class MongoContext : IMongoContext
    {
        public IMongoDatabase Database { get; }

        public MongoContext(IMongoDbSetting mongoDbSetting)
        {
            var client = new MongoClient(mongoDbSetting.ConnectionString);
            Database = client.GetDatabase(mongoDbSetting.DatabaseName);
        }
    }
}
