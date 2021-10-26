namespace MazeCore.MongoDb.Settings
{
    public interface IMongoDbSetting
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
