namespace MazeCore.MongoDb.Settings
{
    public class MongoDbSetting : IMongoDbSetting
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}
